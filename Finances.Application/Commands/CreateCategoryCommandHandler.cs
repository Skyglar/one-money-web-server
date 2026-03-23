using Finances.Domain.AggregateModels.CategoryAggregate;
using MediatR;
using OneMoney.Common.SeedWork;

namespace Finances.Application.Commands;

public sealed class CreateCategoryCommandHandler(
    ICategoryRepository categoryRepository,
    IUnitOfWork unitOfWork
    ) : IRequestHandler<CreateCategoryCommand, Guid> {
    
    public async Task<Guid> Handle(CreateCategoryCommand request, CancellationToken cancellationToken) {
        string color = request.color ?? "FFFFFF"; // Create const with default color
        string imageUrl = request.imageUrl ?? ""; // Same for image url

        Category category = new Category(
            request.name,
            imageUrl,
            color);

        categoryRepository.Add(category);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return category.Id;
    }
}