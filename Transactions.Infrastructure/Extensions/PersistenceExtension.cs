using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OneMoney.Common.SeedWork;
using Transactions.Domain.AggregateModels;
using Transactions.Infrastructure.Data;
using Transactions.Infrastructure.Repositories;

namespace Transactions.Infrastructure.Extensions;

public static class PersistenceExtension {
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration) {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        Console.WriteLine(connectionString);
        
        services.AddDbContext<TransactionsDbContext>(options =>
            options.UseSqlServer(connectionString, sqlOptions =>
            {
                sqlOptions.EnableRetryOnFailure(
                    maxRetryCount: 5,
                    maxRetryDelay: TimeSpan.FromSeconds(10),
                    errorNumbersToAdd: null);
            }));
        
        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<TransactionsDbContext>());
        services.AddScoped<ITransactionRepository, TransactionRepository>();

        return services;
    }
    
    public static void ApplyMigrations(this IServiceProvider serviceProvider) {
        using var scope = serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<TransactionsDbContext>();

        var retries = 10;
        while (retries > 0)
        {
            try
            {
                Console.WriteLine("Trying to migrate DB...");
                dbContext.Database.Migrate();
                Console.WriteLine("Migration successful");
                break;
            }
            catch (Exception ex)
            {
                retries--;
                Console.WriteLine($"DB not ready: {ex.Message}");

                if (retries == 0) throw;

                Thread.Sleep(5000);
            }
        }
    }
}