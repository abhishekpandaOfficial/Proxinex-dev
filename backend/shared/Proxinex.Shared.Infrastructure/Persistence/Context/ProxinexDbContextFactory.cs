using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Proxinex.Shared.Infrastructure.Persistence.Context;

public class ProxinexDbContextFactory
    : IDesignTimeDbContextFactory<ProxinexDbContext>
{
    public ProxinexDbContext CreateDbContext(
        string[] args)
    {
        var optionsBuilder =
            new DbContextOptionsBuilder<ProxinexDbContext>();

        optionsBuilder.UseNpgsql(
            "Host=localhost;Port=5432;Database=proxinex;Username=proxinex;Password=proxinex");

        return new ProxinexDbContext(
            optionsBuilder.Options);
    }
}