using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.JSInterop;

namespace SpiroPortfolio.Services;

/// <summary>Една посета: од каде дошол посетителот (го запишува скриптата во index.html).</summary>
public sealed record Touch(
    [property: JsonPropertyName("ref")] string? Ref,
    [property: JsonPropertyName("source")] string? Source,
    [property: JsonPropertyName("medium")] string? Medium,
    [property: JsonPropertyName("campaign")] string? Campaign,
    [property: JsonPropertyName("referrer")] string? Referrer,
    [property: JsonPropertyName("landing")] string? Landing,
    [property: JsonPropertyName("at")] string? At);

/// <summary>Личен линк за препорака што посетителот го направил на /refer.</summary>
public sealed record MyReferral(
    [property: JsonPropertyName("code")] string Code,
    [property: JsonPropertyName("name")] string Name,
    [property: JsonPropertyName("phone")] string? Phone,
    [property: JsonPropertyName("created")] string Created);

public sealed record Attribution(Touch? First, Touch? Last, string? ReferredBy, int Visits)
{
    /// <summary>Мрежа/страна по човечки: l.facebook.com → Facebook.</summary>
    public static string Friendly(string? source)
    {
        if (string.IsNullOrWhiteSpace(source) || source == "direct") return "Директно / Direct";
        var s = source.ToLowerInvariant();
        if (s.Contains("facebook") || s == "fb") return "Facebook";
        if (s.Contains("instagram") || s == "ig") return "Instagram";
        if (s.Contains("linkedin") || s.Contains("lnkd.in")) return "LinkedIn";
        if (s.Contains("whatsapp")) return "WhatsApp";
        if (s.Contains("viber")) return "Viber";
        if (s.Contains("google")) return "Google";
        if (s.Contains("bing")) return "Bing";
        if (s is "email" or "mail") return "Е-пошта / Email";
        if (s is "qr" or "flyer") return "Флаер (QR) / Flyer";
        return source;
    }

    /// <summary>Текст што оди со пораката од контакт формата.</summary>
    public string Summary()
    {
        var sb = new StringBuilder();
        if (!string.IsNullOrEmpty(ReferredBy)) sb.Append($"Препорака (ref): {ReferredBy}\n");
        if (First is not null)
            sb.Append($"Прва посета: {Friendly(First.Source)}{(First.Campaign is null ? "" : $" / {First.Campaign}")} · {Short(First.At)}\n");
        if (Last is not null && Last.At != First?.At)
            sb.Append($"Последен извор: {Friendly(Last.Source)}{(Last.Campaign is null ? "" : $" / {Last.Campaign}")} · {Short(Last.At)}\n");
        if (Visits > 0) sb.Append($"Број на посети: {Visits}\n");
        return sb.ToString().TrimEnd();
    }

    private static string Short(string? iso) =>
        DateTimeOffset.TryParse(iso, out var d) ? d.ToLocalTime().ToString("dd.MM.yyyy") : "";
}

/// <summary>
/// Чита/пишува податоци за препораки во localStorage. Ништо не се праќа никаде,
/// освен кога посетителот сам ќе испрати порака преку контакт формата.
/// </summary>
public sealed class AttributionService(IJSRuntime js)
{
    private static readonly JsonSerializerOptions Json = new(JsonSerializerDefaults.Web);

    public async Task<Attribution> GetAsync()
    {
        var first = Parse<Touch>(await Get("bss_first_touch"));
        var last = Parse<Touch>(await Get("bss_last_touch"));
        var refBy = await Get("bss_referred_by");
        int.TryParse(await Get("bss_visits"), out var visits);
        return new Attribution(first, last, string.IsNullOrWhiteSpace(refBy) ? null : refBy, visits);
    }

    public async Task<MyReferral?> GetMyReferralAsync() => Parse<MyReferral>(await Get("bss_my_ref"));

    public async Task<MyReferral> CreateMyReferralAsync(string name, string? phone)
    {
        var code = $"{Slug(name)}-{RandomSuffix()}";
        var mine = new MyReferral(code, name.Trim(), string.IsNullOrWhiteSpace(phone) ? null : phone.Trim(),
            DateTimeOffset.UtcNow.ToString("o"));
        await Set("bss_my_ref", JsonSerializer.Serialize(mine, Json));
        return mine;
    }

    public Task ForgetMyReferralAsync() => Set("bss_my_ref", "");

    // ── помошни
    private async Task<string?> Get(string key)
    {
        try { return await js.InvokeAsync<string?>("portfolio.getItem", key); }
        catch (JSException) { return null; }
    }

    private async Task Set(string key, string value)
    {
        try { await js.InvokeVoidAsync("portfolio.setItem", key, value); }
        catch (JSException) { }
    }

    private static T? Parse<T>(string? json) where T : class
    {
        if (string.IsNullOrWhiteSpace(json)) return null;
        try { return JsonSerializer.Deserialize<T>(json, Json); }
        catch (JsonException) { return null; }
    }

    private static readonly Dictionary<char, string> Cyr = new()
    {
        ['а'] = "a", ['б'] = "b", ['в'] = "v", ['г'] = "g", ['д'] = "d", ['ѓ'] = "gj", ['е'] = "e", ['ж'] = "zh",
        ['з'] = "z", ['ѕ'] = "dz", ['и'] = "i", ['ј'] = "j", ['к'] = "k", ['л'] = "l", ['љ'] = "lj", ['м'] = "m",
        ['н'] = "n", ['њ'] = "nj", ['о'] = "o", ['п'] = "p", ['р'] = "r", ['с'] = "s", ['т'] = "t", ['ќ'] = "kj",
        ['у'] = "u", ['ф'] = "f", ['х'] = "h", ['ц'] = "c", ['ч'] = "ch", ['џ'] = "dj", ['ш'] = "sh",
    };

    /// <summary>„Марко Петров“ → „marko-petrov“ (латиница, за линкот).</summary>
    public static string Slug(string name)
    {
        var sb = new StringBuilder();
        foreach (var raw in name.Trim().ToLowerInvariant())
        {
            if (Cyr.TryGetValue(raw, out var lat)) sb.Append(lat);
            else if (raw is >= 'a' and <= 'z' or >= '0' and <= '9') sb.Append(raw);
            else if (sb.Length > 0 && sb[^1] != '-') sb.Append('-');
        }
        var slug = sb.ToString().Trim('-');
        if (slug.Length > 24) slug = slug[..24].TrimEnd('-');
        return slug.Length == 0 ? "friend" : slug;
    }

    private static string RandomSuffix()
    {
        const string chars = "abcdefghjkmnpqrstuvwxyz23456789";
        return new string(Enumerable.Range(0, 4).Select(_ => chars[Random.Shared.Next(chars.Length)]).ToArray());
    }
}
