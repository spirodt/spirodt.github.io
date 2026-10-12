using System.Net.Http.Json;
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
/// Blazor WebAssembly е статична страна (нема сервер), па пораките се праќаат преку
/// бесплатен сервис за форми (Formspree). Ако „FormEndpoint“ во wwwroot/appsettings.json
/// е празен, се отвора е-пошта програмата на посетителот со веќе пополнета порака.
/// </summary>
public sealed class ContactService(IConfiguration config)
{
    private static readonly HttpClient External = new();

    public string? Endpoint => string.IsNullOrWhiteSpace(config["FormEndpoint"]) ? null : config["FormEndpoint"];

    public async Task<SendResult> SendAsync(ContactRequest request)
    {
        if (!string.IsNullOrEmpty(request.Website)) return SendResult.Sent; // бот — тивко игнорирај

        if (Endpoint is null) return SendResult.OpenedMailClient;

        try
        {
            using var message = new HttpRequestMessage(HttpMethod.Post, Endpoint)
            {
                Content = JsonContent.Create(new
                {
                    name = request.Name,
                    email = request.Email,
                    phone = request.Phone,
                    topic = request.Topic,
                    message = request.Message,
                    _subject = $"Барање од веб: {request.Topic}"
                })
            };
            message.Headers.Accept.ParseAdd("application/json");
            var response = await External.SendAsync(message);
            return response.IsSuccessStatusCode ? SendResult.Sent : SendResult.Failed;
        }
        catch (HttpRequestException)
        {
            return SendResult.Failed;
        }
    }

    /// <summary>
    /// Резервен начин кога нема Formspree: ја отвора е-пошта програмата на посетителот
    /// со пополнета порака до главната адреса, со копија до другите две.
    /// </summary>
    public static string FallbackLink(ContactRequest r)
    {
        var c = SiteContent.Contact;
        var text = $"{r.Message}\n\n—\n{r.Name}\n{r.Phone}\n{r.Email}";
        var subject = Uri.EscapeDataString($"Барање од веб: {r.Topic}");
        var cc = string.Join(",", c.Emails.Skip(1));
        return $"mailto:{c.PrimaryEmail}?cc={cc}&subject={subject}&body={Uri.EscapeDataString(text)}";
    }
}
