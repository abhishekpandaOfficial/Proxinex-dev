namespace Proxinex.SpeechService.Application.Interfaces;

public interface ISpeechToTextProvider
{
    Task<string> TranscribeAsync(
        Stream audioStream);
}