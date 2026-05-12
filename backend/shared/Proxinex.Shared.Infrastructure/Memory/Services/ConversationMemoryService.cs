using StackExchange.Redis;
using Proxinex.Shared.Infrastructure.Memory.Interfaces;

namespace Proxinex.Shared.Infrastructure.Memory.Services;

public class ConversationMemoryService
    : IConversationMemoryService
{
    private readonly IDatabase _database;

    public ConversationMemoryService(
        IConnectionMultiplexer redis)
    {
        _database = redis.GetDatabase();
    }

    public async Task SaveMessageAsync(
        string conversationId,
        string role,
        string message)
    {
        await _database.ListRightPushAsync(
            $"chat:{conversationId}",
            $"{role}:{message}");
    }

    public async Task<List<string>> GetConversationAsync(
        string conversationId)
    {
        var messages =
            await _database.ListRangeAsync(
                $"chat:{conversationId}");

        return messages
            .Select(x => x.ToString())
            .ToList();
    }
}