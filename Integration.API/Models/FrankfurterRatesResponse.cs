using System.Text.Json.Serialization;

namespace Integration.API.Models;

public sealed record FrankfurterRatesResponse(
    [property: JsonPropertyName("date")] DateOnly Date,
    [property: JsonPropertyName("base")] string Base,
    [property: JsonPropertyName("quote")] string Quote,
    [property: JsonPropertyName("rate")] decimal Rate);
