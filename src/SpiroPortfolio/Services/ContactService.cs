using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using SpiroPortfolio.Content;

namespace SpiroPortfolio.Services;

public sealed class ContactRequest
{
    public string Name { get; set; } = "";
    public string Email { get; set; } = "";
    public string Phone { get; set; } = "";
    public string Topic { get; set; } = "";
    public string Message { get; set; } = "";
    /// <summary>Скриено поле против спам-ботови. Луѓето не го гледаат.</summary>
    public string Website { get; set; } = "";
}

public enum SendResult { Sent, OpenedMailClient, Failed }

/// <summary>
/// Праќање на пораки од статичната страна (нема сервер):
///  1. Web3Forms  — wwwroot/appsettings.json → "Web3FormsKey" (бесплатно до 250 пораки/месец)
///  2. Formspree  — "FormEndpoint"
///  3. Ништо од горе → се отвора е-пошта програмата на посетителот (mailto).
/// Со секоја порака оди и од каде дошол посетителот (препорака, Facebook, Google…).
/// </summary>
public sealed class ContactService(IConfiguration config, AttributionService attribution)
{
    private static readonly HttpClient External = new();

    private string? Web3FormsKey => Clean(config["Web3FormsKey"]);
    private string? FormspreeEndpoint => Clean(config["FormEndpoint"]);

    /// <summary>Дали пораките се праќаат директно (без е-пошта програма кај посетителот).</summary>
    public bool CanSendDirectly => Web3FormsKey is not null || FormspreeEndpoint is not null;

    public async Task<SendResult> SendAsync(ContactRequest request)
    {
        if (!string.IsNullOrEmpty(request.Website)) return SendResult.Sent; // бот — тивко игнорирај
        if (!CanSendDirectly) return SendResult.OpenedMailClient;

        var source = (await attribution.GetAsync()).Summary();
        var subject = $"[BSS] {request.Topic} — {request.Name}";
        var fields = new Dictionary<string, string?>
        {
            ["name"] = request.Name,
            ["email"] = string.IsNullOrWhiteSpace(request.Email) ? null : request.Email.Trim(),
            ["phone"] = request.Phone,
            ["topic"] = request.Topic,
            ["message"] = request.Message,
            ["source"] = string.IsNullOrEmpty(source) ? "—" : source,
        };
        return await PostAsync(subject, fields, request.Email) ? SendResult.Sent : SendResult.Failed;
    }

    /// <summary>Кога некој ќе направи линк за препорака — за да знаеш кој код е чиј.</summary>
    public async Task NotifyNewReferrerAsync(MyReferral mine)
    {
        if (!CanSendDirectly) return;
        try
        {
            await PostAsync($"[BSS] Нов линк за препорака: {mine.Name}", new Dictionary<string, string?>
            {
                ["name"] = mine.Name,
                ["phone"] = mine.Phone ?? "—",
                ["ref_code"] = mine.Code,
                ["message"] = $"{mine.Name} направи линк за препорака со код „{mine.Code}“."
            }, null);
        }
        catch { /* не е важно ако не успее */ }
    }

    private async Task<bool> PostAsync(string subject, Dictionary<string, string?> fields, string? replyTo)
    {
        try
        {
            HttpRequestMessage message;
            if (Web3FormsKey is { } key)
            {
                var body = new Dictionary<string, object?>(fields.Where(f => f.Value is not null).ToDictionary(f => f.Key, f => (object?)f.Value))
                {
                    ["access_key"] = key,
                    ["subject"] = subject,
                    ["from_name"] = "BSS веб страна",
                    ["botcheck"] = false,
                };
                if (IsEmail(replyTo)) body["replyto"] = replyTo!.Trim();
                message = new HttpRequestMessage(HttpMethod.Post, "https://api.web3forms.com/submit") { Content = JsonContent.Create(body) };
            }
            else
            {
                var body = fields.Where(f => f.Value is not null).ToDictionary(f => f.Key, f => (object?)f.Value);
                body["_subject"] = subject;
                if (IsEmail(replyTo)) body["_replyto"] = replyTo!.Trim();
                message = new HttpRequestMessage(HttpMethod.Post, FormspreeEndpoint) { Content = JsonContent.Create(body) };
            }

            message.Headers.Accept.ParseAdd("application/json");
            using var response = await External.SendAsync(message);
            if (!response.IsSuccessStatusCode) return false;

            // Web3Forms враќа {"success": true/false}
            if (Web3FormsKey is not null)
            {
                using var doc = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
                return doc.RootElement.TryGetProperty("success", out var ok) && ok.ValueKind == JsonValueKind.True;
            }
            return true;
        }
        catch (Exception ex) when (ex is HttpRequestException or JsonException or TaskCanceledException)
        {
            return false;
        }
    }

    /// <summary>
    /// mailto: до главната адреса, со копија до другите две — за копчето „Испрати преку е-пошта“
    /// и како резерва кога нема поставено сервис.
    /// </summary>
    public async Task<string> MailtoLinkAsync(ContactRequest r)
    {
        var c = SiteContent.Contact;
        var source = (await attribution.GetAsync()).Summary();
        var text = $"{r.Message}\n\n—\n{r.Name}\n{r.Phone}\n{r.Email}" + (string.IsNullOrEmpty(source) ? "" : $"\n\n{source}");
        var subject = Uri.EscapeDataString($"[BSS] {(string.IsNullOrWhiteSpace(r.Topic) ? "Барање од веб" : r.Topic)}");
        var cc = string.Join(",", c.Emails.Skip(1));
        return $"mailto:{c.PrimaryEmail}?cc={cc}&subject={subject}&body={Uri.EscapeDataString(text)}";
    }

    private static bool IsEmail(string? s) =>
        !string.IsNullOrWhiteSpace(s) && s.Contains('@') && s.Contains('.') && !s.Contains(' ');

    private static string? Clean(string? s) => string.IsNullOrWhiteSpace(s) ? null : s.Trim();
}
