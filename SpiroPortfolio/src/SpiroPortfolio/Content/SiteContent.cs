using SpiroPortfolio.Services;

namespace SpiroPortfolio.Content;

// ─────────────────────────────────────────────────────────────────────────────
//  СИТЕ ТЕКСТОВИ НА СТРАНАТА СЕ ТУКА.
//  Секој текст е Loc("македонски", "English"). Смени го тука и готово.
//  Ознаките  // TODO  се места каде што треба твој вистински податок.
// ─────────────────────────────────────────────────────────────────────────────

public record Contact(string Phone, string PhoneHref, string Email, string ViberHref, string WhatsAppHref, string LinkedIn, Loc Area)
{
    /// <summary>Е-поштата се прикажува дури кога ќе ја замениш примерната адреса.</summary>
    public bool HasEmail => !Email.Contains("example", StringComparison.OrdinalIgnoreCase);
}
public record Step(string Number, Loc Title, Loc Text);
public record Reason(string Icon, Loc Title, Loc Text);
public record Skill(string Name, Loc Group);
public record ServiceItem(string Icon, Loc Title, Loc Text, Loc[] Points);
public record Product(string Icon, Loc Name, Loc Text, Loc Tag);
public record TimelineItem(Loc Period, Loc Title, Loc Text, string[] Tech);
public record Project(Loc Name, Loc Kind, Loc Text, string[] Tech, Loc[] Highlights);
public record PriceItem(Loc Name, Loc Price, Loc Note);
public record PriceGroup(Loc Title, PriceItem[] Items);
public record Stat(Loc Value, Loc Label);

public static class SiteContent
{
    // ── Контакт ──────────────────────────────────────────────────────────────
    public static readonly Contact Contact = new(
        Phone: "070 541 763",
        PhoneHref: "tel:+38970541763",
        Email: "info@example.mk",                    // TODO: е-пошта за клиенти (додека е „example“, не се прикажува)
        ViberHref: "viber://chat?number=%2B38970541763",
        WhatsAppHref: "https://wa.me/38970541763",
        LinkedIn: "https://www.linkedin.com/in/spirodt/",
        Area: new("Аеродром, Скопје", "Aerodrom, Skopje"));

    // TODO: твое работно време
    public static readonly Loc Hours = new("Пон–Саб · 08–20 ч", "Mon–Sat · 8am–8pm");

    public static readonly Loc MetaTitle = new(
        "Спиро — софтвер, веб и компјутерски сервис · Аеродром, Скопје",
        "Spiro — software, web & computer service · Aerodrom, Skopje");

    // ── Навигација ───────────────────────────────────────────────────────────
    public static readonly (string Id, Loc Label)[] Nav =
    [
        ("services", new("Услуги", "Services")),
        ("software", new("Програми", "Software")),
        ("pricing", new("Цени", "Pricing")),
        ("about", new("За мене", "About")),
        ("projects", new("Проекти", "Projects")),
        ("contact", new("Контакт", "Contact")),
    ];

    // ── Херо ─────────────────────────────────────────────────────────────────
    public static readonly Loc HeroEyebrow = new(
        "Софтвер девелопер · 18 години искуство",
        "Software developer · 18 years of experience");

    public static readonly Loc HeroTitle = new(
        "Софтвер, веб и компјутери — од еден човек кој ги знае сите три.",
        "Software, web and computers — from one person who knows all three.");

    public static readonly Loc HeroLead = new(
        "Изработувам деловни програми и веб решенија, сервисирам компјутери и поставувам мрежи и бекап за фирми и домаќинства во Скопје. Брз одзив, јасна цена, одговор на македонски.",
        "I build business software and web solutions, repair computers, and set up networks and backups for businesses and homes in Skopje. Quick response, clear pricing, straight answers.");

    public static readonly Loc CtaPrimary = new("Побарај понуда", "Request a quote");
    public static readonly Loc CtaSecondary = new("Погледни цени", "See pricing");

    public static readonly Stat[] Stats =
    [
        new(new("18+", "18+"), new("години искуство", "years of experience")),
        new(new("6", "6"), new("готови деловни програми", "ready-made business apps")),
        new(new(".NET 10", ".NET 10"), new("модерна технологија", "modern stack")),
        new(new("24ч", "24h"), new("одговор на барање", "reply to requests")),
    ];

    // ── Зошто јас ────────────────────────────────────────────────────────────
    public static readonly Loc WhyTitle = new("Зошто клиентите се враќаат", "Why clients come back");

