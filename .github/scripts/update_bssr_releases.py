"""
Ги чита последните инсталации на BSSR од bssr.mk и го запишува wwwroot/data/bssr-releases.json.

Се извршува во GitHub Actions пред секое објавување (и еднаш дневно).
Ако bssr.mk не е достапен или форматот се смени, излегува со грешка БЕЗ да го допре
постоечкиот JSON — страната продолжува со последниот успешен список.

Употреба:  python update_bssr_releases.py <патека-до-json> [број-на-верзии]
"""
import datetime
import json
import pathlib
import re
import sys
import urllib.parse
import urllib.request

SOURCE = "https://bssr.mk/bss/Installer/BSSR7/"
LATEST_NAME = "BSS.exe"  # тековната верзија, без број во името


def fetch(url: str) -> str:
    req = urllib.request.Request(url, headers={"User-Agent": "Mozilla/5.0 (BSS website build)"})
    with urllib.request.urlopen(req, timeout=30) as resp:
        return resp.read().decode("utf-8", "replace")


def parse(html: str) -> list[dict]:
    files, seen = [], set()
    for m in re.finditer(r'href="([^"]+?\.exe)"', html, re.IGNORECASE):
        href = m.group(1)
        name = urllib.parse.unquote(href.rstrip("/").split("/")[-1])
        if name in seen:
            continue
        seen.add(name)

        # Датумот и големината стојат во истиот ред од табелата, веднаш по врската.
        row = html[m.end(): m.end() + 1500]
        nxt = re.search(r'href="[^"]+?\.exe"', row, re.IGNORECASE)
        if nxt:
            row = row[: nxt.start()]
        date = re.search(r"(\d{4}-\d{2}-\d{2} \d{2}:\d{2})", row)
        size = re.search(r">\s*(\d+)\s*k\s*<", row) or re.search(r"\b(\d+)k\b", row)
        version = re.search(r"(\d+\.\d+\.\d+)", name)

        files.append({
            "name": name,
            "url": urllib.parse.urljoin(SOURCE, href),
            "version": version.group(1) if version else None,
            "date": date.group(1) if date else "",
            "sizeKb": int(size.group(1)) if size else 0,
            "latest": False,
        })
    return files


def version_key(f: dict):
    v = f["version"]
    return tuple(int(p) for p in v.split(".")) if v else (10**9,)


def main() -> int:
    out = pathlib.Path(sys.argv[1])
    keep = int(sys.argv[2]) if len(sys.argv) > 2 else 5

    files = parse(fetch(SOURCE))
    if not files:
        print("::warning::Не најдов .exe фајлови на bssr.mk — го задржувам постоечкиот список.")
        return 1

    # Најнова = BSS.exe (ако постои), па претходните по број на верзија, од нови кон стари.
    files.sort(key=lambda f: (f["name"] == LATEST_NAME, version_key(f), f["date"]), reverse=True)
    files = files[:keep]
    files[0]["latest"] = True

    data = {
        "source": SOURCE,
        "updated": datetime.datetime.now(datetime.timezone.utc).strftime("%Y-%m-%dT%H:%M:%SZ"),
        "files": files,
    }
    out.write_text(json.dumps(data, ensure_ascii=False, indent=2) + "\n", encoding="utf-8")
    print(f"Запишани {len(files)} верзии во {out}:")
    for f in files:
        print(f"  {f['name']:32} {f['date']:17} {f['sizeKb']}k")
    return 0


if __name__ == "__main__":
    sys.exit(main())
