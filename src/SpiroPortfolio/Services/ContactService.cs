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
    /// Резервен начин кога нема Formspree: е-пошта ако е поставена, инаку WhatsApp со пополнета порака.
    /// </summary>
    public static string FallbackLink(ContactRequest r)
    {
        var text = $"Барање од веб: {r.Topic}\n\n{r.Message}\n\n—\n{r.Name}\n{r.Phone}\n{r.Email}";
        if (!SiteContent.Contact.HasEmail)
            return $"{SiteContent.Contact.WhatsAppHref}?text={Uri.EscapeDataString(text)}";

        var subject = Uri.EscapeDataString($"Барање од веб: {r.Topic}");
        return $"mailto:{SiteContent.Contact.Email}?subject={subject}&body={Uri.EscapeDataString(text)}";
    }
}
