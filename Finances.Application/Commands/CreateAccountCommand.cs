using MediatR;

namespace Finances.Application.Commands;

public record CreateAccountCommand(
    string Name,
    string AccountType, // e.g., "Saving"
    decimal InitialAmount,
    string Currency,
    string? Description
) : IRequest<Guid>;
