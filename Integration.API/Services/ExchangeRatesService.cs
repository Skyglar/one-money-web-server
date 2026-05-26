using System.Net.Http.Json;
using Integration.API.Configuration;
using Integration.API.Models;
using Integration.API.Services.ExchangeRates;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace Integration.API.Services;

public sealed class ExchangeRatesService(
    ILogger<ExchangeRatesService> logger,
    HttpClient httpClient,
    IMemoryCache cache,
    IOptions<ExchangeRatesOptions> options) : IExchangeRateService {
    public async Task<ExchangeRateResult> GetExchangeRates(
        string baseCurrency,
        IEnumerable<string> quotes,
        CancellationToken cancellationToken = default) {
        if (!CurrencyCodeNormalizer.TryNormalizeBaseCurrency(baseCurrency, out var normalizedBase, out var baseError)) {
            return ExchangeRateResult.ValidationError(baseError ?? "Base currency is invalid.");
        }

        if (!CurrencyCodeNormalizer.TryNormalizeQuotes(quotes, out var normalizedQuotes, out var quotesError)) {
            return ExchangeRateResult.ValidationError(quotesError ?? "Quote currencies are invalid.");
        }

        var cacheKey = $"exchange-rates:{normalizedBase}:{string.Join(',', normalizedQuotes)}";

        if (cache.TryGetValue(cacheKey, out ExchangeRatesResponse? cachedResponse) && cachedResponse is not null) {
            logger.LogInformation(
                "Got exchange rates for {BaseCurrency} and {QuoteCurrencies} from cache",
                normalizedBase,
                normalizedQuotes);
            
            return ExchangeRateResult.Success(cachedResponse);
        }

        var query = QueryString.Create([
            new KeyValuePair<string, string?>("base", normalizedBase),
            new KeyValuePair<string, string?>("quotes", string.Join(',', normalizedQuotes))
        ]);

        var url = $"v2/rates{query}";

        try {
            logger.LogInformation(
                "Fetching exchange rates for {BaseCurrency} and {QuoteCurrencies}",
                normalizedBase,
                normalizedQuotes);

            using var responseMessage = await httpClient.GetAsync(
                url,
                cancellationToken);

            if (!responseMessage.IsSuccessStatusCode) {
                logger.LogWarning(
                    "Exchange rates provider returned {StatusCode}",
                    responseMessage.StatusCode);

                return ExchangeRateResult.UpstreamUnavailable(
                    "Exchange rates provider is unavailable.");
            }

            var providerResponse = await responseMessage.Content
                .ReadFromJsonAsync<List<FrankfurterRatesResponse>>(cancellationToken);

            if (providerResponse is null || providerResponse.Count == 0) {
                return ExchangeRateResult.UpstreamBadResponse(
                    "Exchange rates provider returned an unexpected response.");
            }

            var rates = providerResponse.ToDictionary(
                rate => rate.Quote.ToUpperInvariant(),
                rate => 1m / rate.Rate);
            
            var missingQuotes = normalizedQuotes
                .Where(quote => !rates.ContainsKey(quote))
                .ToArray();

            if (missingQuotes.Length > 0) {
                return ExchangeRateResult.UpstreamBadResponse(
                    $"Exchange rates provider did not return rates for: {string.Join(", ", missingQuotes)}.");
            }

            var result = new ExchangeRatesResponse(
                providerResponse[0].Base.ToUpperInvariant(),
                providerResponse[0].Date,
                rates);
            
            cache.Set(
                cacheKey,
                result,
                TimeSpan.FromMinutes(options.Value.CacheDurationMinutes));

            return ExchangeRateResult.Success(result);
        } catch (HttpRequestException ex) {
            logger.LogError(
                ex,
                "Failed to fetch exchange rates");

            return ExchangeRateResult.UpstreamUnavailable(
                "Exchange rates provider is unavailable.");
        } catch (TaskCanceledException ex) when (!cancellationToken.IsCancellationRequested) {
            logger.LogError(
                ex,
                "Exchange rate request timed out");

            return ExchangeRateResult.Timeout(
                "Exchange rates provider request timed out.");
        } catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested) {
            return ExchangeRateResult.Canceled(
                "Exchange rates request was canceled.");
        } catch (System.Text.Json.JsonException ex) {
            logger.LogError(
                ex,
                "Exchange rates provider returned invalid JSON");

            return ExchangeRateResult.UpstreamBadResponse(
                "Exchange rates provider returned an unexpected response.");
        }
    }
}