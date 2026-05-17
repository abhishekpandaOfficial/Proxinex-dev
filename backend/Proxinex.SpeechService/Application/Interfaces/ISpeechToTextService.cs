namespace Proxinex.SpeechService.Application.Interfaces;

public interface ISpeechToTextService
{
    Task<string> TranscribeAsync(
        Stream audioStream);
}