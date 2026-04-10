FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Transactions.API/Transactions.API.csproj", "Transactions.API/"]
COPY ["OneMoney.Common/OneMoney.Common.csproj", "OneMoney.Common/"]
COPY ["Transactions.Application/Transactions.Application.csproj", "Transactions.Application/"]
COPY ["Transactions.Domain/Transactions.Domain.csproj", "Transactions.Domain/"]
COPY ["Transactions.Infrastructure/Transactions.Infrastructure.csproj", "Transactions.Infrastructure/"]
RUN dotnet restore "Transactions.API/Transactions.API.csproj"
COPY . .
WORKDIR "/src/Transactions.API"
RUN dotnet build "./Transactions.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Transactions.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Transactions.API.dll"]
