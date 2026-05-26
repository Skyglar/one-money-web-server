using Integration.API.Services;
using Integration.API.Services.ExchangeRates;
using Microsoft.AspNetCore.Mvc;

namespace Integration.API.Endpoints;

public static class ExchangeRateEndpoints {
    public static void MapExchangeRateEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/exchange-rates")
            .WithTags("Exchange Rates");

        group.MapGet("/", GetExchangeRates)
            .WithName("GetExchangeRates");
    }

    public static async Task<IResult> GetExchangeRates(
        [FromQuery(Name = "base")] string? baseCurrency,
        [FromQuery] string? quotes,
        IExchangeRateService exchangeRateService,
        CancellationToken cancellationToken)
    {
        var requestedQuotes = SplitQuotes(quotes);
        var result = await exchangeRateService.GetExchangeRates(
            baseCurrency ?? string.Empty,
            requestedQuotes,
            cancellationToken);

        return result.Status switch {
            ExchangeRateResultStatus.Success => Results.Ok(result.Value),
            ExchangeRateResultStatus.ValidationError => Results.BadRequest(new { error = result.Error }),
            ExchangeRateResultStatus.UpstreamBadResponse => Results.Problem(
                detail: result.Error,
                statusCode: StatusCodes.Status502BadGateway),
            ExchangeRateResultStatus.Canceled => Results.StatusCode(StatusCodes.Status499ClientClosedRequest),
            _ => Results.Problem(
                detail: result.Error,
                statusCode: StatusCodes.Status503ServiceUnavailable)
        };
    }

    private static IEnumerable<string> SplitQuotes(string? quotes)
    {
        return quotes?
            .Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries) ?? [];
    }
}
