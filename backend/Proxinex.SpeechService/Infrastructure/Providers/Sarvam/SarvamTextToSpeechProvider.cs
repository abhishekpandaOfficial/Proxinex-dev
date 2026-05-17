using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Options;
using Proxinex.SpeechService.Application.Interfaces;
using Proxinex.SpeechService.Configuration;

namespace Proxinex.SpeechService.Infrastructure.Providers.Sarvam;

public class SarvamTextToSpeechProvider
    : ITextToSpeechProvider
{
    private readonly HttpClient _httpClient;

    private readonly SarvamOptions _options;

    public SarvamTextToSpeechProvider(
        HttpClient httpClient,
        IOptions<SarvamOptions> options)
    {
        _httpClient = httpClient;

        _options = options.Value;
    }

    public async Task<byte[]> GenerateSpeechAsync(
        string text)
    {
        var payload = new
        {
            inputs = new[]
            {
                text
            },

            target_language_code = "en-IN",

            speaker = "rahul",

            pace = 1.0,

            speech_sample_rate = 22050,

            enable_preprocessing = true,

            model = "bulbul:v3"
        };

        var request =
            new HttpRequestMessage(
                HttpMethod.Post,
                "https://api.sarvam.ai/text-to-speech");

        request.Headers.Add(
            "api-subscription-key",
            _options.ApiKey);

        request.Content =
            new StringContent(
                JsonSerializer.Serialize(payload),
                Encoding.UTF8,
                "application/json");

        var response =
            await _httpClient.SendAsync(request);

        var responseContent =
            await response.Content
                .ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(
                $"Sarvam API Error: {responseContent}");
        }

        using var document =
            JsonDocument.Parse(responseContent);

        var base64Audio =
            document.RootElement
                .GetProperty("audios")[0]
                .GetString();

        return Convert.FromBase64String(
            base64Audio!);
    }
}