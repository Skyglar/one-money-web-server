using Finances.Domain.SeedWork;

namespace Finances.Domain.AggregateModels.AccountAggregate;

public sealed class Account : Entity, IAggregateRoot {
    public required string Name { get; set; }

    public decimal Amount { get; set; }

    public long CurrencyId { get; set; }

    public AccountType? AccountType { get; set; }

    public Currency? Currency { get; set; }
}