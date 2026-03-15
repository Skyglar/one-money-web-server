using Finances.Domain.Exceptions;
using OneMoney.Common.SeedWork;

namespace Finances.Domain.AggregateModels.AccountAggregate;

public sealed class Account : Entity, IAggregateRoot {
    
    public string Name { get; private set; }

    public decimal Amount { get; private set; }

    public Guid CurrencyId { get; private set; }

    public string? Description { get; private set; }

    public AccountType AccountType { get; private set; }

    public Currency Currency { get; private set; }
    
    private Account() { }

    public Account(string name, decimal initialAmount, string? description, AccountType accountType, Currency currency) {
        if (string.IsNullOrWhiteSpace(name)) throw new AccountException("Name is required");
        if (currency == null) throw new AccountException("Valid currency is required");
        if (initialAmount <= 0) throw new AccountException("Initial amount must be greater than zero");
        
        Id = Guid.NewGuid();
        Name = name;
        CurrencyId = currency.Id;
        Amount = initialAmount;
        Description = description;
        AccountType = accountType;
    }
}