using MediatR;

namespace Finances.Domain.Events;

public record BalanceDeductedDomainEvent(
    Guid TransactionId) : INotification;