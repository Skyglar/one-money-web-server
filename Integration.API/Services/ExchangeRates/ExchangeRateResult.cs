namespace Integration.API.Services.ExchangeRates;

public enum ExchangeRateResultStatus {
    Success,
    ValidationError,
    UpstreamUnavailable,
    UpstreamBadResponse,
    Timeout,
    Canceled
}

public sealed record ExchangeRateResult(
    ExchangeRateResultStatus Status,
    ExchangeRatesResponse? Value,
    string? Error) {
    public static ExchangeRateResult Success(ExchangeRatesResponse value) =>
        new(ExchangeRateResultStatus.Success, value, null);

    public static ExchangeRateResult ValidationError(string error) =>
        new(ExchangeRateResultStatus.ValidationError, null, error);

    public static ExchangeRateResult UpstreamUnavailable(string error) =>
        new(ExchangeRateResultStatus.UpstreamUnavailable, null, error);

    public static ExchangeRateResult UpstreamBadResponse(string error) =>
        new(ExchangeRateResultStatus.UpstreamBadResponse, null, error);

    public static ExchangeRateResult Timeout(string error) =>
        new(ExchangeRateResultStatus.Timeout, null, error);

    public static ExchangeRateResult Canceled(string error) =>
        new(ExchangeRateResultStatus.Canceled, null, error);
}
