using Finances.API.Extensions;
using Finances.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddOpenApi();
builder.Services.AddApplicationServices();
builder.Services.AddMessaging(builder.Configuration);
builder.Services.AddPersistence(builder.Configuration);

var app = builder.Build();

Console.WriteLine("app is starting" + " " + app.Environment.IsDevelopment());

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.MapOpenApi();
    app.Services.ApplyMigrations();
}

app.UseHttpsRedirection();

app.Run();