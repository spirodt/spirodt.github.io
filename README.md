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

## Контакт форма — праќање на е-пошта

Страната е статична (нема сервер), па пораките се праќаат преку бесплатен сервис:

1. Отвори https://web3forms.com, направи бесплатна сметка со **bss@bssr.mk** и земи **Access Key** (бесплатно до 250 пораки/месец).
2. Стави го во `src/SpiroPortfolio/wwwroot/appsettings.json`:
   ```json
   { "Web3FormsKey": "xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx", "FormEndpoint": "" }
   ```
3. Push. Пораките стигнуваат на bss@bssr.mk; „Одговори“ оди директно до посетителот.

Алтернатива: Formspree → `"FormEndpoint": "https://formspree.io/f/xxxxxxx"`.
Без клуч, „Испрати барање“ ја отвора е-пошта програмата на посетителот (до bss@bssr.mk, копија до другите две).
Копчето **„Испрати преку е-пошта“** секогаш ја отвора е-пошта програмата.

Со секоја порака оди и **од каде дошол посетителот** (препорака, Facebook, Google, број на посети…).

## Препораки и извор на посетите

- Скриптата во `wwwroot/index.html` ги памти `?ref=`, `utm_source/medium/campaign` и страната од која дошол посетителот (само во неговиот прелистувач — `bss_first_touch`, `bss_last_touch`, `bss_referred_by`, `bss_visits`).
- `/refer` — секој прави свој линк `https://spirodt.github.io/?ref=marko-petrov-k7m2` (името е дел од кодот). Ако е поставен Web3Forms, добиваш е-пошта „Нов линк за препорака“ со име, телефон и код.
- Наградата за препорака: `ReferReward` во `SiteContent.cs` (празно = не се прикажува).
- Линкови за флаери/реклами: додај `?utm_source=flyer` или `?utm_source=fb-ads&utm_campaign=oktomvri` за да знаеш од каде дошле.

## Споделување на Facebook / LinkedIn

- Сликите за преглед се во `wwwroot/og/` (1200×630). Повторно генерирање: `python tools/og-images/make_og_images.py <папка-со-manrope-woff2>`.
- По објавување, освежи го прегледот: Facebook → https://developers.facebook.com/tools/debug/ · LinkedIn → https://www.linkedin.com/post-inspector/
- Со сопствен домен: GitHub → Settings → Secrets and variables → Actions → **Variables** → `SITE_URL` = `https://bss.bssr.mk/`.

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