    public static readonly Reason[] Reasons =
    [
        new("pin", new("Блиску до вас", "Close to you"),
            new("Во Аеродром сум — доаѓам на адреса, без чекање со денови.", "I'm based in Aerodrom — I come to you, no waiting for days.")),
        new("receipt", new("Цена пред работа", "Price before work"),
            new("Секогаш знаете колку чини пред да почнам. Без скриени трошоци.", "You always know the cost before I start. No hidden fees.")),
        new("code", new("Програмер, не само сервисер", "A developer, not just a technician"),
            new("Ги решавам и проблемите што другите ги враќаат — до ниво на код и база.", "I fix the problems others give up on — down to code and database.")),
        new("shield", new("Податоците на прво место", "Your data first"),
            new("Пред секоја интервенција — бекап. По неа — објаснување што е направено.", "A backup before every job, a clear explanation after it.")),
    ];

    // ── Како работам ─────────────────────────────────────────────────────────
    public static readonly Loc ProcessTitle = new("Како работам", "How I work");

    public static readonly Step[] Process =
    [
        new("01", new("Јавете се или пишете", "Call or message"),
            new("Телефон, Viber или формата подолу — кажете ми што ви треба.", "Phone, Viber or the form below — tell me what you need.")),
        new("02", new("Бесплатна процена", "Free assessment"),
            new("Ја гледам состојбата и ви давам точна цена и рок.", "I look at the situation and give you an exact price and timeline.")),
        new("03", new("Изработка", "The work"),
            new("Работам брзо и уредно, со бекап на податоците пред да почнам.", "Fast, tidy work, with your data backed up before I begin.")),
        new("04", new("Поддршка", "Support"),
            new("Останувам достапен и по завршувањето — не исчезнувам.", "I stay available after the job is done — I don't disappear.")),
    ];

    // ── За мене ──────────────────────────────────────────────────────────────
    public static readonly Loc AboutTitle = new("За мене", "About me");

    public static readonly Loc[] AboutParagraphs =
    [
        new("Јас сум Спиро, софтвер девелопер од Скопје. Повеќе од 18 години пишувам деловен софтвер — од десктоп апликации на .NET Framework до денешни веб системи на .NET 10 и Blazor.",
            "I'm Spiro, a software developer from Skopje. For over 18 years I've been writing business software — from desktop apps on .NET Framework to today's web systems on .NET 10 and Blazor."),
        new("Покрај програмирањето, работам и „под хаубата“: склопување и поправка на компјутери, мрежи, сервери, VPN и автоматски бекап. Тоа значи дека кога ќе ви поставам програма, знам и на што ќе работи и како да ги заштитам податоците.",
            "Beyond code I also work under the hood: building and repairing computers, networks, servers, VPN and automated backups. So when I install software for you, I also know what it runs on and how to keep your data safe."),
        new("Работам директно со клиентот — без посредници, без непотребни зборови. Ако нешто не е потребно, ќе ви кажам.",
            "You work with me directly — no middlemen, no jargon. If you don't need something, I'll tell you."),
    ];

    public static readonly Skill[] Skills =
    [
        new("C#", new("Програмирање", "Development")),
        new(".NET 10", new("Програмирање", "Development")),
        new("Blazor", new("Програмирање", "Development")),
        new("ASP.NET Core", new("Програмирање", "Development")),
        new("EF Core", new("Програмирање", "Development")),
        new(".NET Aspire", new("Програмирање", "Development")),
        new("MudBlazor", new("Програмирање", "Development")),
        new("WinForms / .NET 4.8", new("Програмирање", "Development")),
        new("SQL Server", new("Бази и сервери", "Data & servers")),
        new("Redis", new("Бази и сервери", "Data & servers")),
        new("Docker", new("Бази и сервери", "Data & servers")),
        new("Linux / Ubuntu", new("Бази и сервери", "Data & servers")),
        new("Windows", new("Бази и сервери", "Data & servers")),
        new("OpenWrt", new("Мрежи", "Networking")),
        new("OpenVPN", new("Мрежи", "Networking")),
        new("Traefik / Caddy", new("Мрежи", "Networking")),
        new("Pi-hole / DNS", new("Мрежи", "Networking")),
        new("Nextcloud", new("Бекап", "Backup")),
        new("NAS / RAID", new("Бекап", "Backup")),
        new("PC / Laptop", new("Хардвер", "Hardware")),
    ];

