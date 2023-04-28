using common;
using common.Helpers;
using common.Extensions;
using domain.Database;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Serialization;
using System;
using System.IO;
using domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;

namespace one_money_web_server {
    public class Startup {
        public IConfiguration Configuration { get; }

        private string DbPath { get; }
        private string IdentityDbPath { get; }

        private readonly IWebHostEnvironment _environment;

        public Startup(IWebHostEnvironment env) {
            IConfigurationBuilder builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables();
            Configuration = builder.Build();

            _environment = env;

            FolderManager.InitializeFolderManager(env.ContentRootPath);

            AppSettingsConfigurationManager.SetAppSettingsProperties(Configuration);

            Environment.SpecialFolder folder = Environment.SpecialFolder.LocalApplicationData;
            string path = Environment.GetFolderPath(folder);
            DbPath = Path.Join(path, Configuration.GetConnectionString("DatabaseName"));
            IdentityDbPath = Path.Join(path, Configuration.GetConnectionString("IdentityDatabaseName"));
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services) {
            services.AddControllers(o => o.Filters.Add(new AuthorizeFilter()));
            services.AddSwaggerGen();
            services.AddCors();

            services.AddMvc().AddNewtonsoftJson(options => {
                options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });

            services.AddDbContext<OneMoneyContext>(options => options.UseSqlite($"Data Source={DbPath}"));
            services.AddDbContext<ApplicationIdentityContext>(options => options.UseSqlite($"Data Source={IdentityDbPath}"));

            services.AddIdentity<User, IdentityRole>(options => {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 6;
                options.Password.RequireUppercase = false;
                options.User.RequireUniqueEmail = true;
                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
            })
            .AddEntityFrameworkStores<ApplicationIdentityContext>()
            .AddDefaultTokenProviders();

            services.AddAuthentication();

            services.RegisterServices(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            app.UseSwagger();
            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Random name");
            });

            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseStaticFiles(new StaticFileOptions {
                RequestPath = "/Data",
                FileProvider = new PhysicalFileProvider(_environment.ContentRootPath + "\\Data"),
                ServeUnknownFileTypes = true
            });

            app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
            });
        }
    }
}
