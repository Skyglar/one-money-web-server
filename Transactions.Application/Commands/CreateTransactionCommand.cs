using MediatR;
using OneMoney.Common.Primitives;

namespace Transactions.Application.Commands;

public record CreateTransactionCommand(
    Guid AccountId,
    Guid CategoryId,
    decimal Amount,
    string Currency,
    string Description
    ) : IRequest<Result<Guid>>;