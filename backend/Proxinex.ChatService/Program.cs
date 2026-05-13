using Proxinex.Shared.SemanticKernel.Extensions;
using Serilog;
using OpenTelemetry.Trace;
using OpenTelemetry.Resources;
using OpenTelemetry.Metrics;
using StackExchange.Redis;
using Proxinex.Shared.Infrastructure.Memory.Interfaces;
using Proxinex.Shared.Infrastructure.Memory.Services;
using Microsoft.EntityFrameworkCore;
using Proxinex.Shared.Infrastructure.Persistence.Context;

using Proxinex.ChatService.Application.Chat.Interfaces;
using Proxinex.ChatService.Application.Chat.Services;
using Proxinex.Shared.Infrastructure.Persistence.Repositories;
using Proxinex.Shared.Infrastructure.Persistence.Repositories.Interfaces;
using Proxinex.Shared.SemanticKernel.Configuration;
using Proxinex.Shared.SemanticKernel.Routing.Interfaces;
using Proxinex.Shared.SemanticKernel.Routing;
using Proxinex.ChatService.Infrastructure.Rag;

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
builder.Services.AddOpenTelemetry().ConfigureResource(resource =>
    {
        resource.AddService("Proxinex.ChatService");
    })
    .WithTracing(tracing =>
    {
        tracing
            .AddAspNetCoreInstrumentation()
            .AddHttpClientInstrumentation()
            .AddOtlpExporter(options =>
            {
                options.Endpoint =
                    new Uri("http://localhost:4317");
            });
    })
    .WithMetrics(metrics =>
    {
        metrics
            .AddAspNetCoreInstrumentation()
            .AddRuntimeInstrumentation();
    });;

    builder.Services.Configure<AIModelsOptions>(
            builder.Configuration.GetSection("AIModels"));

            builder.Services.AddSingleton<
                IModelRouter,
                ModelRouter>();

    // Adding Redis connection multiplexer and conversation memory service
    builder.Services.AddSingleton<IConnectionMultiplexer>(
        ConnectionMultiplexer.Connect("localhost:6379"));

    builder.Services.AddHttpClient();

    builder.Services.AddScoped<
            IRagServiceClient,
            RagServiceClient>();
            
    builder.Services.AddScoped<
        IConversationMemoryService,
        ConversationMemoryService>();

    builder.Services.AddScoped<
            IChatOrchestrationService,
            ChatOrchestrationService>();

    builder.Services.AddScoped<
            IChatHistoryRepository,
            ChatHistoryRepository>();

    builder.Services.AddDbContext<ProxinexDbContext>(
            options =>
            {
                options.UseNpgsql(
                    "Host=localhost;Port=5432;Database=proxinex;Username=proxinex;Password=proxinex");
            });

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