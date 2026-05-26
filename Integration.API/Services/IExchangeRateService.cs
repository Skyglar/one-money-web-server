using Integration.API.Services.ExchangeRates;

namespace Integration.API.Services;

public interface IExchangeRateService {
    Task<ExchangeRateResult> GetExchangeRates(
        string baseCurrency,
        IEnumerable<string> quotes,
        CancellationToken cancellationToken = default);
}
