using MediatR;
using OneMoney.Common.Primitives;
using OneMoney.Common.SeedWork;
using Transactions.Domain.AggregateModels;

namespace Transactions.Application.Commands;

public sealed class CreateTransactionCommandHandler(
    ITransactionRepository transactionRepository,
    IUnitOfWork unitOfWork) 
    : IRequestHandler<CreateTransactionCommand, Result<Guid>> {
    
    public async Task<Result<Guid>> Handle(CreateTransactionCommand request, CancellationToken cancellationToken) {

        var transaction = new Transaction(
            request.Amount,
            request.AccountId,
            request.CategoryId,
            request.Currency,
            request.Description);
        
        transactionRepository.Add(transaction);
        
        await unitOfWork.SaveEntitiesAsync(cancellationToken);
        
        return Result<Guid>.Success(transaction.Id);
    }
}