    // ── Услуги ───────────────────────────────────────────────────────────────
    public static readonly Loc ServicesTitle = new("Што можам да направам за вас", "What I can do for you");
    public static readonly Loc ServicesLead = new(
        "Една точка за контакт за софтвер, веб и компјутерска опрема.",
        "One point of contact for software, web and IT equipment.");

    public static readonly ServiceItem[] Services =
    [
        new("code", new("Софтвер по мерка", "Custom software"),
            new("Деловни програми прилагодени на вашиот начин на работа — од мала евиденција до целосен систем.",
                "Business applications shaped around how you actually work — from a simple register to a full system."),
            [new("Анализа на потребите", "Requirements analysis"), new("Десктоп или веб", "Desktop or web"), new("Обука и поддршка", "Training and support")]),

        new("globe", new("Веб страни и апликации", "Websites & web apps"),
            new("Брзи, модерни страни за вашиот бизнис, на македонски и англиски, кои се гледаат добро и на телефон.",
                "Fast, modern sites for your business, in Macedonian and English, that look right on a phone."),
            [new("Презентациски страни", "Business websites"), new("Веб апликации", "Web applications"), new("Домен и хостинг", "Domain and hosting")]),

        new("wrench", new("Сервис на компјутери", "Computer repair"),
            new("Хардверски и софтверски проблеми, бавни компјутери, вируси, надградби и нови конфигурации.",
                "Hardware and software faults, slow PCs, malware, upgrades and new builds."),
            [new("Дијагностика", "Diagnostics"), new("Инсталација на систем", "OS installation"), new("SSD и RAM надградба", "SSD & RAM upgrades")]),

        new("network", new("Мрежи", "Networking"),
            new("Дизајн и поставување на мрежи за канцеларии, објекти и домови — кабел, Wi‑Fi, рутери и безбеден далечински пристап.",
                "Design and setup of networks for offices, shops and homes — cabling, Wi‑Fi, routers and secure remote access."),
            [new("Wi‑Fi покриеност", "Wi‑Fi coverage"), new("VPN пристап", "VPN access"), new("Блокирање реклами и закани", "Ad & threat blocking")]),

        new("shield", new("Одржување и бекап", "Maintenance & backup"),
            new("Редовно одржување и автоматски бекап, за податоците да не исчезнат кога ќе откаже диск.",
                "Regular maintenance and automated backups, so your data survives when a drive fails."),
            [new("Автоматски бекап", "Automated backup"), new("NAS и облак", "NAS and cloud"), new("Месечно одржување", "Monthly maintenance")]),
    ];

    // ── Готови програми ──────────────────────────────────────────────────────
    public static readonly Loc SoftwareTitle = new("Готови деловни програми", "Ready-made business software");
    public static readonly Loc SoftwareLead = new(
        "Проверени решенија кои ги прилагодувам на вашата фирма. Побарајте демо — ќе ви покажам на живо.",
        "Proven solutions I tailor to your business. Ask for a demo — I'll show you live.");

    public static readonly Product[] Products =
    [
        new("cart", new("Продажба (POS)", "Sales (POS)"),
            new("Каса за продавници и угостителство, со залиха, извештаи и вградени AI функции.",
                "Point of sale for shops and hospitality, with stock, reports and built-in AI features."),
            new("BSSR POS", "BSSR POS")),
        new("receipt", new("Фактурирање", "Invoicing"),
            new("Фактури, профактури и испратници, преглед на долгови и извештаи за сметководство.",
                "Invoices, pro-formas and delivery notes, outstanding balances and accountant-ready reports."),
            new("Фирми", "Businesses")),
        new("building", new("Водење згради", "Building management"),
            new("Станари, заеднички трошоци, задолжувања по стан и известувања — за управители и куќни совети.",
                "Residents, shared costs, per-flat charges and notices — for building managers and residents' councils."),
            new("Управители", "Managers")),
        new("wallet", new("Евиденција на уплати", "Payment tracking"),
            new("Кој платил, кој должи и колку — со потсетници и извештаи по период.",
                "Who paid, who owes and how much — with reminders and period reports."),
            new("Членарини, наеми", "Fees, rent")),
        new("calendar", new("Резервации", "Reservations"),
            new("Календар на термини, соби или маси, без двојни резервации.",
                "A calendar of appointments, rooms or tables, with no double bookings."),
            new("Салони, сместување", "Salons, lodging")),
        new("badge", new("Евиденција на гаранции", "Warranty tracking"),
            new("Продадени уреди, сериски броеви, рокови на гаранција и сервисна историја.",
                "Sold devices, serial numbers, warranty periods and service history."),
            new("Сервиси, техника", "Repair shops, retail")),
    ];

