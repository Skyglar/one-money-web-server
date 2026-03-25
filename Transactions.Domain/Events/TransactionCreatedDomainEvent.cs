using MediatR;

namespace Transactions.Domain.Events;

public record TransactionCreatedDomainEvent(
    Guid TransactionId,
    Guid AccountId,
    Guid CategoryId,
    decimal Amount) : INotification;