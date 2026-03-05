namespace Finances.Application.Commands;

public record CreateAccountCommand(string Name, decimal Amount, string CurrencyCode);
