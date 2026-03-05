namespace Finances.Domain.AggregateModels.AccountAggregate;

public sealed class AccountType {
    public int Id { get; init; }
    public required string Name { get; init; }
}