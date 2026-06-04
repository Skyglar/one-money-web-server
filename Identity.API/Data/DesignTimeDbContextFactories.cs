using Duende.IdentityServer.EntityFramework.DbContexts;
using Duende.IdentityServer.EntityFramework.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Identity.API.Data;

public sealed class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args) =>
        new(DesignTimeDbContextConfiguration.CreateOptionsBuilder<ApplicationDbContext>().Options);
}

public sealed class ConfigurationDbContextFactory : IDesignTimeDbContextFactory<ConfigurationDbContext>
{
    public ConfigurationDbContext CreateDbContext(string[] args)
    {
        var connectionString = DesignTimeDbContextConfiguration.GetConnectionString();
        var services = new ServiceCollection();
        services.AddIdentityServer()
            .AddConfigurationStore(options =>
            {
                options.ConfigureDbContext = b => b.UseSqlServer(connectionString,
                    sql => DesignTimeDbContextConfiguration.ConfigureSqlServer<ConfigurationDbContext>(sql));
            });

        var provider = services.BuildServiceProvider();
        return provider.GetRequiredService<ConfigurationDbContext>();
    }
}

public sealed class PersistedGrantDbContextFactory : IDesignTimeDbContextFactory<PersistedGrantDbContext>
{
    public PersistedGrantDbContext CreateDbContext(string[] args)
    {
        var connectionString = DesignTimeDbContextConfiguration.GetConnectionString();
        var services = new ServiceCollection();
        services.AddIdentityServer()
            .AddOperationalStore(options =>
            {
                options.ConfigureDbContext = b => b.UseSqlServer(connectionString,
                    sql => DesignTimeDbContextConfiguration.ConfigureSqlServer<PersistedGrantDbContext>(sql));
            });

        var provider = services.BuildServiceProvider();
        return provider.GetRequiredService<PersistedGrantDbContext>();
    }
}

internal static class DesignTimeDbContextConfiguration
{
    internal static DbContextOptionsBuilder<TContext> CreateOptionsBuilder<TContext>()
        where TContext : DbContext
    {
        var optionsBuilder = new DbContextOptionsBuilder<TContext>();
        optionsBuilder.UseSqlServer(GetConnectionString(),
            sql => ConfigureSqlServer<TContext>(sql));
        return optionsBuilder;
    }

    internal static void ConfigureSqlServer<TContext>(SqlServerDbContextOptionsBuilder sql)
        where TContext : DbContext
    {
        sql.MigrationsAssembly(IdentityDbConstants.MigrationsAssembly);
        sql.MigrationsHistoryTable(GetHistoryTableName<TContext>());
    }

    internal static string GetConnectionString()
    {
        var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false)
            .AddJsonFile($"appsettings.{environment}.json", optional: true)
            .AddEnvironmentVariables()
            .Build();

        return configuration.GetConnectionString("IdentityConnection")
            ?? throw new InvalidOperationException("Connection string 'IdentityConnection' is not configured.");
    }

    private static string GetHistoryTableName<TContext>() =>
        typeof(TContext).Name switch
        {
            nameof(ApplicationDbContext) => IdentityDbConstants.ApplicationMigrationsHistoryTable,
            nameof(ConfigurationDbContext) => IdentityDbConstants.ConfigurationMigrationsHistoryTable,
            nameof(PersistedGrantDbContext) => IdentityDbConstants.OperationalMigrationsHistoryTable,
            _ => throw new InvalidOperationException($"Unsupported DbContext type: {typeof(TContext).Name}")
        };
}
