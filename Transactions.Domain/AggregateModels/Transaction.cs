using OneMoney.Common.SeedWork;
using Transactions.Domain.Events;
using Transactions.Domain.Exceptions;

namespace Transactions.Domain.AggregateModels;

public sealed class Transaction : Entity, IAggregateRoot {
    public decimal Amount { get; private set; }
    public Guid AccountId { get; private set; }
    public Guid CategoryId { get; private set; }
    public DateTime TransactionDate { get; private set; }
    public string CurrencyCode { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public TransactionStatus Status { get; private set; } = TransactionStatus.Pending;
    public string? FailureReason { get; private set; }

    private Transaction() { }

    public Transaction(decimal amount, Guid accountId, Guid categoryId, string currencyCode, string description) {
        if (amount <= 0) throw new TransactionException(ExceptionMessages.AMOUNT_CANNOT_BE_ZERO_OR_NEGATIVE);
        if (accountId == Guid.Empty) throw new TransactionException(ExceptionMessages.ACCOUNT_ID_IS_INVALID);
        if (categoryId == Guid.Empty) throw new TransactionException(ExceptionMessages.CATEGORY_ID_IS_INVALID);

        Id = Guid.NewGuid();
        Amount = amount;
        AccountId = accountId;
        CategoryId = categoryId;
        CurrencyCode = currencyCode;
        Description = description;
        TransactionDate = DateTime.UtcNow;

        AddTransactionCreatedDomainEvent(Id, accountId, categoryId, amount);
    }

    public void MarkAsCompleted() => Status = TransactionStatus.Completed;

    public void MarkAsFailed(string reason) {
        Status = TransactionStatus.Failed;
        FailureReason = reason;
    }

    private void AddTransactionCreatedDomainEvent(Guid transactionId, Guid accountId, Guid categoryId, decimal amount) {
        var transactionCreatedDomainEvent = new TransactionCreatedDomainEvent(transactionId, accountId, categoryId, amount);
        
        AddDomainEvent(transactionCreatedDomainEvent);
    }
}