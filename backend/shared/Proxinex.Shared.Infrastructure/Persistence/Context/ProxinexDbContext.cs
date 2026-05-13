using Microsoft.EntityFrameworkCore;
using Proxinex.Shared.Infrastructure.Persistence.Entities;

namespace Proxinex.Shared.Infrastructure.Persistence.Context;

public class ProxinexDbContext : DbContext
{
    public ProxinexDbContext(
        DbContextOptions<ProxinexDbContext> options)
        : base(options)
    {
    }

    public DbSet<ChatHistory> ChatHistories =>
        Set<ChatHistory>();
    public DbSet<DocumentChunk> DocumentChunks =>
        Set<DocumentChunk>();
}