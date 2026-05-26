using Integration.API.Configuration;
using Integration.API.Services;
using Microsoft.Extensions.Options;

namespace Integration.API.Extensions;

public static class ExchangeRateIntegrationExtensions {
    public static IServiceCollection AddExchangeRateIntegration(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services
            .AddOptions<ExchangeRatesOptions>()
            .Bind(configuration.GetSection(ExchangeRatesOptions.SectionName))
            .ValidateDataAnnotations()
            .Validate(options => options.BaseUrl.IsAbsoluteUri, "ExchangeRates:BaseUrl must be an absolute URI.")
            .ValidateOnStart();

        services.AddMemoryCache();

        services
            .AddHttpClient<IExchangeRateService, ExchangeRatesService>((serviceProvider, client) => {
                var options = serviceProvider.GetRequiredService<IOptions<ExchangeRatesOptions>>().Value;
                client.BaseAddress = options.BaseUrl;
                client.Timeout = TimeSpan.FromSeconds(options.TimeoutSeconds);
            })
            .AddStandardResilienceHandler();

        services.AddHealthChecks();

        return services;
    }
}
