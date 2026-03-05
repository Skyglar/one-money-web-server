using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// --- 1. Configuration & Initializations ---
// .NET 10 automatically loads appsettings.json and Environment Variables.
var configuration = builder.Configuration;
var env = builder.Environment;

// --- 2. Configure Services (Old ConfigureServices) ---

builder.Services.AddControllers(o => o.Filters.Add(new AuthorizeFilter()))
    .AddNewtonsoftJson(options => {
        options.SerializerSettings.ContractResolver = new DefaultContractResolver();
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
    });

builder.Services.AddSwaggerGen();
builder.Services.AddCors();

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