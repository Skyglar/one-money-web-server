using MediatR;
using OneMoney.Common.Primitives;

namespace Finances.Application.Commands;

public record ProcessIncomingTransactionCommand(
    Guid TransactionId,
    Guid AccountId,
    Guid CategoryId,
    decimal Amount) : IRequest<Result>; 
    
