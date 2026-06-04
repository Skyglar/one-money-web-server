using Identity.API.Data;
using Identity.API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Identity.API.Extensions;

public static class PersistenceExtensions
{
    public static IServiceCollection AddIdentityDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("IdentityConnection")
            ?? throw new InvalidOperationException("Connection string 'IdentityConnection' is not configured.");
        var issuerUri = configuration["Identity:IssuerUri"];

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString, sql => ConfigureSqlServer(sql, IdentityDbConstants.ApplicationMigrationsHistoryTable, enableRetry: true)));

        services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 6;
                options.Password.RequireDigit = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

        services.AddIdentityServer(options =>
            {
                options.Events.RaiseErrorEvents = true;
                options.Events.RaiseInformationEvents = true;
                options.Events.RaiseFailureEvents = true;
                options.Events.RaiseSuccessEvents = true;
                options.EmitStaticAudienceClaim = true;

                if (!string.IsNullOrWhiteSpace(issuerUri))
                    options.IssuerUri = issuerUri;
            })
            .AddAspNetIdentity<ApplicationUser>()
            .AddConfigurationStore(options =>
            {
                options.ConfigureDbContext = b => b.UseSqlServer(connectionString,
                    sql => ConfigureSqlServer(sql, IdentityDbConstants.ConfigurationMigrationsHistoryTable));
            })
            .AddOperationalStore(options =>
            {
                options.ConfigureDbContext = b => b.UseSqlServer(connectionString,
                    sql => ConfigureSqlServer(sql, IdentityDbConstants.OperationalMigrationsHistoryTable));

                options.EnableTokenCleanup = true;
            })
            .AddDeveloperSigningCredential();

        return services;
    }

    private static void ConfigureSqlServer(
        SqlServerDbContextOptionsBuilder sql,
        string migrationsHistoryTable,
        bool enableRetry = false)
    {
        sql.MigrationsAssembly(IdentityDbConstants.MigrationsAssembly);
        sql.MigrationsHistoryTable(migrationsHistoryTable);

        if (enableRetry)
            sql.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), []);
    }
}
