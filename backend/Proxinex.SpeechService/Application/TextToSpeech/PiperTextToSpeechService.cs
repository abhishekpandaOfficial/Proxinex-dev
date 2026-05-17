using Proxinex.SpeechService.Application.Interfaces;

namespace Proxinex.SpeechService.Application.TextToSpeech;

public class PiperTextToSpeechService
    : ITextToSpeechService
{
    public async Task<byte[]> GenerateSpeechAsync(
        string text)
    {
        await Task.Delay(500);

        return Array.Empty<byte>();
    }
}