using Scalar.AspNetCore;
using Transactions.API.Endpoints;
using Transactions.API.Extensions;
using Transactions.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

// builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();
builder.Services.AddApplicationServices();
builder.Services.AddMessaging(builder.Configuration);
builder.Services.AddPersistence(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.MapOpenApi();
    app.Services.ApplyMigrations();
    app.MapScalarApiReference();
}

app.MapTransactionEndpoints();

app.Run();