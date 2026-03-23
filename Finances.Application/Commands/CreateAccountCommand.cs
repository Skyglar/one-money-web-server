using Finances.Domain.AggregateModels.AccountAggregate;
using MediatR;

namespace Finances.Application.Commands;

public record CreateAccountCommand(
    string Name,
    AccountType AccountType, // e.g., "Saving"
    decimal InitialAmount,
    string Currency,
    string? Description
) : IRequest<Guid>;
