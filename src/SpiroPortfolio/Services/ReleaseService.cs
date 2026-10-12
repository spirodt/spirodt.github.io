using System.Net.Http.Json;
using System.Text.Json.Serialization;

namespace SpiroPortfolio.Services;

public sealed record ReleaseFile(
    [property: JsonPropertyName("name")] string Name,
    [property: JsonPropertyName("url")] string Url,
    [property: JsonPropertyName("version")] string? Version,
    [property: JsonPropertyName("date")] string Date,
    [property: JsonPropertyName("sizeKb")] long SizeKb,
    [property: JsonPropertyName("latest")] bool Latest)
{
    public string SizeMb => $"{SizeKb / 1024.0:0.0} MB";

    /// <summary>„2026-10-12 07:04“ → „12.10.2026“.</summary>
    public string DisplayDate =>
        DateTime.TryParse(Date, System.Globalization.CultureInfo.InvariantCulture,
            System.Globalization.DateTimeStyles.None, out var d) ? d.ToString("dd.MM.yyyy") : Date;
}

public sealed record ReleaseFeed(
    [property: JsonPropertyName("source")] string Source,
    [property: JsonPropertyName("updated")] string Updated,
    [property: JsonPropertyName("files")] ReleaseFile[] Files);

/// <summary>
/// Ги чита инсталациите на BSSR од wwwroot/data/bssr-releases.json.
/// Фајлот го освежува GitHub Actions (.github/scripts/update_bssr_releases.py) при секое објавување и секој ден.
/// </summary>
public sealed class ReleaseService(HttpClient http)
{
    public const string SourceUrl = "https://bssr.mk/bss/Installer/BSSR7/";

    private ReleaseFeed? _cache;

    public async Task<ReleaseFeed> GetAsync()
    {
        if (_cache is not null) return _cache;
        try
        {
            _cache = await http.GetFromJsonAsync<ReleaseFeed>("data/bssr-releases.json") ?? Fallback;
        }
        catch (Exception)
        {
            _cache = Fallback;
        }
        return _cache;
    }

    // Резерва ако JSON-от не може да се вчита — барем најновата инсталација.
    private static ReleaseFeed Fallback => new(SourceUrl, "", [
        new("BSS.exe", SourceUrl + "BSS.exe", null, "", 0, true)
    ]);
}
