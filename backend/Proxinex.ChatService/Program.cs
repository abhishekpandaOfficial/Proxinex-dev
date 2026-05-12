using Proxinex.Shared.SemanticKernel.Extensions;
using Serilog;
using OpenTelemetry.Trace;
using OpenTelemetry.Resources;
using OpenTelemetry.Metrics;
using StackExchange.Redis;
using Proxinex.Shared.Infrastructure.Memory.Interfaces;
using Proxinex.Shared.Infrastructure.Memory.Services;


Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File(
        "logs/proxinex-.txt",
        rollingInterval: RollingInterval.Day)
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog();



// Controllers
builder.Services.AddControllers();


// Swagger
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();


// Semantic Kernel
builder.Services.AddProxinexKernel(
    builder.Configuration);


// OpenTelemetry
builder.Services.AddOpenTelemetry().WithTracing(tracing =>
    {
        tracing
            .AddAspNetCoreInstrumentation()
            .AddHttpClientInstrumentation()
            .AddOtlpExporter();
    })
    .WithMetrics(metrics =>
    {
        metrics
            .AddAspNetCoreInstrumentation()
            .AddRuntimeInstrumentation();
    });;

    // Adding Redis connection multiplexer and conversation memory service
    builder.Services.AddSingleton<IConnectionMultiplexer>(
        ConnectionMultiplexer.Connect("localhost:6379"));

    builder.Services.AddScoped<
        IConversationMemoryService,
        ConversationMemoryService>();


// Build app
var app = builder.Build();


// Swagger
app.UseSwagger();

app.UseSwaggerUI();


// HTTPS
app.UseHttpsRedirection();


// Authorization
app.UseAuthorization();


// Controllers
app.MapControllers();


// Run
app.Run();