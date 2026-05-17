using System.Net.Http.Headers;
using System.Text.Json;
using Microsoft.Extensions.Options;
using Proxinex.SpeechService.Application.Interfaces;
using Proxinex.SpeechService.Configuration;

namespace Proxinex.SpeechService.Infrastructure.Providers.Sarvam;

public class SarvamSpeechToTextProvider
    : ISpeechToTextProvider
{
    private readonly HttpClient _httpClient;

    private readonly SarvamOptions _options;

    public SarvamSpeechToTextProvider(
        HttpClient httpClient,
        IOptions<SarvamOptions> options)
    {
        _httpClient = httpClient;

        _options = options.Value;
    }

    public async Task<string> TranscribeAsync(
        Stream audioStream)
    {
        using var content =
            new MultipartFormDataContent();

        var audioContent =
            new StreamContent(audioStream);

        audioContent.Headers.ContentType =
            new MediaTypeHeaderValue(
                "audio/wav");

        content.Add(
            audioContent,
            "file",
            "audio.wav");

        content.Add(
            new StringContent("en-IN"),
            "language_code");

        content.Add(
            new StringContent("saarika:v2.5"),
            "model");

        var request =
            new HttpRequestMessage(
                HttpMethod.Post,
                "https://api.sarvam.ai/speech-to-text");

        request.Headers.Add(
            "api-subscription-key",
            _options.ApiKey);

        request.Content = content;

        var response =
            await _httpClient.SendAsync(
                request);

        var responseBody =
            await response.Content
                .ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(
                $"Sarvam API Error: {responseBody}");
        }

        using var document =
            JsonDocument.Parse(responseBody);

        return document.RootElement
            .GetProperty("transcript")
            .GetString() ?? "";
    }
}