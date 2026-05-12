using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.SemanticKernel;
using Proxinex.Shared.Contracts.Chat;
using Proxinex.Shared.SemanticKernel.Configuration;
using Serilog;
using Proxinex.Shared.Infrastructure.Memory.Interfaces;

namespace Proxinex.ChatService.Controllers;

[ApiController]
[Route("api/chat")]
public class ChatController : ControllerBase
{
    private readonly Kernel _kernel;

    private readonly OllamaOptions _ollamaOptions;
    private readonly IConversationMemoryService _memoryService;
    

  public ChatController(
    Kernel kernel,
    IOptions<OllamaOptions> ollamaOptions,
    IConversationMemoryService memoryService)
{
    _kernel = kernel;

    _ollamaOptions = ollamaOptions.Value;

    _memoryService = memoryService;
}

    [HttpPost]
    public async Task<IActionResult> Chat(
        [FromBody] ChatRequest request)
    {
        // Generate conversation ID
        var conversationId =
            request.ConversationId ??
            Guid.NewGuid().ToString();

        // Retrieve conversation history
        var history =
         await _memoryService.GetConversationAsync(
        conversationId);

        var conversationHistory =
            string.Join("\n", history);

        // Build prompt - now memory-aware prompting.
     var finalPrompt =
                    $"""
                    System:
                    {request.SystemPrompt}

                    Conversation History:
                    {conversationHistory}

                    User:
                    {request.Prompt}
                    """;

        // Logging request
        Log.Information(
            "AI Request Received | ConversationId: {ConversationId} | Model: {Model} | Prompt: {Prompt}",
            conversationId,
            request.Model ?? _ollamaOptions.ModelId,
            request.Prompt);

        // Start latency tracking
        var startTime = DateTime.UtcNow;
        await _memoryService.SaveMessageAsync(
            conversationId,
            "user",
            request.Prompt);

        // Invoke model
        var result =
            await _kernel.InvokePromptAsync(finalPrompt);

            await _memoryService.SaveMessageAsync(
                conversationId,
                "assistant",
                result.ToString());

        // Calculate latency
        var latency =
            DateTime.UtcNow - startTime;

        // Log response
        Log.Information(
            "AI Response Generated | ConversationId: {ConversationId} | Latency: {Latency}ms",
            conversationId,
            latency.TotalMilliseconds);

        // Return response
        return Ok(new ChatResponse
        {
            Response = result.ToString(),

            Model =
                request.Model ??
                _ollamaOptions.ModelId,

            Provider = "Ollama",

            ConversationId = conversationId,

            Temperature = request.Temperature,

            Stream = request.Stream,

            Timestamp = DateTime.UtcNow,

            TraceId = Guid.NewGuid(),

            LatencyMs = (long)latency.TotalMilliseconds
        });
    }

    [HttpPost("stream")]
    public async Task StreamChat(
        [FromBody] ChatRequest request)
    {
        Response.ContentType = "text/event-stream";

        var finalPrompt =
            string.IsNullOrWhiteSpace(request.SystemPrompt)
            ? request.Prompt
            : $"""
               System:
               {request.SystemPrompt}

               User:
               {request.Prompt}
               """;

        Log.Information(
            "Streaming AI Request Started | Model: {Model}",
            request.Model ?? _ollamaOptions.ModelId);

        await foreach (
        var chunk in _kernel.InvokePromptStreamingAsync(finalPrompt))
        {
            var response = new
            {
                content = chunk.ToString(),

                model = request.Model ??
                        _ollamaOptions.ModelId,

                conversationId =
                    request.ConversationId,

                timestamp = DateTime.UtcNow
            };

            var json =
                System.Text.Json.JsonSerializer.Serialize(response);

            await Response.WriteAsync(
                $"data: {json}\n\n");

            await Response.Body.FlushAsync();
        }

        Log.Information(
            "Streaming AI Response Completed");
    }
}