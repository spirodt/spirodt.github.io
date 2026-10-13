"""
Facebook, LinkedIn, Viber и WhatsApp НЕ извршуваат JavaScript — ги читаат само <meta> ознаките од HTML-от.
Blazor страната има еден index.html, па овој скрипт по `dotnet publish` прави:

  download.html, refer.html  — копија од index.html со сопствен наслов, опис и слика за преглед
                               (GitHub Pages ја служи /download од download.html, без пренасочување)
  и ги заменува апсолутните адреси со вистинската адреса на страната (ако е сопствен домен).

Употреба:  python make_static_pages.py <out/wwwroot> <site-url, пр. https://spirodt.github.io/>
"""
import html
import pathlib
import re
import sys

DEFAULT_URL = "https://spirodt.github.io/"

PAGES = {
    "download": {
        "title": "Преземи BSSR 7 — BSS · Business Software Solutions",
        "description": "Најновата верзија на деловниот софтвер BSSR за Windows (продажба, фактурирање, евиденција) + претходни верзии. Помош при инсталација: +389 70 541 763.",
        "image": "og/og-download.png",
        "alt": "Преземи BSSR 7 — деловен софтвер за Windows",
    },
    "refer": {
        "title": "Препорачај нè — BSS · Business Software Solutions",
        "description": "Знаете некој на кого му треба ИТ помош? Направете свој линк за препорака и споделете го на Facebook, Instagram, LinkedIn, Viber или WhatsApp.",
        "image": "og/og-refer.png",
        "alt": "Препорачај нè — сподели го твојот линк",
    },
}


def set_meta(doc: str, attr: str, key: str, value: str) -> str:
    pattern = re.compile(rf'(<meta\s+{attr}="{re.escape(key)}"\s+content=")[^"]*(")')
    return pattern.sub(lambda m: m.group(1) + html.escape(value, quote=True) + m.group(2), doc)


def main() -> None:
    root = pathlib.Path(sys.argv[1])
    site = sys.argv[2].rstrip("/") + "/"
    index_path = root / "index.html"
    index = index_path.read_text(encoding="utf-8")

    if site != DEFAULT_URL:
        index = index.replace(DEFAULT_URL, site)
        index_path.write_text(index, encoding="utf-8")

    for slug, p in PAGES.items():
        url, image = f"{site}{slug}", f"{site}{p['image']}"
        doc = re.sub(r"<title>.*?</title>", f"<title>{html.escape(p['title'])}</title>", index, flags=re.S)
        doc = re.sub(r'(<link rel="canonical" href=")[^"]*(")', rf"\g<1>{url}\g<2>", doc)
        for key, value in [("og:url", url), ("og:title", p["title"]), ("og:description", p["description"]),
                           ("og:image", image), ("og:image:secure_url", image), ("og:image:alt", p["alt"])]:
            doc = set_meta(doc, "property", key, value)
        for key, value in [("description", p["description"]), ("twitter:title", p["title"]),
                           ("twitter:description", p["description"]), ("twitter:image", image)]:
            doc = set_meta(doc, "name", key, value)
        (root / f"{slug}.html").write_text(doc, encoding="utf-8")
        print(f"✓ {slug}.html  →  {url}")


if __name__ == "__main__":
    main()
