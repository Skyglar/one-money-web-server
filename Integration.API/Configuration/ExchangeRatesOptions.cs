using System.ComponentModel.DataAnnotations;

namespace Integration.API.Configuration;

public sealed class ExchangeRatesOptions {
    public const string SectionName = "ExchangeRates";

    [Required]
    public Uri BaseUrl { get; init; } = default!;

    [Range(1, 60)]
    public int TimeoutSeconds { get; init; } = 10;

    [Range(1, 1440)]
    public int CacheDurationMinutes { get; init; } = 30;
}
