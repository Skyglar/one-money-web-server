namespace OneMoney.Common.Events;

public record TransactionFailedIntegrationEvent(
    Guid TransactionId,
    string Error);
    
