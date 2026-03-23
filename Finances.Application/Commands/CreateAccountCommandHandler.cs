using Finances.Domain.AggregateModels.AccountAggregate;
using Finances.Domain.Exceptions;
using MediatR;
using OneMoney.Common.SeedWork;

namespace Finances.Application.Commands;

public class CreateAccountCommandHandler : IRequestHandler<CreateAccountCommand, Guid> {
    private readonly IAccountRepository _accountRepository;
    private readonly ICurrencyRepository _currencyRepository;
    private readonly IUnitOfWork _uow;

    public CreateAccountCommandHandler(
        IAccountRepository accountRepository,
        ICurrencyRepository currencyRepository,
        IUnitOfWork uow) {
        _accountRepository = accountRepository;
        _currencyRepository = currencyRepository;
        _uow = uow;
    }

    public async Task<Guid> Handle(CreateAccountCommand request, CancellationToken ct) {
        Currency currency = await _currencyRepository.FindByCodeAsync(request.Currency, ct);

        if (currency == null) {
            throw new AccountException("Currency not found");
        }

        var account = new Account(
            request.Name,
            request.InitialAmount,
            request.Description,
            request.AccountType,
            currency
        );

        _accountRepository.Add(account);

        await _uow.SaveChangesAsync(ct);

        return account.Id;
    }
}