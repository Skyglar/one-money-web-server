using MediatR;

namespace Finances.Application.Commands;

public record CreateCategoryCommand(
    string Name,
    string? Color,
    string? ImageUrl
) : IRequest<Guid>;
    
