namespace OneMoney.Common.Events;

public record TransactionCreatedIntegrationEvent (
    decimal Amount,
    Guid AccountId,
    Guid CategoryId,
    Guid TransactionId,
    DateTime CreatedAt
);