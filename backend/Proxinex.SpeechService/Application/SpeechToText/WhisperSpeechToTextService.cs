using Proxinex.SpeechService.Application.Interfaces;

namespace Proxinex.SpeechService.Application.SpeechToText;

public class WhisperSpeechToTextService
    : ISpeechToTextService
{
    public async Task<string> TranscribeAsync(
        Stream audioStream)
    {
        // TEMP MOCK IMPLEMENTATION

        await Task.Delay(500);

        return "This is a transcribed voice message from Proxinex.";
    }
}