using Finances.Domain.AggregateModels.AccountAggregate;
using Finances.Domain.Exceptions;

namespace Finances.UnitTests.Domain;

public class AccountAggregateTests {

    [Fact]
    public void Constructor_WithValidParams_ShouldCreateAccount() {
        // Arrange
        var name = "Savings";
        var amount = 100.50m;
        var description = "My primary savings";
        var accountType = AccountType.Savings;
        var currency = new Currency("USD", "usd");

        // Act
        var account = new Account(name, amount, description, accountType, currency);

        // Assert
        Assert.Equal(name, account.Name);
        Assert.Equal(amount, account.Amount);
        Assert.Equal(currency.Id, account.CurrencyId);
        Assert.Equal(accountType, account.AccountType);
    }
    
    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    public void Constructor_WithInvalidName_ShouldThrowAccountException(string invalidName)
    {
        // Arrange
        var currency = new Currency("USD", "$");

        // Act & Assert
        var exception = Assert.Throws<AccountException>(() => 
            new Account(invalidName, 10, "Desc", AccountType.Savings, currency));
        
        Assert.Equal("Name is required", exception.Message);
    }
    
    [Fact]
    public void Constructor_WithNullCurrency_ShouldThrowAccountException()
    {
        // Act & Assert
        Assert.Throws<AccountException>(() => 
            new Account("Test", 10, "Desc", AccountType.Savings, null!));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-100)]
    public void Constructor_WithInvalidAmount_ShouldThrowAccountException(decimal invalidAmount)
    {
        // Arrange
        var currency = new Currency("USD", "$");

        // Act & Assert
        var exception = Assert.Throws<AccountException>(() => 
            new Account("Test", invalidAmount, "Desc", AccountType.Savings, currency));

        Assert.Equal("Initial amount must be greater than zero", exception.Message);
    }
}