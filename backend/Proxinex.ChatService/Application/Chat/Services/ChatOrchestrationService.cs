using Microsoft.Extensions.Options;
using Microsoft.SemanticKernel;
using Proxinex.ChatService.Application.Chat.Interfaces;
using Proxinex.Shared.Contracts.Chat;
using Proxinex.Shared.Infrastructure.Memory.Interfaces;
using Proxinex.Shared.Infrastructure.Persistence.Entities;
using Proxinex.Shared.Infrastructure.Persistence.Repositories.Interfaces;
using Proxinex.Shared.SemanticKernel.Configuration;
using Proxinex.Shared.SemanticKernel.Routing.Interfaces;
using Serilog;
using System.Text.Json;

namespace Proxinex.ChatService.Application.Chat.Services;

public class ChatOrchestrationService
    : IChatOrchestrationService
{
    private readonly Kernel _kernel;

    private readonly OllamaOptions _ollamaOptions;

    private readonly IConversationMemoryService _memoryService;

    private readonly IChatHistoryRepository _repository;

    private readonly IModelRouter _router;

    public ChatOrchestrationService(
        Kernel kernel,
        IOptions<OllamaOptions> ollamaOptions,
        IConversationMemoryService memoryService,
        IChatHistoryRepository repository,
        IModelRouter router)
    {
        _kernel = kernel;

        _ollamaOptions = ollamaOptions.Value;

        _memoryService = memoryService;

        _repository = repository;

        _router = router;
    }

    public async Task<ChatResponse> ProcessChatAsync(
        ChatRequest request)
    {
        // =========================
        // MODEL ROUTING
        // =========================

        var routedModel =
            _router.Route(
                request.Prompt,

                requiresCode:
                    request.Prompt.Contains(
                        "code",
                        StringComparison.OrdinalIgnoreCase),

                requiresLongContext:
                    request.Prompt.Length > 3000,

                enterprise:
                    request.Prompt.Contains(
                        "enterprise",
                        StringComparison.OrdinalIgnoreCase));

        Log.Information(
            "AI Model Routed | Model: {Model} | Provider: {Provider}",
            routedModel.Name,
            routedModel.Provider);

        // =========================
        // CONVERSATION ID
        // =========================

        var conversationId =
            request.ConversationId ??
            Guid.NewGuid().ToString();

        // =========================
        // MEMORY RETRIEVAL
        // =========================

        var history =
            await _memoryService.GetConversationAsync(
                conversationId);

        var conversationHistory =
            string.Join("\n", history);

        // =========================
        // PROMPT BUILDING
        // =========================

        var finalPrompt =
            $"""
             System:
             {request.SystemPrompt}

             Conversation History:
             {conversationHistory}

             User:
             {request.Prompt}
             """;

        Log.Information(
            "AI Request Started | ConversationId: {ConversationId} | RoutedModel: {Model}",
            conversationId,
            routedModel.Name);

        // =========================
        // LATENCY TRACKING
        // =========================

        var startTime = DateTime.UtcNow;

        // =========================
        // SAVE USER MESSAGE
        // =========================

        await _memoryService.SaveMessageAsync(
            conversationId,
            "user",
            request.Prompt);

        // =========================
        // AI EXECUTION
        // =========================

        var result =
            await _kernel.InvokePromptAsync(
                finalPrompt);

        // =========================
        // TOKEN TRACKING
        // =========================

        var promptTokens =
            EstimateTokens(request.Prompt);

        var completionTokens =
            EstimateTokens(result.ToString());

        var totalTokens =
            promptTokens + completionTokens;

        var estimatedCost =
            totalTokens * 0.000001m;

        // =========================
        // SAVE AI MESSAGE
        // =========================

        await _memoryService.SaveMessageAsync(
            conversationId,
            "assistant",
            result.ToString());

        // =========================
        // LATENCY CALCULATION
        // =========================

        var latency =
            DateTime.UtcNow - startTime;

        // =========================
        // DATABASE PERSISTENCE
        // =========================

        var entity = new ChatHistory
        {
            Id = Guid.NewGuid(),

            ConversationId = conversationId,

            Prompt = request.Prompt,

            Response = result.ToString(),

            Model = routedModel.Name,

            Provider = routedModel.Provider,

            LatencyMs =
                (long)latency.TotalMilliseconds,

            TraceId =
                Guid.NewGuid().ToString(),

            Timestamp = DateTime.UtcNow,

            PromptTokens = promptTokens,

            CompletionTokens = completionTokens,

            TotalTokens = totalTokens,

            EstimatedCost = estimatedCost
        };

        await _repository.AddAsync(entity);

        // =========================
        // LOGGING
        // =========================

        Log.Information(
            "AI Response Completed | ConversationId: {ConversationId} | Model: {Model} | Tokens: {Tokens} | Latency: {Latency}ms",
            conversationId,
            routedModel.Name,
            totalTokens,
            latency.TotalMilliseconds);

        // =========================
        // RESPONSE
        // =========================

        return new ChatResponse
        {
            Response = result.ToString(),

            Model = routedModel.Name,

            Provider = routedModel.Provider,

            ConversationId = conversationId,

            Temperature = request.Temperature,

            Stream = request.Stream,

            Timestamp = DateTime.UtcNow,

            TraceId = Guid.NewGuid(),

            LatencyMs =
                (long)latency.TotalMilliseconds,

            PromptTokens = promptTokens,

            CompletionTokens = completionTokens,

            TotalTokens = totalTokens,

            EstimatedCost = estimatedCost
        };
    }

    public async IAsyncEnumerable<string> StreamChatAsync(
        ChatRequest request)
    {
        var routedModel =
            _router.Route(
                request.Prompt,

                requiresCode:
                    request.Prompt.Contains(
                        "code",
                        StringComparison.OrdinalIgnoreCase),

                requiresLongContext:
                    request.Prompt.Length > 3000);

        Log.Information(
            "Streaming Started | RoutedModel: {Model}",
            routedModel.Name);

        var finalPrompt =
            $"""
             System:
             {request.SystemPrompt}

             User:
             {request.Prompt}
             """;

        await foreach (
            var chunk in _kernel.InvokePromptStreamingAsync(
                finalPrompt))
        {
            var response = new
            {
                content = chunk.ToString(),

                model = routedModel.Name,

                provider = routedModel.Provider,

                timestamp = DateTime.UtcNow
            };

            yield return JsonSerializer.Serialize(
                response);
        }

        Log.Information(
            "Streaming Completed | RoutedModel: {Model}",
            routedModel.Name);
    }

    private int EstimateTokens(string text)
    {
        if (string.IsNullOrWhiteSpace(text))
            return 0;

        return text.Split(
            ' ',
            StringSplitOptions.RemoveEmptyEntries).Length;
    }
}