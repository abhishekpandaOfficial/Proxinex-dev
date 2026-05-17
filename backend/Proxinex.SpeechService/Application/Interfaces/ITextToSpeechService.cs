namespace Proxinex.SpeechService.Application.Interfaces;

public interface ITextToSpeechService
{
    Task<byte[]> GenerateSpeechAsync(
        string text);
}