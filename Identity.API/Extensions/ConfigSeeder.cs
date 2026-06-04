using Duende.IdentityServer.EntityFramework.DbContexts;
using Duende.IdentityServer.EntityFramework.Mappers;
using Identity.API.Data;
using Identity.API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Identity.API.Extensions;

public static class ConfigSeeder
{
    public static async Task SeedAsync(WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var services = scope.ServiceProvider;
        var logger = services.GetRequiredService<ILoggerFactory>().CreateLogger(nameof(ConfigSeeder));
        var configuration = app.Configuration;

        var configDb = services.GetRequiredService<ConfigurationDbContext>();

        if (!await configDb.Clients.AnyAsync())
        {
            foreach (var resource in Config.IdentityResources)
                configDb.IdentityResources.Add(resource.ToEntity());

            foreach (var apiScope in Config.ApiScopes)
                configDb.ApiScopes.Add(apiScope.ToEntity());

            foreach (var apiResource in Config.ApiResources)
                configDb.ApiResources.Add(apiResource.ToEntity());

            foreach (var client in Config.Clients)
                configDb.Clients.Add(client.ToEntity());

            await configDb.SaveChangesAsync();
            logger.LogInformation("Seeded IdentityServer configuration store.");
        }

        await SeedDevUserAsync(services, configuration, logger);
    }

    private static async Task SeedDevUserAsync(
        IServiceProvider services,
        IConfiguration configuration,
        ILogger logger)
    {
        var email = configuration["Seed:DevUser:Email"] ?? "dev@onemoney.local";
        var password = configuration["Seed:DevUser:Password"] ?? "DevPass123!";

        var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();

        if (await userManager.FindByEmailAsync(email) is not null)
            return;

        var user = new ApplicationUser
        {
            UserName = email,
            Email = email,
            EmailConfirmed = true
        };

        var result = await userManager.CreateAsync(user, password);
        if (result.Succeeded)
        {
            logger.LogInformation("Created seed user {Email}", email);
            return;
        }

        logger.LogWarning(
            "Failed to create seed user {Email}: {Errors}",
            email,
            string.Join(", ", result.Errors.Select(e => e.Description)));
    }
}
