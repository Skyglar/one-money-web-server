using Integration.API.Endpoints;
using Integration.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddExchangeRateIntegration(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment()) {
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapHealthChecks("/health");
app.MapExchangeRateEndpoints();

app.Run();
