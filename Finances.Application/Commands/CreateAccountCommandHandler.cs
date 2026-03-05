using Finances.Domain.AggregateModels.AccountAggregate;

namespace Finances.Application.Commands;

// Will use MediatR in the future 
public sealed class CreateAccountCommandHandler(IAccountRepository accountRepository) {
    
    public async Task<bool> Handle(CreateAccountCommand command, CancellationToken cancellationToken)
    {
        Account? account = await accountRepository.FindAsync(command.Name);
        if (account != null)
        {
            return false;
        }
        
        await accountRepository.AddAsync(new Account {Name = command.Name, Amount = command.Amount });
        
        return await accountRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
    }
}