    // ── Искуство ─────────────────────────────────────────────────────────────
    public static readonly Loc ExperienceTitle = new("Искуство", "Experience");
    public static readonly Loc ExperienceLead = new(
        "Од првите десктоп програми до модерни облак системи.",
        "From early desktop programs to modern cloud-ready systems.");

    // TODO: провери ги периодите и додади работодавци/фирми ако сакаш да ги прикажеш
    public static readonly TimelineItem[] Timeline =
    [
        new(new("2026", "2026"),
            new("BSSR POS — продажба со вештачка интелигенција", "BSSR POS — AI-assisted point of sale"),
            new("Касата добива вградени AI функции за побрза работа на продавачот и подобри извештаи.",
                "The POS gains built-in AI features for faster checkout work and smarter reporting."),
            ["AI", ".NET", "POS"]),
        new(new("2025 — денес", "2025 — now"),
            new("BSAspire — нова генерација деловен систем", "BSAspire — next-generation business system"),
            new("Целосна изработка на веб систем за управување со бизнис на .NET 10 и Blazor, со македонски интерфејс и автоматски тестови.",
                "End-to-end build of a web-based business management system on .NET 10 and Blazor, with a Macedonian UI and automated tests."),
            [".NET 10", "Blazor", "Aspire", "EF Core", "SQL Server"]),
        new(new("2010-ти", "2010s"),
            new("BSSMainTS — десктоп деловен софтвер", "BSSMainTS — desktop business software"),
            new("Развој и одржување на деловна апликација за продажба, фактурирање и евиденција, која ја користат фирми секој ден; подоцна овозможен далечински пристап за повеќе корисници.",
                "Building and maintaining a business application for sales, invoicing and records used daily by companies; later enabled for remote multi-user access."),
            ["C#", "WinForms", ".NET 4.8", "SQL Server"]),
        new(new("2008", "2008"),
            new("Почеток — софтвер и компјутерски сервис", "Start — software and computer service"),
            new("Први деловни програми по нарачка, сервис на компјутери и поставување мрежи за мали фирми.",
                "First custom business programs, computer repair and network setups for small businesses."),
            ["C#", ".NET", "Hardware"]),
    ];

    // ── Проекти ──────────────────────────────────────────────────────────────
    public static readonly Loc ProjectsTitle = new("Избрани проекти", "Selected projects");

    public static readonly Project[] Projects =
    [
        new(new("BSAspire", "BSAspire"),
            new("Деловен систем · веб", "Business system · web"),
            new("Модуларен систем за управување со бизнис со каса (POS), шифрарници, документи и извештаи. Градена со чиста архитектура и покриена со интеграциски тестови.",
                "A modular business management system with POS, master data, documents and reports. Built on a clean architecture and covered by integration tests."),
            [".NET 10", "Blazor Server", "MudBlazor", "ASP.NET Core API", "EF Core", "SQL Server", "Redis", "Aspire"],
            [new("42 ентитети", "42 entities"), new("180 интеграциски тестови", "180 integration tests"), new("Македонски интерфејс", "Macedonian UI")]),

        new(new("BSSR POS", "BSSR POS"),
            new("Продажба · AI", "Point of sale · AI"),
            new("Каса за продавници со вградени AI можности — побрзо внесување, паметни предлози и подобар преглед на продажбата.",
                "A shop POS with built-in AI capabilities — faster entry, smart suggestions and clearer sales insight."),
            [".NET", "AI", "SQL Server"],
            [new("Вградена AI", "Built-in AI"), new("Продавници и угостителство", "Retail & hospitality")]),

        new(new("BSSMainTS", "BSSMainTS"),
            new("Десктоп → далечински пристап", "Desktop → remote access"),
            new("Постоечка WinForms апликација е направена достапна за 5–25 корисници од различни локации, без препишување и без скапи лиценци.",
                "An existing WinForms application made available to 5–25 users across locations, with no rewrite and no expensive licences."),
            ["WinForms", ".NET 4.8", "MeshCentral"],
            [new("5–25 корисници", "5–25 users"), new("Без нови лиценци", "No new licences")]),

        new(new("Мала приватна инфраструктура", "Small private infrastructure"),
            new("Мрежа · бекап · сервери", "Network · backup · servers"),
            new("Целосно поставена мрежа со сопствен рутер, VPN, блокирање реклами, приватен облак за документи и фотографии и автоматски бекап — истото што го нудам за мали канцеларии.",
                "A complete network with a custom router, VPN, ad blocking, a private cloud for files and photos and automated backups — the same setup I offer to small offices."),
            ["OpenWrt", "Docker", "Traefik", "Nextcloud", "Immich", "OpenVPN", "Pi-hole"],
            [new("Сè на сопствен хардвер", "All on own hardware"), new("Пристап од секаде", "Access from anywhere")]),
    ];

