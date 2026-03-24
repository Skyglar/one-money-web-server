using Finances.Application.Commands;
using Finances.Domain.AggregateModels.CategoryAggregate;
using NSubstitute;
using OneMoney.Common.SeedWork;

namespace Finances.UnitTests.Application;

public class CreateCategoryCommandHandlerTests {
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICategoryRepository _categoryRepository;
    private readonly CreateCategoryCommandHandler _handler;

    public CreateCategoryCommandHandlerTests() {
        _unitOfWork = Substitute.For<IUnitOfWork>();
        _categoryRepository = Substitute.For<ICategoryRepository>();

        _handler = new CreateCategoryCommandHandler(_categoryRepository, _unitOfWork);
    }

    [Fact]
    public async Task Handle_ValidRequest_ShouldCreateCategoryAndReturnId() {
        // Arrange
        var command = new CreateCategoryCommand(
            "Restaurants",
            "",
            "");
        
        // Act
        var resultId = await _handler.Handle(command, CancellationToken.None);
        
        // Assert
        Assert.NotEqual(Guid.Empty, resultId);
        
        _categoryRepository.Received(1).Add(Arg.Is<Category>(a => 
            a != null &&
            a.Name == command.Name && 
            a.Color == command.Color &&
            a.Image == command.ImageUrl &&
            a.Id == resultId));

        await _unitOfWork.Received(1).SaveChangesAsync(Arg.Any<CancellationToken>());
    }
}