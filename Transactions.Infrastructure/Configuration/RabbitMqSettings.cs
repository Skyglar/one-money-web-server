namespace Transactions.Infrastructure.Configuration;

public class RabbitMqSettings {
    public string Host { get; init; } = string.Empty;
    public string VirtualHost { get; init; } = string.Empty;
    public string Username { get; init; } = string.Empty;
    public string Password { get; init; } = string.Empty;
}