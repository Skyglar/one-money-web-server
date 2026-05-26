using System.Net;
using System.Text;
using Integration.API.Configuration;
using Integration.API.Services;
using Integration.API.Services.ExchangeRates;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;

namespace Integration.UnitTests.Services;

public class ExchangeRatesServiceTests {
    [Fact]
    public async Task GetExchangeRates_ValidRequest_BuildsEncodedProviderUrl()
    {
        var handler = new StubHttpMessageHandler(_ => new HttpResponseMessage(HttpStatusCode.OK) {
            Content = JsonContent("""
                [
                  {
                    "date": "2026-05-09",
                    "base": "USD",
                    "quote": "EUR",
                    "rate": 0.91
                  },
                  {
                    "date": "2026-05-09",
                    "base": "USD",
                    "quote": "UAH",
                    "rate": 41.2
                  }
                ]
                """)
        });
        var service = CreateService(handler);

        var result = await service.GetExchangeRates(
            "usd",
            ["eur", "EUR", "uah"],
            CancellationToken.None);

        Assert.Equal(ExchangeRateResultStatus.Success, result.Status);
        Assert.NotNull(handler.RequestUri);
        Assert.Equal("/v2/rates", handler.RequestUri.AbsolutePath);

        var query = QueryHelpers.ParseQuery(handler.RequestUri.Query);
        Assert.Equal("USD", query["base"]);
        Assert.Equal("EUR,UAH", query["quotes"]);
    }

    [Fact]
    public async Task GetExchangeRates_ProviderResponse_DeserializesRateRows()
    {
        var handler = new StubHttpMessageHandler(_ => new HttpResponseMessage(HttpStatusCode.OK) {
            Content = JsonContent("""
                [
                  {
                    "date": "2026-05-09",
                    "base": "USD",
                    "quote": "EUR",
                    "rate": 0.91
                  },
                  {
                    "date": "2026-05-09",
                    "base": "USD",
                    "quote": "UAH",
                    "rate": 41.2
                  }
                ]
                """)
        });
        var service = CreateService(handler);

        var result = await service.GetExchangeRates(
            "USD",
            ["EUR", "UAH"],
            CancellationToken.None);

        Assert.Equal(ExchangeRateResultStatus.Success, result.Status);
        Assert.NotNull(result.Value);
        Assert.Equal("USD", result.Value.Base);
        Assert.Equal(new DateOnly(2026, 5, 9), result.Value.Date);
        Assert.Equal(1m / 0.91m, result.Value.Rates["EUR"]);
        Assert.Equal(1m / 41.2m, result.Value.Rates["UAH"]);
    }

    [Fact]
    public async Task GetExchangeRates_EmptyQuotes_ReturnsValidationError()
    {
        var service = CreateService(new StubHttpMessageHandler(_ => new HttpResponseMessage(HttpStatusCode.OK)));

        var result = await service.GetExchangeRates(
            "USD",
            [],
            CancellationToken.None);

        Assert.Equal(ExchangeRateResultStatus.ValidationError, result.Status);
        Assert.Equal("At least one quote currency is required.", result.Error);
    }

    [Fact]
    public async Task GetExchangeRates_ProviderFailure_ReturnsUpstreamUnavailable()
    {
        var handler = new StubHttpMessageHandler(_ => new HttpResponseMessage(HttpStatusCode.BadGateway));
        var service = CreateService(handler);

        var result = await service.GetExchangeRates(
            "USD",
            ["EUR"],
            CancellationToken.None);

        Assert.Equal(ExchangeRateResultStatus.UpstreamUnavailable, result.Status);
    }

    private static ExchangeRatesService CreateService(StubHttpMessageHandler handler)
    {
        var httpClient = new HttpClient(handler) {
            BaseAddress = new Uri("https://example.test/")
        };

        var cache = new MemoryCache(new MemoryCacheOptions());
        var options = Options.Create(new ExchangeRatesOptions {
            BaseUrl = new Uri("https://example.test/"),
            TimeoutSeconds = 10,
            CacheDurationMinutes = 30
        });

        return new ExchangeRatesService(
            NullLogger<ExchangeRatesService>.Instance,
            httpClient,
            cache,
            options);
    }

    private static StringContent JsonContent(string json) =>
        new(json, Encoding.UTF8, "application/json");

    private sealed class StubHttpMessageHandler(
        Func<HttpRequestMessage, HttpResponseMessage> responseFactory) : HttpMessageHandler {
        public Uri? RequestUri { get; private set; }

        protected override Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            RequestUri = request.RequestUri;
            return Task.FromResult(responseFactory(request));
        }
    }
}
