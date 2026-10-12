namespace SpiroPortfolio.Components;

/// <summary>
/// Илустрации за услугите (векторски SVG, во боите на брендот).
/// Клучот одговара на ServiceItem.Art во Content/SiteContent.cs.
/// Платно 320×180; позадината ја дава CSS-от (.svc-art[data-cat]).
/// </summary>
public static class ServiceArtLibrary
{
    private const string Open =
        """<svg viewBox="0 0 320 180" xmlns="http://www.w3.org/2000/svg" aria-hidden="true" focusable="false" preserveAspectRatio="xMidYMid meet"><circle cx="276" cy="26" r="48" fill="#fff" opacity=".45"/><circle cx="34" cy="160" r="32" fill="#fff" opacity=".35"/>""";

    private const string Close = "</svg>";

    public static string Get(string key) =>
        Open + (Art.TryGetValue(key, out var inner) ? inner : Art["software"]) + Close;

    private static readonly Dictionary<string, string> Art = new()
    {
        // ── Софтвер по мерка: прозорец со код + запченик
        ["software"] = """
            <rect x="56" y="26" width="200" height="128" rx="12" fill="#fff"/>
            <path d="M68 26h176a12 12 0 0 1 12 12v12H56V38a12 12 0 0 1 12-12z" fill="#0b1f4d"/>
            <circle cx="72" cy="38" r="3.5" fill="#f5a524"/><circle cx="84" cy="38" r="3.5" fill="#fff" opacity=".5"/><circle cx="96" cy="38" r="3.5" fill="#fff" opacity=".5"/>
            <rect x="74" y="66" width="60" height="7" rx="3.5" fill="#1e46c7"/><rect x="140" y="66" width="40" height="7" rx="3.5" fill="#c9d6fb"/>
            <rect x="88" y="82" width="80" height="7" rx="3.5" fill="#c9d6fb"/>
            <rect x="88" y="98" width="52" height="7" rx="3.5" fill="#f5a524"/><rect x="146" y="98" width="30" height="7" rx="3.5" fill="#c9d6fb"/>
            <rect x="88" y="114" width="70" height="7" rx="3.5" fill="#c9d6fb"/>
            <rect x="74" y="130" width="34" height="7" rx="3.5" fill="#1e46c7"/>
            <g transform="translate(244 134)">
              <g fill="#f5a524"><rect x="-5" y="-28" width="10" height="12" rx="2"/><rect x="-5" y="-28" width="10" height="12" rx="2" transform="rotate(45)"/><rect x="-5" y="-28" width="10" height="12" rx="2" transform="rotate(90)"/><rect x="-5" y="-28" width="10" height="12" rx="2" transform="rotate(135)"/><rect x="-5" y="-28" width="10" height="12" rx="2" transform="rotate(180)"/><rect x="-5" y="-28" width="10" height="12" rx="2" transform="rotate(225)"/><rect x="-5" y="-28" width="10" height="12" rx="2" transform="rotate(270)"/><rect x="-5" y="-28" width="10" height="12" rx="2" transform="rotate(315)"/></g>
              <circle r="20" fill="#f5a524"/><circle r="8" fill="#fff"/>
            </g>
            """,

        // ── Веб: прелистувач + телефон
        ["web"] = """
            <rect x="36" y="30" width="194" height="124" rx="12" fill="#fff"/>
            <path d="M48 30h170a12 12 0 0 1 12 12v8H36v-8a12 12 0 0 1 12-12z" fill="#c9d6fb"/>
            <rect x="70" y="36" width="110" height="8" rx="4" fill="#fff"/>
            <rect x="50" y="62" width="100" height="10" rx="5" fill="#0b1f4d"/>
            <rect x="50" y="78" width="80" height="6" rx="3" fill="#c9d6fb"/>
            <rect x="50" y="92" width="44" height="14" rx="7" fill="#1e46c7"/>
            <rect x="160" y="60" width="56" height="48" rx="8" fill="#f5a524"/>
            <path d="M166 100l14-16 10 10 8-8 12 14z" fill="#fff" opacity=".75"/><circle cx="200" cy="74" r="5" fill="#fff" opacity=".75"/>
            <rect x="50" y="118" width="50" height="24" rx="6" fill="#e4ebfa"/><rect x="106" y="118" width="50" height="24" rx="6" fill="#e4ebfa"/><rect x="162" y="118" width="54" height="24" rx="6" fill="#e4ebfa"/>
            <rect x="238" y="56" width="58" height="104" rx="12" fill="#0b1f4d"/>
            <rect x="244" y="66" width="46" height="80" rx="4" fill="#fff"/>
            <rect x="250" y="74" width="34" height="6" rx="3" fill="#0b1f4d"/><rect x="250" y="86" width="26" height="4" rx="2" fill="#c9d6fb"/>
            <rect x="250" y="96" width="34" height="22" rx="4" fill="#f5a524"/><rect x="250" y="124" width="22" height="8" rx="4" fill="#1e46c7"/>
            <rect x="259" y="150" width="16" height="3" rx="1.5" fill="#fff" opacity=".5"/>
            """,

        // ── Сервис на компјутери: лаптоп + клуч
        ["pc"] = """
            <rect x="74" y="32" width="164" height="102" rx="10" fill="#0b1f4d"/>
            <rect x="84" y="42" width="144" height="82" rx="4" fill="#1e46c7"/>
            <circle cx="156" cy="72" r="15" fill="#fff"/>
            <path d="M149 72l5 5 9-10" fill="none" stroke="#1e46c7" stroke-width="3.5" stroke-linecap="round" stroke-linejoin="round"/>
            <rect x="104" y="98" width="104" height="8" rx="4" fill="#fff" opacity=".3"/><rect x="104" y="98" width="72" height="8" rx="4" fill="#f5a524"/>
            <path d="M54 134h204l-12 16H66z" fill="#c9d6fb"/><rect x="136" y="134" width="40" height="5" rx="2.5" fill="#0b1f4d" opacity=".2"/>
            <g transform="translate(220 90) scale(2.5)"><path d="M14.7 6.3a4 4 0 0 0-5.4 5.2L3.5 17.3a1.8 1.8 0 0 0 2.5 2.5l5.8-5.8a4 4 0 0 0 5.2-5.4l-2.5 2.5-2.3-.3-.3-2.3z" fill="#f5a524" stroke="#0b1f4d" stroke-width=".9" stroke-linejoin="round"/></g>
            """,

        // ── Хардвер и електроника: плочка, чип и лемилица
        ["electronics"] = """
            <rect x="44" y="38" width="216" height="116" rx="12" fill="#0b1f4d"/>
            <path d="M64 62h40v24h30M64 132h56v-20h18M240 60h-36v28h-18M240 134h-30v-22h-24" fill="none" stroke="#f5a524" stroke-width="3" stroke-linecap="round" stroke-linejoin="round" opacity=".9"/>
            <g fill="#f5a524"><circle cx="64" cy="62" r="4.5"/><circle cx="64" cy="132" r="4.5"/><circle cx="240" cy="60" r="4.5"/><circle cx="240" cy="134" r="4.5"/></g>
            <g fill="#c9d6fb"><rect x="130" y="76" width="8" height="4" rx="1"/><rect x="130" y="86" width="8" height="4" rx="1"/><rect x="130" y="96" width="8" height="4" rx="1"/><rect x="130" y="106" width="8" height="4" rx="1"/><rect x="186" y="76" width="8" height="4" rx="1"/><rect x="186" y="86" width="8" height="4" rx="1"/><rect x="186" y="96" width="8" height="4" rx="1"/><rect x="186" y="106" width="8" height="4" rx="1"/></g>
            <rect x="138" y="70" width="48" height="46" rx="6" fill="#1e46c7"/>
            <rect x="148" y="87" width="28" height="12" rx="3" fill="#fff" opacity=".85"/>
            <rect x="84" y="94" width="14" height="22" rx="3" fill="#c9d6fb"/><rect x="88" y="90" width="6" height="4" fill="#c9d6fb"/>
            <rect x="206" y="98" width="22" height="12" rx="3" fill="#c9d6fb"/>
            <line x1="304" y1="10" x2="252" y2="48" stroke="#1e46c7" stroke-width="13" stroke-linecap="round"/>
            <line x1="252" y1="48" x2="246" y2="52" stroke="#f5a524" stroke-width="9" stroke-linecap="round"/>
            <line x1="246" y1="52" x2="226" y2="67" stroke="#c9d6fb" stroke-width="4" stroke-linecap="round"/>
            <circle cx="223" cy="69" r="5" fill="#f5a524"/>
            <path d="M212 62l-7-5M214 76l-8 4M226 80l2 8" stroke="#f5a524" stroke-width="2.5" stroke-linecap="round"/>
            """,

        // ── Мрежи: рутер и уреди
        ["network"] = """
            <g stroke="#1e46c7" stroke-width="2.5" stroke-dasharray="5 5" fill="none"><path d="M160 113L70 58M160 113L250 58M160 113L72 146M160 113L248 146"/></g>
            <circle cx="70" cy="58" r="24" fill="#fff"/><rect x="58" y="48" width="24" height="15" rx="2" fill="#0b1f4d"/><rect x="54" y="63" width="32" height="4" rx="2" fill="#0b1f4d"/>
            <circle cx="250" cy="58" r="24" fill="#fff"/><rect x="243" y="45" width="14" height="25" rx="3" fill="#0b1f4d"/><circle cx="250" cy="65" r="1.5" fill="#fff"/>
            <circle cx="72" cy="146" r="24" fill="#fff"/><rect x="60" y="136" width="24" height="16" rx="2" fill="#0b1f4d"/><rect x="69" y="152" width="6" height="5" fill="#0b1f4d"/>
            <circle cx="248" cy="146" r="24" fill="#fff"/><rect x="236" y="139" width="18" height="14" rx="3" fill="#0b1f4d"/><path d="M254 143l8-4v14l-8-4z" fill="#0b1f4d"/>
            <path d="M134 98V74M186 98V74" stroke="#0b1f4d" stroke-width="5" stroke-linecap="round"/>
            <rect x="122" y="98" width="76" height="30" rx="8" fill="#0b1f4d"/>
            <circle cx="140" cy="113" r="3" fill="#f5a524"/><circle cx="152" cy="113" r="3" fill="#22c55e"/><circle cx="164" cy="113" r="3" fill="#22c55e"/><rect x="174" y="111" width="14" height="4" rx="2" fill="#fff" opacity=".3"/>
            <g fill="none" stroke="#f5a524" stroke-width="4" stroke-linecap="round"><path d="M146 66a20 20 0 0 1 28 0"/><path d="M138 56a32 32 0 0 1 44 0"/></g>
            <circle cx="160" cy="74" r="4" fill="#f5a524"/>
            """,

        // ── Бекап: сервери + штит
        ["backup"] = """
            <g>
              <rect x="60" y="40" width="124" height="30" rx="7" fill="#0b1f4d"/><circle cx="78" cy="55" r="4" fill="#22c55e"/><rect x="92" y="52" width="60" height="6" rx="3" fill="#fff" opacity=".25"/><rect x="160" y="50" width="12" height="10" rx="2" fill="#fff" opacity=".15"/>
              <rect x="60" y="76" width="124" height="30" rx="7" fill="#0b1f4d"/><circle cx="78" cy="91" r="4" fill="#22c55e"/><rect x="92" y="88" width="60" height="6" rx="3" fill="#fff" opacity=".25"/><rect x="160" y="86" width="12" height="10" rx="2" fill="#fff" opacity=".15"/>
              <rect x="60" y="112" width="124" height="30" rx="7" fill="#1e46c7"/><circle cx="78" cy="127" r="4" fill="#f5a524"/><rect x="92" y="124" width="60" height="6" rx="3" fill="#fff" opacity=".35"/><rect x="160" y="122" width="12" height="10" rx="2" fill="#fff" opacity=".2"/>
            </g>
            <g transform="translate(192 46) scale(3.6)"><path d="M12 3l8 3v6c0 4.5-3.3 8.3-8 9-4.7-.7-8-4.5-8-9V6z" fill="#f5a524"/><polyline points="8.5 12 11 14.5 15.5 9.5" fill="none" stroke="#0b1f4d" stroke-width="1.8" stroke-linecap="round" stroke-linejoin="round"/></g>
            <path d="M206 146Q238 170 270 140" fill="none" stroke="#1e46c7" stroke-width="4" stroke-linecap="round"/>
            <path d="M274 136.5L268.8 147.8L262.2 140.3z" fill="#1e46c7"/>
            """,

        // ── AI: невронска мрежа + искри
        ["ai"] = """
            <g stroke="#c9d6fb" stroke-width="2">
              <path d="M70 56L140 44M70 56L140 80M70 56L140 116M70 56L140 152M70 92L140 44M70 92L140 80M70 92L140 116M70 92L140 152M70 128L140 44M70 128L140 80M70 128L140 116M70 128L140 152"/>
              <path d="M140 44L210 74M140 80L210 74M140 116L210 74M140 152L210 74M140 44L210 110M140 80L210 110M140 116L210 110M140 152L210 110"/>
            </g>
            <g fill="#1e46c7"><circle cx="70" cy="56" r="10"/><circle cx="70" cy="92" r="10"/><circle cx="70" cy="128" r="10"/></g>
            <g fill="#0b1f4d"><circle cx="140" cy="44" r="10"/><circle cx="140" cy="80" r="10"/><circle cx="140" cy="116" r="10"/><circle cx="140" cy="152" r="10"/></g>
            <g fill="#f5a524"><circle cx="210" cy="74" r="13"/><circle cx="210" cy="110" r="13"/></g>
            <path d="M262 30C264 46 268 50 284 52C268 54 264 58 262 74C260 58 256 54 240 52C256 50 260 46 262 30Z" fill="#f5a524"/>
            <path d="M288 84C289 91 291 93 298 94C291 95 289 97 288 104C287 97 285 95 278 94C285 93 287 91 288 84Z" fill="#1e46c7"/>
            <rect x="236" y="122" width="62" height="28" rx="14" fill="#0b1f4d"/>
            <text x="267" y="141" text-anchor="middle" font-family="Manrope, system-ui, sans-serif" font-weight="800" font-size="15" fill="#fff">AI</text>
            """,

        // ── Консултации: табла со графикон + разговор
        ["consulting"] = """
            <rect x="34" y="30" width="152" height="104" rx="10" fill="#fff"/>
            <path d="M70 134l-14 30M150 134l14 30" stroke="#0b1f4d" stroke-width="5" stroke-linecap="round"/>
            <rect x="52" y="96" width="18" height="24" rx="3" fill="#c9d6fb"/><rect x="78" y="80" width="18" height="40" rx="3" fill="#c9d6fb"/><rect x="104" y="66" width="18" height="54" rx="3" fill="#1e46c7"/><rect x="130" y="54" width="18" height="66" rx="3" fill="#0b1f4d"/>
            <path d="M56 82l26-14 26 6 34-28" fill="none" stroke="#f5a524" stroke-width="4" stroke-linecap="round" stroke-linejoin="round"/>
            <circle cx="142" cy="46" r="5" fill="#f5a524"/>
            <path d="M206 40h70a10 10 0 0 1 10 10v30a10 10 0 0 1-10 10h-46l-14 12v-12h-10a10 10 0 0 1-10-10v-30a10 10 0 0 1 10-10z" fill="#0b1f4d"/>
            <rect x="214" y="54" width="54" height="6" rx="3" fill="#fff" opacity=".8"/><rect x="214" y="68" width="34" height="6" rx="3" fill="#f5a524"/>
            <path d="M220 112h56a10 10 0 0 1 10 10v22a10 10 0 0 1-10 10h-10v10l-12-10h-34a10 10 0 0 1-10-10v-22a10 10 0 0 1 10-10z" fill="#f5a524"/>
            <path d="M234 126l-6 7 6 7M262 126l6 7-6 7M252 124l-8 18" fill="none" stroke="#0b1f4d" stroke-width="3" stroke-linecap="round" stroke-linejoin="round"/>
            """,

        // ── План и естимации: гант + календар
        ["planning"] = """
            <rect x="36" y="30" width="194" height="122" rx="12" fill="#fff"/>
            <g stroke="#e4e8f0" stroke-width="1.5"><path d="M78 42v100M118 42v100M158 42v100M198 42v100"/></g>
            <rect x="50" y="50" width="62" height="14" rx="7" fill="#1e46c7"/>
            <rect x="90" y="72" width="72" height="14" rx="7" fill="#c9d6fb"/>
            <rect x="128" y="94" width="58" height="14" rx="7" fill="#f5a524"/>
            <rect x="160" y="116" width="58" height="14" rx="7" fill="#0b1f4d"/>
            <path d="M210 48l9 9-9 9-9-9z" fill="#f5a524"/>
            <path d="M232 72v12M268 72v12" stroke="#0b1f4d" stroke-width="5" stroke-linecap="round"/>
            <rect x="212" y="78" width="76" height="76" rx="10" fill="#0b1f4d"/>
            <path d="M222 78h56a10 10 0 0 1 10 10v10h-76v-10a10 10 0 0 1 10-10z" fill="#f5a524"/>
            <g fill="#fff" opacity=".35"><rect x="224" y="106" width="12" height="9" rx="2"/><rect x="244" y="106" width="12" height="9" rx="2"/><rect x="264" y="106" width="12" height="9" rx="2"/><rect x="224" y="121" width="12" height="9" rx="2"/><rect x="264" y="121" width="12" height="9" rx="2"/><rect x="224" y="136" width="12" height="9" rx="2"/><rect x="244" y="136" width="12" height="9" rx="2"/><rect x="264" y="136" width="12" height="9" rx="2"/></g>
            <rect x="244" y="121" width="12" height="9" rx="2" fill="#f5a524"/>
            """,

        // ── DevOps: бесконечна јамка
        ["devops"] = """
            <path d="M160 88C140 56 82 56 82 88C82 120 140 120 160 88" fill="none" stroke="#1e46c7" stroke-width="20" stroke-linecap="round"/>
            <path d="M160 88C180 56 238 56 238 88C238 120 180 120 160 88" fill="none" stroke="#f5a524" stroke-width="20" stroke-linecap="round"/>
            <path d="M147 70l10 4-4 10" fill="none" stroke="#fff" stroke-width="3" stroke-linecap="round" stroke-linejoin="round"/>
            <path d="M173 106l-10-4 4-10" fill="none" stroke="#fff" stroke-width="3" stroke-linecap="round" stroke-linejoin="round"/>
            <text x="114" y="94" text-anchor="middle" font-family="Manrope, system-ui, sans-serif" font-weight="800" font-size="15" fill="#0b1f4d">DEV</text>
            <text x="206" y="94" text-anchor="middle" font-family="Manrope, system-ui, sans-serif" font-weight="800" font-size="15" fill="#0b1f4d">OPS</text>
            <g font-family="Manrope, system-ui, sans-serif" font-weight="700" font-size="11" fill="#0b1f4d">
              <rect x="40" y="140" width="56" height="22" rx="11" fill="#fff"/><text x="68" y="155" text-anchor="middle">build</text>
              <rect x="102" y="140" width="50" height="22" rx="11" fill="#fff"/><text x="127" y="155" text-anchor="middle">test</text>
              <rect x="158" y="140" width="62" height="22" rx="11" fill="#fff"/><text x="189" y="155" text-anchor="middle">deploy</text>
              <rect x="226" y="140" width="56" height="22" rx="11" fill="#0b1f4d"/><text x="254" y="155" text-anchor="middle" fill="#fff">monitor</text>
            </g>
            """,

        // ── Release менаџмент: верзии на временска линија + hotfix гранка
        ["release"] = """
            <path d="M40 116H280" stroke="#c9d6fb" stroke-width="6" stroke-linecap="round"/>
            <path d="M40 116H200" stroke="#1e46c7" stroke-width="6" stroke-linecap="round"/>
            <path d="M135 116C150 148 185 148 200 116" fill="none" stroke="#f5a524" stroke-width="3" stroke-dasharray="5 5"/>
            <circle cx="167" cy="139" r="6" fill="#f5a524"/>
            <g font-family="Manrope, system-ui, sans-serif" font-weight="800" font-size="12" text-anchor="middle">
              <rect x="44" y="64" width="52" height="24" rx="7" fill="#0b1f4d"/><text x="70" y="80" fill="#fff">v1.0</text>
              <rect x="109" y="64" width="52" height="24" rx="7" fill="#0b1f4d"/><text x="135" y="80" fill="#fff">v1.1</text>
              <rect x="174" y="64" width="52" height="24" rx="7" fill="#0b1f4d"/><text x="200" y="80" fill="#fff">v1.2</text>
              <rect x="239" y="64" width="52" height="24" rx="7" fill="#f5a524"/><text x="265" y="80" fill="#0b1f4d">v2.0</text>
            </g>
            <g stroke-width="2"><path d="M70 88v18M135 88v18M200 88v18" stroke="#0b1f4d"/><path d="M265 88v18" stroke="#f5a524"/></g>
            <g fill="#1e46c7"><circle cx="70" cy="116" r="10"/><circle cx="135" cy="116" r="10"/><circle cx="200" cy="116" r="10"/></g>
            <g fill="#fff"><circle cx="70" cy="116" r="4"/><circle cx="135" cy="116" r="4"/><circle cx="200" cy="116" r="4"/></g>
            <circle cx="265" cy="116" r="10" fill="#fff" stroke="#f5a524" stroke-width="4"/>
            <path d="M265 30v26" stroke="#0b1f4d" stroke-width="3" stroke-linecap="round"/>
            <path d="M266 31h22l-6 7 6 7h-22z" fill="#f5a524"/>
            """,

        // ── Менаџирање на cloud: облак + сервери
        ["cloud"] = """
            <circle cx="72" cy="52" r="6" fill="#f5a524" opacity=".8"/><circle cx="252" cy="44" r="10" fill="#f5a524"/>
            <g fill="#1e46c7"><circle cx="124" cy="78" r="26"/><circle cx="160" cy="62" r="34"/><circle cx="198" cy="80" r="24"/><rect x="100" y="78" width="120" height="26" rx="13"/></g>
            <path d="M146 82V60m-8 8l8-8 8 8M174 60v22m-8-8l8 8 8-8" fill="none" stroke="#fff" stroke-width="4" stroke-linecap="round" stroke-linejoin="round"/>
            <path d="M110 104v20M160 104v20M210 104v20" stroke="#1e46c7" stroke-width="3"/>
            <g>
              <rect x="86" y="124" width="48" height="38" rx="6" fill="#0b1f4d"/><circle cx="96" cy="134" r="3" fill="#22c55e"/><rect x="104" y="132" width="22" height="4" rx="2" fill="#fff" opacity=".3"/><circle cx="96" cy="150" r="3" fill="#22c55e"/><rect x="104" y="148" width="22" height="4" rx="2" fill="#fff" opacity=".3"/>
              <rect x="136" y="124" width="48" height="38" rx="6" fill="#0b1f4d"/><circle cx="146" cy="134" r="3" fill="#22c55e"/><rect x="154" y="132" width="22" height="4" rx="2" fill="#fff" opacity=".3"/><circle cx="146" cy="150" r="3" fill="#f5a524"/><rect x="154" y="148" width="22" height="4" rx="2" fill="#fff" opacity=".3"/>
              <rect x="186" y="124" width="48" height="38" rx="6" fill="#0b1f4d"/><circle cx="196" cy="134" r="3" fill="#22c55e"/><rect x="204" y="132" width="22" height="4" rx="2" fill="#fff" opacity=".3"/><circle cx="196" cy="150" r="3" fill="#22c55e"/><rect x="204" y="148" width="22" height="4" rx="2" fill="#fff" opacity=".3"/>
            </g>
            """,

        // ── Cloud трошоци: облак, опаѓачки графикон, монети
        ["cloudcost"] = """
            <g fill="#c9d6fb"><circle cx="100" cy="62" r="20"/><circle cx="128" cy="50" r="26"/><circle cx="156" cy="64" r="18"/><rect x="80" y="62" width="94" height="20" rx="10"/></g>
            <rect x="56" y="94" width="148" height="66" rx="10" fill="#fff"/>
            <g stroke="#e4e8f0" stroke-width="1.5"><path d="M68 112h124M68 130h124M68 148h124"/></g>
            <path d="M70 110L100 118L126 114L152 134L182 142" fill="none" stroke="#1e46c7" stroke-width="4" stroke-linecap="round" stroke-linejoin="round"/>
            <path d="M189.9 143.9L178.9 146.2L181.5 136.6z" fill="#1e46c7"/>
            <g>
              <rect x="216" y="132" width="60" height="12" fill="#d48a12"/><ellipse cx="246" cy="144" rx="30" ry="8" fill="#d48a12"/><ellipse cx="246" cy="132" rx="30" ry="8" fill="#f5a524"/>
              <rect x="216" y="118" width="60" height="14" fill="#d48a12"/><ellipse cx="246" cy="118" rx="30" ry="8" fill="#f5a524"/>
              <rect x="216" y="104" width="60" height="14" fill="#d48a12"/><ellipse cx="246" cy="104" rx="30" ry="8" fill="#f5a524"/>
              <ellipse cx="246" cy="104" rx="18" ry="4" fill="none" stroke="#d48a12" stroke-width="2"/>
            </g>
            <circle cx="262" cy="56" r="22" fill="#0b1f4d"/>
            <path d="M262 45v22m-8-8l8 8 8-8" fill="none" stroke="#fff" stroke-width="4" stroke-linecap="round" stroke-linejoin="round"/>
            """,
    };
}
