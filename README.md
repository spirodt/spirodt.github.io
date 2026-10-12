# BSS — Business Software Solutions (Blazor WebAssembly, .NET 10)

Двојазична (МК / EN) страна за промоција на услуги: софтвер, веб, компјутерски сервис, мрежи и бекап — насочена кон клиенти во Аеродром, Скопје.

## Стартување

```bash
cd src/SpiroPortfolio
dotnet watch
```

Отвори адресата што ќе ја покаже терминалот (обично `http://localhost:5xxx`).

## Каде се менува содржината

Сите текстови се во **`src/SpiroPortfolio/Content/SiteContent.cs`**. Секој текст е `new("македонски", "English")`.

Пред објавување пребарај `TODO` во тој фајл:

| Што | Каде |
|---|---|
| Телефон и е-пошти (прва адреса = главна, на неа одат пораките) | `Contact` најгоре |
| Име на брендот | `BrandName`, `BrandFull` |
| Работно време (горна лента, контакт, футер) | `Hours` |
| Ценовник | `Pricing` — сегашните цени се почетни предлози |
| Години во искуството | `Timeline` |
| Бројки во херото (18+, 6, 24ч) | `Stats` |

## Контакт форма

Страната е статична (нема сервер), па формата работи на два начини:

1. **Без поставување** — „Испрати“ ја отвора е-пошта програмата на посетителот со пополнета порака до bss@bssr.mk (копија до другите две адреси).
2. **Со Formspree (препорачано, бесплатно до 50 пораки/месец)** — направи форма на formspree.io и стави ја адресата во `wwwroot/appsettings.json`:

```json
{ "FormEndpoint": "https://formspree.io/f/xxxxxxx" }
```

Пораките ќе ти стигнуваат на е-пошта без посетителот да отвора своја програма.

## Јазик

- Копчињата **МК / EN** горе десно; изборот се памти во прелистувачот.
- Директна врска на англиски: `.../?lang=en` (корисно за LinkedIn).

## Темен режим

Копчето со месечина/сонце во менито. Без избор, страната го следи системот (Windows/телефон); изборот се памти.
Боите се во `wwwroot/css/site.css` → `:root` (светли) и `:root[data-theme="dark"]` (темни).

## Страна „Преземи BSSR“ (`/download`)

Ги прикажува последните 5 инсталации од https://bssr.mk/bss/Installer/BSSR7/ (BSS.exe + 4 претходни).

- Списокот е во `wwwroot/data/bssr-releases.json`.
- GitHub Actions го освежува автоматски со `.github/scripts/update_bssr_releases.py` — при секој push **и секој ден во 06:00**, па новите верзии сами се појавуваат.
- Ако bssr.mk не одговори, останува последниот успешен список (објавувањето не паѓа).
- Рачно освежување: Actions → *Deploy to GitHub Pages* → **Run workflow**.

## Објавување (бесплатно) на GitHub Pages → https://spirodt.github.io

1. На GitHub направи **јавно** репо со точно име `spirodt.github.io`.
2. Push-ни го содржината на овој фолдер на гранката `main`:
   ```bash
   git init -b main
   git add .
   git commit -m "Portfolio site"
   git remote add origin https://github.com/spirodt/spirodt.github.io.git
   git push -u origin main
   ```
3. Во репото: **Settings → Pages → Build and deployment → Source: GitHub Actions**.
4. Отвори го табот **Actions** — по 2–3 минути страната е на https://spirodt.github.io.

Секој нов push на `main` автоматски ја објавува новата верзија.

Друго име на репо (пр. `portfolio`) исто работи, но адресата е `https://spirodt.github.io/portfolio/` — workflow-от сам го поправа `base href`.
Со сопствен домен (пр. `spiro.mk`): избриши го чекорот *Fix base href* и додај го доменот во Settings → Pages → Custom domain.

## Структура

```
src/SpiroPortfolio/
├─ Content/SiteContent.cs      ← сите текстови (МК + EN)
├─ Services/LanguageService.cs ← јазик, localStorage, ?lang=
├─ Services/ContactService.cs  ← Formspree / mailto
├─ Components/                 ← секции: Hero, About, Services, Software, Experience, Projects, Pricing, Contact
├─ Layout/MainLayout.razor
└─ wwwroot/                    ← index.html (SEO, schema.org), css, js
```
