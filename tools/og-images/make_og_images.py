"""
Ги прави сликите за преглед при споделување (Facebook / LinkedIn / Viber / WhatsApp), 1200×630 PNG.
Илустрациите се земаат од Components/ServiceArtLibrary.cs, за да се совпаѓаат со страната.

Употреба (потребни: pip install playwright && playwright install chromium):
    python tools/og-images/make_og_images.py <папка-со-manrope-woff2> [chromium-патека]
Излез: src/SpiroPortfolio/wwwroot/og/og-home.png, og-download.png, og-refer.png
"""
import pathlib
import re
import sys

ROOT = pathlib.Path(__file__).resolve().parents[2]
ART_CS = ROOT / "src/SpiroPortfolio/Components/ServiceArtLibrary.cs"
OUT = ROOT / "src/SpiroPortfolio/wwwroot/og"

cs = ART_CS.read_text(encoding="utf-8")
OPEN = re.search(r'private const string Open =\s*"""(.*?)""";', cs, re.S).group(1)
ART = {k: v for k, v in re.findall(r'\["(\w+)"\] = """(.*?)"""', cs, re.S)}


def art(key: str) -> str:
    return OPEN + ART[key] + "</svg>"


LOGO = """<svg viewBox="0 0 40 40" width="64" height="64"><rect width="40" height="40" rx="10" fill="#fff"/>
<path d="M12.5 13.5 8 20l4.5 6.5M27.5 13.5 32 20l-4.5 6.5" fill="none" stroke="#0b1f4d" stroke-width="2.4" stroke-linecap="round" stroke-linejoin="round" opacity=".5"/>
<text x="20" y="27.2" text-anchor="middle" font-family="Manrope" font-weight="800" font-size="19" fill="#0b1f4d">B</text>
<circle cx="31" cy="9" r="3" fill="#f5a524"/></svg>"""

PAGES = {
    "og-home": dict(
        eyebrow="Аеродром · Скопје",
        title="Софтвер, веб и ИТ&nbsp;сервис — од едно место",
        sub="Деловни програми · Веб страни · Сервис и електроника · Мрежи и бекап · DevOps · Cloud · AI",
        right=f"""<div class="tiles">
            <div class="tile t-home">{art("pc")}</div><div class="tile t-dev">{art("web")}</div>
            <div class="tile t-pro">{art("cloud")}</div><div class="tile t-pro">{art("devops")}</div></div>""",
    ),
    "og-download": dict(
        eyebrow="BSSR 7 · Windows",
        title="Преземи BSSR — деловен софтвер",
        sub="Продажба · Фактурирање · Евиденција на уплати · Резервации · Гаранции · Најновата верзија секогаш на врвот",
        right=f"""<div class="one t-dev">{art("software")}</div>""",
    ),
    "og-refer": dict(
        eyebrow="Препорака",
        title="Препорачај нè — сподели го твојот линк",
        sub="Знаете некој на кого му треба помош со компјутер, програма, веб страна или cloud? Еден клик и сте ги поврзале.",
        right=f"""<div class="one t-pro">{art("consulting")}</div>""",
    ),
}

