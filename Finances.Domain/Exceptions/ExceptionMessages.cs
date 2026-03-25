namespace Finances.Domain.Exceptions;

public static class ExceptionMessages {
    public const string NAME_CANT_BE_NULL_OR_EMPTY = "Name is required";
    
    public const string CURRENCY_CANT_BE_NULL = "Currency is required";
    
    public const string INVALID_AMOUNT = "Invalid amount";
    
    public const string INSUFFICIENT_AMOUNT = "Insufficient funds";
}