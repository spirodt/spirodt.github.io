using Microsoft.JSInterop;
using SpiroPortfolio.Content;

namespace SpiroPortfolio.Services;

public enum Lang { Mk, En }

/// <summary>Двојазичен текст. Секој текст на страната е еден ваков запис.</summary>
public readonly record struct Loc(string Mk, string En)
{
    public string Get(Lang lang) => lang == Lang.En ? En : Mk;
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

    public Task ToggleAsync() => SetAsync(Current == Lang.Mk ? Lang.En : Lang.Mk);

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

    public static string Code(Lang lang) => lang == Lang.En ? "en" : "mk";

    private static Lang? Parse(string? value) => value?.Trim().ToLowerInvariant() switch
    {
        "en" => Lang.En,
        "mk" => Lang.Mk,
        _ => null
    };
}
