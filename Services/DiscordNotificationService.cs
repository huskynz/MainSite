using System.Net.Http.Json;
using System.Runtime.ConstrainedExecution;

namespace _12.Services;

public class DiscordNotificationService
{
    private readonly HttpClient _httpClient;
    private readonly string? _webhookUrl;
    private readonly ILogger<DiscordNotificationService> _logger;
    public DiscordNotificationService(HttpClient httpClient, IConfiguration configuration, ILogger<DiscordNotificationService> logger)
    {
        _httpClient = httpClient;
        _webhookUrl = configuration["Discord:ContactWebhookUrl"];
        _logger = logger;
    }

    public async Task<bool> SendContactMessageAsync(string name, string email, string message, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(_webhookUrl))
        {
            _logger.LogWarning("Discord contact webhook URL is not configured; skipping notification.");
            return false;
        }

        var payload = new
        {
            embeds = new[]
            {
                new
                {
                    title = "New contact form submission",
                    color = 0x8B5CF6,
                    fields = new[]
                    {
                        new { name = "Site Version", value = "v12.0.0", inline = true},
                        new { name = "Name", value = Truncate(name, 256), inline = true },
                        new { name = "Email", value = Truncate(email, 256), inline = true },
                        new { name = "Message", value = Truncate(message, 1024), inline = false },
                    },
                    timestamp = DateTime.UtcNow.ToString("o"),
                },
            },
        };

        try
        {
            var response = await _httpClient.PostAsJsonAsync(_webhookUrl, payload, cancellationToken);
            response.EnsureSuccessStatusCode();
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to send Discord contact form notification.");
            return false;
        }
    }

    private static string Truncate(string value, int maxLength)
    {
        return value.Length <= maxLength ? value : value[..maxLength];
    }
}
