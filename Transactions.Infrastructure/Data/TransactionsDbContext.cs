using MediatR;
using Microsoft.EntityFrameworkCore;
using OneMoney.Common.SeedWork;
using Transactions.Infrastructure.EntityConfigurations;

namespace Transactions.Infrastructure.Data;

public sealed class TransactionsDbContext : DbContext, IUnitOfWork {
    private readonly IMediator _mediator;
    
    public TransactionsDbContext(DbContextOptions<TransactionsDbContext> options, IMediator mediator) : base(options) {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        base.OnModelCreating(modelBuilder);

        modelBuilder.HasDefaultSchema("transactions");
        modelBuilder.ApplyConfiguration(new TransactionTypeEntityConfiguration());
    }
    
    public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default) {
        // Dispatch Domain Events collection. 
        // Choices:
        // A) Right BEFORE committing data (EF SaveChanges) into the DB will make a single transaction including  
        // side effects from the domain event handlers which are using the same DbContext with "InstancePerLifetimeScope" or "scoped" lifetime
        // B) Right AFTER committing data (EF SaveChanges) into the DB will make multiple transactions. 
        // You will need to handle eventual consistency and compensatory actions in case of failures in any of the Handlers. 
        await _mediator.DispatchDomainEventsAsync(this);

        // After executing this line all the changes (from the Command Handler and Domain Event Handlers) 
        // performed through the DbContext will be committed
        _ = await base.SaveChangesAsync(cancellationToken);

        return true;
    }
}