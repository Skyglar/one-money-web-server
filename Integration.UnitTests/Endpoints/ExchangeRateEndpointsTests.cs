using Integration.API;
using Integration.API.Endpoints;
using Integration.API.Services;
using Integration.API.Services.ExchangeRates;
using Microsoft.AspNetCore.Http;
using NSubstitute;

namespace Integration.UnitTests.Endpoints;

public class ExchangeRateEndpointsTests {
    [Fact]
    public async Task GetExchangeRates_ServiceSuccess_ReturnsOk()
    {
        var service = Substitute.For<IExchangeRateService>();
        service.GetExchangeRates("USD", Arg.Is<IEnumerable<string>>(quotes =>
                quotes != null && quotes.SequenceEqual(new[] { "EUR", "UAH" })),
            Arg.Any<CancellationToken>())
            .Returns(ExchangeRateResult.Success(new ExchangeRatesResponse(
                "USD",
                new DateOnly(2026, 5, 9),
                new Dictionary<string, decimal> {
                    ["EUR"] = 0.91m,
                    ["UAH"] = 41.2m
                })));

        var result = await ExchangeRateEndpoints.GetExchangeRates(
            "USD",
            "EUR,UAH",
            service,
            CancellationToken.None);

        AssertStatusCode(result, StatusCodes.Status200OK);
    }

    [Fact]
    public async Task GetExchangeRates_ServiceValidationError_ReturnsBadRequest()
    {
        var service = Substitute.For<IExchangeRateService>();
        service.GetExchangeRates(Arg.Any<string>(), Arg.Any<IEnumerable<string>>(), Arg.Any<CancellationToken>())
            .Returns(ExchangeRateResult.ValidationError("At least one quote currency is required."));

        var result = await ExchangeRateEndpoints.GetExchangeRates(
            "USD",
            null,
            service,
            CancellationToken.None);

        AssertStatusCode(result, StatusCodes.Status400BadRequest);
    }

    [Theory]
    [InlineData(ExchangeRateResultStatus.UpstreamBadResponse, StatusCodes.Status502BadGateway)]
    [InlineData(ExchangeRateResultStatus.UpstreamUnavailable, StatusCodes.Status503ServiceUnavailable)]
    [InlineData(ExchangeRateResultStatus.Timeout, StatusCodes.Status503ServiceUnavailable)]
    public async Task GetExchangeRates_ServiceFailure_ReturnsMappedStatusCode(
        ExchangeRateResultStatus status,
        int expectedStatusCode)
    {
        var service = Substitute.For<IExchangeRateService>();
        service.GetExchangeRates(Arg.Any<string>(), Arg.Any<IEnumerable<string>>(), Arg.Any<CancellationToken>())
            .Returns(new ExchangeRateResult(status, null, "Provider failed."));

        var result = await ExchangeRateEndpoints.GetExchangeRates(
            "USD",
            "EUR",
            service,
            CancellationToken.None);

        AssertStatusCode(result, expectedStatusCode);
    }

    private static void AssertStatusCode(IResult result, int expectedStatusCode)
    {
        var statusCodeResult = Assert.IsAssignableFrom<IStatusCodeHttpResult>(result);
        Assert.Equal(expectedStatusCode, statusCodeResult.StatusCode);
    }
}
