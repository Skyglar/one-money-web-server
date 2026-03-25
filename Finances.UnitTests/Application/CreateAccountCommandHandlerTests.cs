using Finances.Application.Commands;
using Finances.Domain.AggregateModels.AccountAggregate;
using NSubstitute;
using OneMoney.Common.SeedWork;

namespace Finances.UnitTests.Application;

public class CreateAccountCommandHandlerTests {
    private readonly IAccountRepository _accountRepo;
    private readonly ICurrencyRepository _currencyRepo;
    private readonly IUnitOfWork _uow;
    private readonly CreateAccountCommandHandler _handler;

    public CreateAccountCommandHandlerTests()
    {
        // 1. Create the Mocks
        _accountRepo = Substitute.For<IAccountRepository>();
        _currencyRepo = Substitute.For<ICurrencyRepository>();
        _uow = Substitute.For<IUnitOfWork>();

        // 2. Initialize the Handler with Mocks
        _handler = new CreateAccountCommandHandler(_accountRepo, _currencyRepo, _uow);
    }

    [Fact]
    public async Task Handle_ValidRequest_ShouldCreateAccountAndReturnId()
    {
        // Arrange
        var command = new CreateAccountCommand(
            "My Savings", 
            AccountType.Savings,
            1000m, 
            "USD", 
            "Vacation fund"
        );

        var fakeCurrency = new Currency("USD", "$");
        
        // Tell the mock what to return when the handler asks for a currency
        _currencyRepo.FindByCodeAsync("USD", Arg.Any<CancellationToken>())
            .Returns(fakeCurrency);

        // Act
        var resultId = await _handler.Handle(command, CancellationToken.None);

        // Assert
        
        // A. Verify the ID returned is not empty
        Assert.NotEqual(Guid.Empty, resultId);

        // B. Verify the repository received an Account with correct data
        _accountRepo.Received(1).Add(Arg.Is<Account>(a => 
            a != null &&
            a.Name == command.Name && 
            a.Balance == command.InitialAmount &&
            a.Id == resultId));

        // C. Verify that SaveChanges was actually called
        await _uow.Received(1).SaveChangesAsync(Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task Handle_CurrencyNotFound_ShouldThrowException()
    {
        // Arrange
        var command = new CreateAccountCommand("Test", AccountType.Savings,100, "INVALID", "Desc");
        
        // Return null to simulate currency not found
        _currencyRepo.FindByCodeAsync("INVALID", Arg.Any<CancellationToken>())
            .Returns((Currency)null!);

        // Act & Assert
        // This confirms your domain logic or handler handles missing data
        await Assert.ThrowsAnyAsync<Exception>(() => 
            _handler.Handle(command, CancellationToken.None));
    }
}
