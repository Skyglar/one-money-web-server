using System;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Serialization;

// Namespaces from your project
using common;
using common.Helpers;
using domain.Entities;
using Infrastructure.Database;
using Infrastructure.IdentityConfiguration;
using one_money_web_server.Extensions;

var builder = WebApplication.CreateBuilder(args);

// --- 1. Configuration & Initializations ---
// .NET 10 automatically loads appsettings.json and Environment Variables.
var configuration = builder.Configuration;
var env = builder.Environment;

FolderManager.InitializeFolderManager(env.ContentRootPath);

// Database Path Setup
var folder = Environment.SpecialFolder.LocalApplicationData;
var path = Environment.GetFolderPath(folder);
var dbPath = Path.Join(path, configuration.GetConnectionString("DatabaseName"));
var identityDbPath = Path.Join(path, configuration.GetConnectionString("IdentityDatabaseName"));

// --- 2. Configure Services (Old ConfigureServices) ---

builder.Services.AddControllers(o => o.Filters.Add(new AuthorizeFilter()))
    .AddNewtonsoftJson(options => {
        options.SerializerSettings.ContractResolver = new DefaultContractResolver();
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
    });

builder.Services.AddSwaggerGen();
builder.Services.AddCors();

// Database Contexts
builder.Services.AddDbContext<OneMoneyContext>(options => 
    options.UseSqlite($"Data Source={dbPath}"));
builder.Services.AddDbContext<ApplicationIdentityContext>(options => 
    options.UseSqlite($"Data Source={identityDbPath}"));

// Identity
builder.Services.AddIdentity<User, IdentityRole>(options => {
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 6;
    options.Password.RequireUppercase = false;
    options.User.RequireUniqueEmail = true;
    options.User.AllowedUserNameCharacters = AuthConfig.AllowedUserNameCharacters;
})
.AddEntityFrameworkStores<ApplicationIdentityContext>()
.AddDefaultTokenProviders();

// Authentication
builder.Services.AddAuthentication(options => {
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options => {
    options.TokenValidationParameters = new TokenValidationParameters {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = AuthConfig.ISSUER,
        ValidAudience = AuthConfig.AUDIENCE,
        IssuerSigningKey = AuthConfig.GetSymmetricSecurityKey(),
    };
});

// Custom Extension Method
builder.Services.RegisterServices(configuration);

var app = builder.Build();

// --- 3. Configure Pipeline (Old Configure) ---

app.UseSwagger();
app.UseSwaggerUI(c => {
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "One Money API v1");
});

if (app.Environment.IsDevelopment()) {
    app.UseDeveloperExceptionPage();
}

app.UseRouting();

// CORS must be between UseRouting and UseAuthentication
app.UseCors(b => b.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

app.UseAuthentication();
app.UseAuthorization();

// Static Files
app.UseStaticFiles(new StaticFileOptions {
    RequestPath = "/Data",
    FileProvider = new PhysicalFileProvider(Path.Combine(env.ContentRootPath, "Data")),
    ServeUnknownFileTypes = true
});

app.MapControllers();

app.Run();