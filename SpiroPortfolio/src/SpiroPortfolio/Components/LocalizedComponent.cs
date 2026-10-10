using Microsoft.AspNetCore.Components;
using SpiroPortfolio.Services;

namespace SpiroPortfolio.Components;

/// <summary>База за секции: го прима јазикот од MainLayout и дава кратко T(...) за превод.</summary>
public abstract class LocalizedComponent : ComponentBase
{
    [CascadingParameter] public Lang Lang { get; set; }

    protected string T(Loc text) => text.Get(Lang);

    protected string T(string mk, string en) => Lang == Lang.En ? en : mk;

    /// <summary>ARIA атрибутите бараат "true"/"false" како текст, не HTML boolean.</summary>
    protected static string Aria(bool value) => value ? "true" : "false";
}
