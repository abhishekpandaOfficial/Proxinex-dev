using Proxinex.SpeechService.Application.Interfaces;
using Proxinex.SpeechService.Application.SpeechToText;
using Proxinex.SpeechService.Application.TextToSpeech;
using Proxinex.SpeechService.Configuration;
using Proxinex.SpeechService.Infrastructure.Providers.Sarvam;

var builder = WebApplication.CreateBuilder(args);

// =====================================
// Controllers
// =====================================

builder.Services.AddControllers();

// =====================================
// Swagger
// =====================================

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.Configure<SarvamOptions>(
    builder.Configuration.GetSection("Sarvam"));

// =====================================
// Speech Services
// =====================================

// builder.Services.AddScoped<
//     ISpeechToTextService,
//     WhisperSpeechToTextService>();

// builder.Services.AddScoped<
//     ITextToSpeechService,
//     PiperTextToSpeechService>();

builder.Services.AddHttpClient();

builder.Services.AddScoped<
    ISpeechToTextProvider,
    SarvamSpeechToTextProvider>();

builder.Services.AddScoped<
    ITextToSpeechProvider,
    SarvamTextToSpeechProvider>();

var app = builder.Build();

// =====================================
// Swagger
// =====================================

app.UseSwagger();

app.UseSwaggerUI();

// =====================================
// HTTPS
// =====================================

app.UseHttpsRedirection();

// =====================================
// Controllers
// =====================================

app.MapControllers();

app.Run();