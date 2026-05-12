using Proxinex.Shared.Contracts.Chat;

namespace Proxinex.ChatService.Application.Chat.Interfaces;

public interface IChatOrchestrationService
{
    Task<ChatResponse> ProcessChatAsync(
        ChatRequest request);

    IAsyncEnumerable<string> StreamChatAsync(
        ChatRequest request);
}