using MediatR;
using Microsoft.AspNetCore.Mvc;
using OneMoney.Common.Primitives;
using Transactions.Application.Commands;

namespace Transactions.API.Endpoints;

public static class TransactionEndpoints {
    public static void MapTransactionEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/transactions")
            .WithTags("Transactions"); // Groups them in Swagger

        group.MapPost("/", CreateTransaction);
    }

    private static async Task<IResult> CreateTransaction(
        [FromBody] CreateTransactionCommand command, 
        IMediator mediator,
        CancellationToken ct)
    {
        // We use MediatR to keep the Endpoint "Thin"
        Result result = await mediator.Send(command, ct);

        return result.IsSuccess 
            ? Results.Ok(result) 
            : Results.BadRequest(result.Error);
    }
}