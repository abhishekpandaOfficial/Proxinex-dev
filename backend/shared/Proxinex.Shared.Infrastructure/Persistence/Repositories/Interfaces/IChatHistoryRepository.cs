using Proxinex.Shared.Infrastructure.Persistence.Entities;

namespace Proxinex.Shared.Infrastructure.Persistence.Repositories.Interfaces;

public interface IChatHistoryRepository
{
    Task AddAsync(ChatHistory entity);
}