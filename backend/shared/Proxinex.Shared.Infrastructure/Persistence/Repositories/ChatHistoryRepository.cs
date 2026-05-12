using Proxinex.Shared.Infrastructure.Persistence.Context;
using Proxinex.Shared.Infrastructure.Persistence.Entities;
using Proxinex.Shared.Infrastructure.Persistence.Repositories.Interfaces;

namespace Proxinex.Shared.Infrastructure.Persistence.Repositories;

public class ChatHistoryRepository
    : IChatHistoryRepository
{
    private readonly ProxinexDbContext _dbContext;

    public ChatHistoryRepository(
        ProxinexDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(
        ChatHistory entity)
    {
        _dbContext.ChatHistories.Add(entity);

        await _dbContext.SaveChangesAsync();
    }
}