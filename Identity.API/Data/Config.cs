using Duende.IdentityServer.Models;

namespace Identity.API.Data;

public static class Config {
    public static IEnumerable<IdentityResource> IdentityResources => [
        new IdentityResources.OpenId(),
        new IdentityResources.Profile()
    ];

    public static IEnumerable<ApiScope> ApiScopes => [
        new ApiScope(name: "transactions.api", displayName: "Access to OneMoney Transactions Microservice"),
        new ApiScope(name: "finances.api", displayName: "Access to OneMoney Finance Microservice"),
        new ApiScope(name: "analytics.api", displayName: "Access to Analytics Microservice")
    ];

    public static IEnumerable<ApiResource> ApiResources => [
        new ApiResource("transactions.api", "Transactions API") {
            Scopes = { "transactions.api" }
        },
        new ApiResource("finances.api", "Finances API") {
            Scopes = { "finances.api" }
        },
        new ApiResource("analytics.api", "Analytics API") {
            Scopes = { "analytics.api" }
        }
    ];

    public static IEnumerable<Client> Clients => [
        new Client
        {
            ClientId = "onemoney-web-client",
            ClientName = "OneMoney Web Application",
            AllowedGrantTypes = GrantTypes.Code,
            RequireClientSecret = false,
            RequirePkce = true,
            RedirectUris = ["http://localhost:3000/callback"],
            PostLogoutRedirectUris = ["http://localhost:3000/"],
            AllowedCorsOrigins = ["http://localhost:3000"],
            AllowedScopes = [
                "openid",
                "profile",
                "transactions.api",
                "finances.api",
                "analytics.api"
            ]
        }
    ];
}
