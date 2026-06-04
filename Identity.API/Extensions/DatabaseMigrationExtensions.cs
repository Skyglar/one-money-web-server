using Duende.IdentityServer.EntityFramework.DbContexts;
using Identity.API.Data;
using Microsoft.EntityFrameworkCore;

namespace Identity.API.Extensions;

public static class DatabaseMigrationExtensions
{
    public static async Task MigrateIdentityDatabasesAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var services = scope.ServiceProvider;
        var logger = services.GetRequiredService<ILoggerFactory>()
            .CreateLogger(nameof(DatabaseMigrationExtensions));

        var applicationDb = services.GetRequiredService<ApplicationDbContext>();
        var configurationDb = services.GetRequiredService<ConfigurationDbContext>();
        var persistedGrantDb = services.GetRequiredService<PersistedGrantDbContext>();

        logger.LogInformation("Applying Application database migrations...");
        await applicationDb.Database.MigrateAsync();

        logger.LogInformation("Applying Configuration database migrations...");
        await configurationDb.Database.MigrateAsync();

        logger.LogInformation("Applying Operational database migrations...");
        await persistedGrantDb.Database.MigrateAsync();

        logger.LogInformation("Identity database migrations completed.");
    }
}
