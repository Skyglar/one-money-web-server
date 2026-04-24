using NSubstitute;
using OneMoney.Common.SeedWork;
using Transactions.Application.Commands;
using Transactions.Domain.AggregateModels;

namespace Transactions.UnitTests.Application;

public class CreateTransactionCommandHandlerTests {
    private readonly ITransactionRepository _transactionRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly CreateTransactionCommandHandler _handler;
    
    public CreateTransactionCommandHandlerTests() {
        _transactionRepository = Substitute.For<ITransactionRepository>();
        _unitOfWork = Substitute.For<IUnitOfWork>();
        
        _handler = new CreateTransactionCommandHandler(_transactionRepository, _unitOfWork);
    }
    
    [Fact]
    public async Task Handle_ValidRequest_ShouldCreateTransactionAndReturnId() {
        // Arrange
        var command = new CreateTransactionCommand(
            Guid.NewGuid(), 
            Guid.NewGuid(), 
            100m,
            "usd",
            "");
        
        // Act
        var result = await _handler.Handle(command, CancellationToken.None);
        
        // Assert
        Assert.NotEqual(Guid.Empty, result.Value);
        
        _transactionRepository.Received(1).Add(Arg.Is<Transaction>(a => 
            a != null &&
            a.AccountId == command.AccountId && 
            a.CategoryId == command.CategoryId &&
            a.Amount == command.Amount &&
            a.Id == result.Value));

        await _unitOfWork.Received(1).SaveEntitiesAsync(Arg.Any<CancellationToken>());
    }
}