CSS = """
@font-face{font-family:Manrope;font-weight:400;src:url(FONTS/manrope-cyrillic-400-normal.woff2)}
@font-face{font-family:Manrope;font-weight:400;src:url(FONTS/manrope-latin-400-normal.woff2);unicode-range:U+0000-00FF,U+2000-206F}
@font-face{font-family:Manrope;font-weight:600;src:url(FONTS/manrope-cyrillic-600-normal.woff2)}
@font-face{font-family:Manrope;font-weight:600;src:url(FONTS/manrope-latin-600-normal.woff2);unicode-range:U+0000-00FF,U+2000-206F}
@font-face{font-family:Manrope;font-weight:700;src:url(FONTS/manrope-cyrillic-700-normal.woff2)}
@font-face{font-family:Manrope;font-weight:700;src:url(FONTS/manrope-latin-700-normal.woff2);unicode-range:U+0000-00FF,U+2000-206F}
@font-face{font-family:Manrope;font-weight:800;src:url(FONTS/manrope-cyrillic-800-normal.woff2)}
@font-face{font-family:Manrope;font-weight:800;src:url(FONTS/manrope-latin-800-normal.woff2);unicode-range:U+0000-00FF,U+2000-206F}
*{box-sizing:border-box}
body{margin:0;width:1200px;height:630px;font-family:Manrope,sans-serif;overflow:hidden}
.card{width:1200px;height:630px;display:grid;grid-template-columns:620px 580px;background:#0b1f4d;position:relative;overflow:hidden}
.ring{position:absolute;left:-140px;bottom:-180px;width:460px;height:460px;border-radius:50%;border:80px solid rgba(245,165,36,.13)}
.left{position:relative;padding:58px 0 0 64px;color:#fff;display:flex;flex-direction:column}
.brand{display:flex;align-items:center;gap:16px}
.brand b{font-size:34px;font-weight:800;line-height:1;letter-spacing:-.02em}
.brand small{display:block;font-size:14px;font-weight:700;letter-spacing:.14em;text-transform:uppercase;color:rgba(255,255,255,.62);margin-top:6px}
.eyebrow{display:inline-block;align-self:flex-start;margin-top:44px;background:#f5a524;color:#0b1f4d;font-size:17px;font-weight:800;letter-spacing:.12em;text-transform:uppercase;padding:8px 14px;border-radius:8px}
h1{margin:22px 0 0;font-size:56px;line-height:1.02;font-weight:800;letter-spacing:-.035em;max-width:520px}
p{margin:20px 0 0;font-size:20px;line-height:1.45;color:rgba(255,255,255,.78);max-width:500px;font-weight:600}
.right{position:relative;background:#f5f7fb;display:flex;align-items:center;justify-content:center;padding:40px}
.tiles{display:grid;grid-template-columns:1fr 1fr;gap:16px;width:100%}
.tile,.one{border-radius:18px;overflow:hidden;box-shadow:0 20px 40px -24px rgba(11,31,77,.5)}
.tile svg,.one svg{display:block;width:100%;height:auto}
.one{width:100%}
.t-home{background:#fff1d6}.t-dev{background:#e3eafd}.t-pro{background:#e8ecf4}
.bar{position:absolute;left:0;right:0;bottom:0;height:78px;background:#f5a524;display:flex;align-items:center;justify-content:space-between;padding:0 64px;color:#0b1f4d}
.bar .phone{font-size:30px;font-weight:800;letter-spacing:-.01em}
.bar .meta{font-size:19px;font-weight:700;display:flex;gap:26px}
"""


def html(page: dict, fonts: str) -> str:
    return f"""<!doctype html><html lang="mk"><head><meta charset="utf-8"><style>{CSS.replace("FONTS", fonts)}</style></head>
<body><div class="card"><div class="ring"></div>
<div class="left">
  <div class="brand">{LOGO}<div><b>BSS</b><small>Business Software Solutions</small></div></div>
  <span class="eyebrow">{page["eyebrow"]}</span>
  <h1>{page["title"]}</h1>
  <p>{page["sub"]}</p>
</div>
<div class="right">{page["right"]}</div>
<div class="bar"><span class="phone">+389 70 541 763</span><span class="meta"><span>bss@bssr.mk</span><span>spirodt.github.io</span></span></div>
</div></body></html>"""


def main() -> None:
    from playwright.sync_api import sync_playwright

    fonts = pathlib.Path(sys.argv[1]).resolve().as_uri()
    chromium = sys.argv[2] if len(sys.argv) > 2 else None
    OUT.mkdir(parents=True, exist_ok=True)
    tmp = OUT / "_tmp.html"
    with sync_playwright() as p:
        browser = p.chromium.launch(executable_path=chromium) if chromium else p.chromium.launch()
        page = browser.new_page(viewport={"width": 1200, "height": 630})
        for name, data in PAGES.items():
            tmp.write_text(html(data, fonts), encoding="utf-8")
            page.goto(tmp.as_uri())
            page.wait_for_timeout(400)
            page.screenshot(path=str(OUT / f"{name}.png"))
            print("✓", OUT / f"{name}.png")
        browser.close()
    tmp.unlink()


if __name__ == "__main__":
    main()
