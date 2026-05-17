using Proxinex.SpeechService.Application.Interfaces;
using Proxinex.SpeechService.Application.SpeechToText;
using Proxinex.SpeechService.Application.TextToSpeech;

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

// =====================================
// Speech Services
// =====================================

builder.Services.AddScoped<
    ISpeechToTextService,
    WhisperSpeechToTextService>();

builder.Services.AddScoped<
    ITextToSpeechService,
    PiperTextToSpeechService>();

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