using System.Text.RegularExpressions;

namespace Integration.API.Services.ExchangeRates;

public static partial class CurrencyCodeNormalizer {
    public static bool TryNormalizeBaseCurrency(string? value, out string currency, out string? error)
    {
        return TryNormalize(value, "Base currency", out currency, out error);
    }

    public static bool TryNormalizeQuotes(IEnumerable<string>? values, out string[] quotes, out string? error)
    {
        quotes = [];

        if (values is null) {
            error = "At least one quote currency is required.";
            return false;
        }

        var normalized = new List<string>();

        foreach (var value in values) {
            if (!TryNormalize(value, "Quote currency", out var quote, out error)) {
                return false;
            }

            if (!normalized.Contains(quote, StringComparer.Ordinal)) {
                normalized.Add(quote);
            }
        }

        if (normalized.Count == 0) {
            error = "At least one quote currency is required.";
            return false;
        }

        quotes = normalized.ToArray();
        error = null;
        return true;
    }

    private static bool TryNormalize(string? value, string fieldName, out string currency, out string? error)
    {
        currency = string.Empty;

        if (string.IsNullOrWhiteSpace(value)) {
            error = $"{fieldName} is required.";
            return false;
        }

        var normalized = value.Trim().ToUpperInvariant();

        if (!CurrencyCodeRegex().IsMatch(normalized)) {
            error = $"{fieldName} must be a three-letter currency code.";
            return false;
        }

        currency = normalized;
        error = null;
        return true;
    }

    [GeneratedRegex("^[A-Z]{3}$")]
    private static partial Regex CurrencyCodeRegex();
}
