using MediatR;

namespace OneMoney.Common.Events;

public record TransactionSuccessIntegrationEvent(Guid TransactionId);