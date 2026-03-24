using Transactions.Domain.AggregateModels;
using Transactions.Domain.Exceptions;

namespace Transactions.UnitTests.Domain;

public class TransactionAggregateTests {
    private readonly decimal _validAmount;
    private readonly Guid _accountId;
    private readonly Guid _categoryId;
    private readonly string _currencyCode;
    private readonly string _description;

    public TransactionAggregateTests() {
        _validAmount = 100m;
        _accountId = Guid.NewGuid();
        _categoryId = Guid.NewGuid();
        _currencyCode = "usd";
        _description = "Description";
    }

    [Fact]
    public void Constructor_WithValidParams_ShouldCreateTransaction() {
        // Arrange & Act
        var transaction = new Transaction(
            _validAmount,
            _accountId,
            _categoryId,
            _currencyCode,
            _description);

        // Assert
        Assert.Equal(_validAmount, transaction.Amount);
        Assert.Equal(_accountId, transaction.AccountId);
        Assert.Equal(_categoryId, transaction.CategoryId);
        Assert.Equal(_currencyCode, transaction.CurrencyCode);
        Assert.Equal(_description, transaction.Description);
        Assert.Equal(TransactionStatus.Pending, transaction.Status);
        Assert.NotEqual(Guid.Empty, transaction.Id);
    }

    [Theory]
    [InlineData("0")]
    [InlineData("-100")]
    public void Constructor_WithInvalidAmount_ShouldThrowTransactionException(string invalidAmountStr) {
        var invalidAmount = decimal.Parse(invalidAmountStr);
        // Act & Assert
        var exception = Assert.Throws<TransactionException>(() =>
            new Transaction(invalidAmount, _accountId, _categoryId, _currencyCode, _description));

        Assert.Equal(ExceptionMessages.AMOUNT_CANNOT_BE_ZERO_OR_NEGATIVE, exception.Message);
    }

    [Fact]
    public void Constructor_WithInvalidAccountId_ShouldThrowTransactionException() {
        var exception = Assert.Throws<TransactionException>(() =>
            new Transaction(_validAmount, Guid.Empty, _categoryId, _currencyCode, _description));

        Assert.Equal(ExceptionMessages.ACCOUNT_ID_IS_INVALID, exception.Message);
    }

    [Fact]
    public void Constructor_WithInvalidCategoryId_ShouldThrowTransactionException() {
        var exception = Assert.Throws<TransactionException>(() =>
            new Transaction(_validAmount, _accountId, Guid.Empty, _currencyCode, _description));

        Assert.Equal(ExceptionMessages.CATEGORY_ID_IS_INVALID, exception.Message);
    }
}