    // ── Цени ─────────────────────────────────────────────────────────────────
    public static readonly Loc PricingTitle = new("Ценовник", "Pricing");
    public static readonly Loc PricingLead = new(
        "Ориентациони цени во денари. Точна цена добивате пред да почнам — без изненадувања.",
        "Indicative prices in MKD. You get an exact price before I start — no surprises.");
    public static readonly Loc PricingNote = new(
        "Цените се без ДДВ и без делови/лиценци. За фирми со месечен договор — попуст на сервис.",
        "Prices exclude VAT, parts and licences. Businesses on a monthly plan get a service discount.");

    // TODO: прилагоди ги цените — ова се почетни предлози
    public static readonly PriceGroup[] Pricing =
    [
        new(new("Компјутерски сервис", "Computer service"),
        [
            new(new("Дијагностика", "Diagnostics"), new("500 ден.", "500 MKD"), new("се одбива ако се поправа", "deducted if repaired")),
            new(new("Инсталација на Windows + драјвери", "Windows install + drivers"), new("1.200 ден.", "1,200 MKD"), new("со основни програми", "incl. basic apps")),
            new(new("Чистење вируси и забрзување", "Malware removal & tune-up"), new("1.000 ден.", "1,000 MKD"), new("", "")),
            new(new("Пренос на податоци / нов SSD", "Data transfer / new SSD"), new("од 800 ден.", "from 800 MKD"), new("", "")),
            new(new("Излез на адреса во Аеродром", "Home visit in Aerodrom"), new("бесплатно", "free"), new("со поправка", "with a repair")),
        ]),
        new(new("Мрежи и бекап", "Networks & backup"),
        [
            new(new("Поставување рутер и Wi‑Fi", "Router & Wi‑Fi setup"), new("од 1.500 ден.", "from 1,500 MKD"), new("дом / мала канцеларија", "home / small office")),
            new(new("Мрежа за канцеларија", "Office network"), new("од 6.000 ден.", "from 6,000 MKD"), new("по увид", "after site visit")),
            new(new("VPN далечински пристап", "VPN remote access"), new("од 3.000 ден.", "from 3,000 MKD"), new("", "")),
            new(new("Автоматски бекап / NAS", "Automated backup / NAS"), new("од 4.000 ден.", "from 4,000 MKD"), new("поставување", "setup")),
            new(new("Месечно одржување", "Monthly maintenance"), new("од 3.000 ден./мес.", "from 3,000 MKD/mo"), new("до 5 компјутери", "up to 5 PCs")),
        ]),
        new(new("Софтвер и веб", "Software & web"),
        [
            new(new("Презентациска веб страна", "Business website"), new("од 18.000 ден.", "from 18,000 MKD"), new("МК + EN, мобилна верзија", "MK + EN, mobile-ready")),
            new(new("Готова програма (лиценца)", "Ready-made app (licence)"), new("од 1.500 ден./мес.", "from 1,500 MKD/mo"), new("со поддршка", "incl. support")),
            new(new("Прилагодување на програма", "App customisation"), new("по договор", "on request"), new("", "")),
            new(new("Софтвер по мерка", "Custom software"), new("по понуда", "quoted"), new("бесплатна прва средба", "free first meeting")),
            new(new("Час работа (девелопер)", "Developer hour"), new("1.500 ден.", "1,500 MKD"), new("", "")),
        ]),
    ];

    // ── Контакт ──────────────────────────────────────────────────────────────
    public static readonly Loc ContactTitle = new("Да поразговараме", "Let's talk");
    public static readonly Loc ContactLead = new(
        "Опишете го проблемот или идејата — ќе ви одговорам во рок од 24 часа, најчесто и порано.",
        "Describe the problem or idea — I'll reply within 24 hours, usually sooner.");

    public static readonly Loc[] ContactTopics =
    [
        new("Сервис на компјутер", "Computer repair"),
        new("Мрежа / Wi‑Fi", "Network / Wi‑Fi"),
        new("Бекап и одржување", "Backup & maintenance"),
        new("Веб страна", "Website"),
        new("Готова програма", "Ready-made software"),
        new("Софтвер по мерка", "Custom software"),
        new("Друго", "Other"),
    ];
}
