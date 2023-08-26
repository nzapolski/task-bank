using System.Data;
using HeyBanking.App.Commands.Withdraw;
using HeyBanking.App.Common.Exceptions;
using HeyBanking.App.Common.Interfaces;
using HeyBanking.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HeyBanking.App.Commands.DeleteAccount
{
    public class WithdrawAndDeleteAccountCommand : IRequest<WithdrawDto>
    {
        public Guid AccountId { get; set; }
    }

    public class WithdrawAndDeleteAccountCommandHandler : IRequestHandler<WithdrawAndDeleteAccountCommand, WithdrawDto>
    {
        private readonly IApplicationDbContext _context;

        public WithdrawAndDeleteAccountCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<WithdrawDto> Handle(WithdrawAndDeleteAccountCommand request, CancellationToken cancellationToken)
        {
            using (var dbContextTransaction = _context.Database.BeginTransaction(IsolationLevel.RepeatableRead))
            {
                var account = await _context.Accounts
                .SingleOrDefaultAsync(x => x.Id == request.AccountId, cancellationToken);

                if (account == null)
                {
                    throw new NotFoundException(nameof(Account), request.AccountId);
                }

                // for race condition test
                // await Task.Delay(60 * 1000);

                _context.Accounts.Remove(account);

                await _context.SaveChangesAsync(cancellationToken);

                dbContextTransaction.Commit();

                return new WithdrawDto
                {
                    AccountId = request.AccountId,
                    WithdrawAmount = account.Amount,
                    Balance = 0
                };
            }
        }
    }
}
