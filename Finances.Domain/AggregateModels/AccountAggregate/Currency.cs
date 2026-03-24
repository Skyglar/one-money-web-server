
using Finances.Domain.Exceptions;
using OneMoney.Common.SeedWork;

namespace Finances.Domain.AggregateModels.AccountAggregate;

public sealed class Currency : Entity, IAggregateRoot {
    public string Name { get; private set; } = string.Empty;

    public string Code { get; private set; } = string.Empty;

    private Currency() { }

    public Currency(string name, string code) {
        if (string.IsNullOrWhiteSpace(name)) throw new AccountException("Name is required");
        if (string.IsNullOrWhiteSpace(code)) throw new AccountException("Code is required");
        
        Id = Guid.NewGuid();
        Name = name;
        Code = code;
    }
}