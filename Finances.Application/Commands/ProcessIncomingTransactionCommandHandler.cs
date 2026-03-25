using Finances.Domain.AggregateModels.AccountAggregate;
using Finances.Domain.AggregateModels.CategoryAggregate;
using Finances.Domain.Exceptions;
using MediatR;
using OneMoney.Common.Primitives;
using OneMoney.Common.SeedWork;

namespace Finances.Application.Commands;

public sealed class ProcessIncomingTransactionCommandHandler(
    IAccountRepository accountRepository,
    ICategoryRepository categoryRepository,
    IUnitOfWork unitOfWork)
    : IRequestHandler<ProcessIncomingTransactionCommand, Result> {
    public async Task<Result> Handle(ProcessIncomingTransactionCommand request, CancellationToken cancellationToken) {
        var account = await accountRepository.GetByIdAsync(request.AccountId, cancellationToken);
        if (account == null) return Result.Failure("Invalid account.");

        var category = await categoryRepository.GetByIdAsync(request.CategoryId, cancellationToken);
        if (category == null) return Result.Failure("Invalid category.");

        try 
        {
            account.Debit(request.Amount, request.TransactionId);
        }
        catch (AccountException ex) 
        {
            return Result.Failure(ex.Message);
        }
        
        accountRepository.Update(account);
        
        await unitOfWork.SaveChangesAsync(cancellationToken);
        
        return Result.Success();
    }
}