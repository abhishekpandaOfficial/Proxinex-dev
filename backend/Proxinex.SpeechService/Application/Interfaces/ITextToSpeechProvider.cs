namespace Proxinex.SpeechService.Application.Interfaces;

public interface ITextToSpeechProvider
{
    Task<byte[]> GenerateSpeechAsync(
        string text);
}