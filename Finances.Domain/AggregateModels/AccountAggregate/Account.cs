using Finances.Domain.Events;
using Finances.Domain.Exceptions;
using OneMoney.Common.SeedWork;

namespace Finances.Domain.AggregateModels.AccountAggregate;

public sealed class Account : Entity, IAggregateRoot {
    
    public string Name { get; private set; } = string.Empty;

    public decimal Balance { get; private set; }

    public Guid CurrencyId { get; private set; }

    public string? Description { get; private set; }

    public AccountType AccountType { get; private set; }

    public Currency Currency { get; private set; } = null!;
    
    private Account() { }

    public Account(string name, decimal initialBalance, string? description, AccountType accountType, Currency currency) {
        if (string.IsNullOrWhiteSpace(name)) throw new AccountException(ExceptionMessages.NAME_CANT_BE_NULL_OR_EMPTY);
        if (currency == null) throw new AccountException(ExceptionMessages.CURRENCY_CANT_BE_NULL);
        if (initialBalance < 0) throw new AccountException(ExceptionMessages.INVALID_AMOUNT);
        
        Id = Guid.NewGuid();
        Name = name;
        CurrencyId = currency.Id;
        Balance = initialBalance;
        Description = description;
        AccountType = accountType;
    }

    public void Debit(decimal amount, Guid transactionId) {
        if (amount < 0) throw new AccountException(ExceptionMessages.INVALID_AMOUNT);
        if (Balance < amount) throw new AccountException(ExceptionMessages.INSUFFICIENT_AMOUNT);
        Balance -= amount;
        
        AddDomainEvent(new BalanceDeductedDomainEvent(transactionId));
    }
}