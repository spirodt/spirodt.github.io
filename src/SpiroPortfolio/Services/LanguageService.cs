using Microsoft.JSInterop;
using SpiroPortfolio.Content;

namespace SpiroPortfolio.Services;

public enum Lang { Mk, En, De, Sq }

/// <summary>Јазиците на страната, по редослед во изборникот.</summary>
public static class Languages
{
    public static readonly (Lang Lang, string Code, string Short, string Native)[] All =
    [
        (Lang.Mk, "mk", "МК", "Македонски"),
        (Lang.En, "en", "EN", "English"),
        (Lang.De, "de", "DE", "Deutsch"),
        (Lang.Sq, "sq", "SQ", "Shqip"),
    ];
}

/// <summary>
/// Текст на страната. Се пишува македонски + англиски; германскиот и албанскиот
/// се бараат по англискиот во Content/Translations.g.cs (од translations.tsv).
/// </summary>
public readonly record struct Loc(string Mk, string En)
{
    public string Get(Lang lang) => lang switch
    {
        Lang.Mk => Mk,
        Lang.En => En,
        Lang.De => Translations.ByEnglish.TryGetValue(En, out var t) ? t.De : En,
        Lang.Sq => Translations.ByEnglish.TryGetValue(En, out var t) ? t.Sq : En,
        _ => Mk
    };
}

/// <summary>
/// Го чува избраниот јазик (localStorage + ?lang= во URL) и ги известува компонентите при промена.
/// </summary>
public sealed class LanguageService(IJSRuntime js)
{
    private const string StorageKey = "lang";

    public Lang Current { get; private set; } = Lang.Mk;

    public event Action? Changed;

    public async Task InitializeAsync(string? queryLang)
    {
        var fromQuery = Parse(queryLang);
        if (fromQuery is not null)
        {
            Current = fromQuery.Value;
        }
        else
        {
            try
            {
                var stored = await js.InvokeAsync<string?>("portfolio.getItem", StorageKey);
                Current = Parse(stored) ?? Lang.Mk;
            }
            catch (JSException)
            {
                Current = Lang.Mk;
            }
        }

        await ApplyAsync();
    }

    public async Task SetAsync(Lang lang)
    {
        if (lang == Current) return;
        Current = lang;
        await ApplyAsync();
        Changed?.Invoke();
    }


    private async Task ApplyAsync()
    {
        try
        {
            await js.InvokeVoidAsync("portfolio.setItem", StorageKey, Code(Current));
            await js.InvokeVoidAsync("portfolio.setDocumentLanguage", Code(Current), SiteContent.MetaTitle.Get(Current));
        }
        catch (JSException)
        {
            // localStorage може да е блокиран (приватен прозорец) — страната работи и без него.
        }
    }

    public static string Code(Lang lang) => Languages.All.First(l => l.Lang == lang).Code;

    private static Lang? Parse(string? value) => value?.Trim().ToLowerInvariant() switch
    {
        "en" => Lang.En,
        "mk" => Lang.Mk,
        "de" => Lang.De,
        "sq" or "al" => Lang.Sq,
        _ => null
    };
}
