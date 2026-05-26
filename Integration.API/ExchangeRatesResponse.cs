namespace Integration.API;

public sealed record ExchangeRatesResponse(
    string Base,
    DateOnly Date,
    IReadOnlyDictionary<string, decimal> Rates);
