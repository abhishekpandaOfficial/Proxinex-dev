namespace Proxinex.Shared.Infrastructure.Memory.Interfaces;

public interface IConversationMemoryService
{
    Task SaveMessageAsync(
        string conversationId,
        string role,
        string message);

    Task<List<string>> GetConversationAsync(
        string conversationId);
}