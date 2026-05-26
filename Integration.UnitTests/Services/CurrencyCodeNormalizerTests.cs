using Integration.API.Services.ExchangeRates;

namespace Integration.UnitTests.Services;

public class CurrencyCodeNormalizerTests {
    [Fact]
    public void TryNormalizeBaseCurrency_LowercaseCode_ReturnsUppercaseCode()
    {
        var isValid = CurrencyCodeNormalizer.TryNormalizeBaseCurrency(
            "usd",
            out var currency,
            out var error);

        Assert.True(isValid);
        Assert.Equal("USD", currency);
        Assert.Null(error);
    }

    [Fact]
    public void TryNormalizeQuotes_DuplicateMixedCaseCodes_ReturnsDistinctUppercaseCodes()
    {
        var isValid = CurrencyCodeNormalizer.TryNormalizeQuotes(
            ["eur", "EUR", "uah"],
            out var quotes,
            out var error);

        Assert.True(isValid);
        Assert.Equal(["EUR", "UAH"], quotes);
        Assert.Null(error);
    }

    [Theory]
    [InlineData("")]
    [InlineData("US")]
    [InlineData("USDD")]
    [InlineData("12A")]
    public void TryNormalizeBaseCurrency_InvalidCode_ReturnsError(string baseCurrency)
    {
        var isValid = CurrencyCodeNormalizer.TryNormalizeBaseCurrency(
            baseCurrency,
            out _,
            out var error);

        Assert.False(isValid);
        Assert.NotNull(error);
    }

    [Fact]
    public void TryNormalizeQuotes_EmptyQuotes_ReturnsError()
    {
        var isValid = CurrencyCodeNormalizer.TryNormalizeQuotes(
            [],
            out var quotes,
            out var error);

        Assert.False(isValid);
        Assert.Empty(quotes);
        Assert.Equal("At least one quote currency is required.", error);
    }
}
