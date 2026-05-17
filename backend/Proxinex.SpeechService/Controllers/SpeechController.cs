using Microsoft.AspNetCore.Mvc;
using Proxinex.SpeechService.Application.Interfaces;
using Serilog;

namespace Proxinex.SpeechService.Controllers;

[ApiController]
[Route("api/speech")]
public class SpeechController : ControllerBase
{
    private readonly ISpeechToTextProvider _stt;

    private readonly ITextToSpeechProvider _tts;

    public SpeechController(
        ISpeechToTextProvider stt,
        ITextToSpeechProvider tts)
    {
        _stt = stt;

        _tts = tts;
    }

    // =========================================
    // SPEECH TO TEXT
    // =========================================
    [HttpPost("transcribe")]
    public async Task<IActionResult> Transcribe(
        IFormFile file,
        CancellationToken cancellationToken)
    {
        try
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest(new
                {
                    error = "Audio file is required."
                });
            }

            Log.Information(
                "STT Request Started | File: {FileName}",
                file.FileName);

            using var stream =
                file.OpenReadStream();

            var text =
                await _stt.TranscribeAsync(
                    stream);

            Log.Information(
                "STT Request Completed");

            return Ok(new
            {
                transcription = text,

                fileName = file.FileName,

                timestamp = DateTime.UtcNow
            });
        }
        catch (Exception ex)
        {
            Log.Error(
                ex,
                "STT Processing Failed");

            return StatusCode(500, new
            {
                error = ex.Message
            });
        }
    }

    // =========================================
    // TEXT TO SPEECH
    // =========================================
    [HttpPost("speak")]
    public async Task<IActionResult> Speak(
        [FromBody] TextToSpeechRequest request,
        CancellationToken cancellationToken)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(
                request.Text))
            {
                return BadRequest(new
                {
                    error = "Text is required."
                });
            }

            Log.Information(
                "TTS Request Started");

            var audio =
                await _tts.GenerateSpeechAsync(
                    request.Text);

            Log.Information(
                "TTS Request Completed");

            return File(
                audio,
                "audio/wav",
                $"speech-{Guid.NewGuid()}.wav");
        }
        catch (Exception ex)
        {
            Log.Error(
                ex,
                "TTS Processing Failed");

            return StatusCode(500, new
            {
                error = ex.Message
            });
        }
    }

    // =========================================
    // HEALTH CHECK
    // =========================================
    [HttpGet("health")]
    public IActionResult Health()
    {
        return Ok(new
        {
            service = "Proxinex Speech Service",

            status = "healthy",

            timestamp = DateTime.UtcNow
        });
    }
}

public class TextToSpeechRequest
{
    public string Text { get; set; } = "";
}