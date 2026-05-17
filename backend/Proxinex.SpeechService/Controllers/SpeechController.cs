using Microsoft.AspNetCore.Mvc;
using Proxinex.SpeechService.Application.Interfaces;

namespace Proxinex.SpeechService.Controllers;

[ApiController]
[Route("api/speech")]
public class SpeechController : ControllerBase
{
    private readonly ISpeechToTextService _speechToText;

    private readonly ITextToSpeechService _textToSpeech;

    public SpeechController(
        ISpeechToTextService speechToText,
        ITextToSpeechService textToSpeech)
    {
        _speechToText = speechToText;

        _textToSpeech = textToSpeech;
    }

    [HttpPost("transcribe")]
    public async Task<IActionResult> Transcribe(
        IFormFile file)
    {
        using var stream =
            file.OpenReadStream();

        var result =
            await _speechToText
                .TranscribeAsync(stream);

        return Ok(new
        {
            transcription = result
        });
    }

    [HttpPost("speak")]
    public async Task<IActionResult> Speak(
        [FromBody] TextToSpeechRequest request)
    {
        var audio =
            await _textToSpeech
                .GenerateSpeechAsync(
                    request.Text);

        return File(
            audio,
            "audio/wav",
            "speech.wav");
    }
}

public class TextToSpeechRequest
{
    public string Text { get; set; }
        = default!;
}