using System.Data;
using HeyBanking.App.Common.Exceptions;
using HeyBanking.App.Common.Interfaces;
using HeyBanking.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HeyBanking.App.Commands.Withdraw
{
    public class WithdrawCommand: IRequest<WithdrawDto>
    {
        public Guid AccountId { get; set; }

        public decimal Amount { get; set; }
    }

    public class WithdrawCommandHandlerIRequestHandler : IRequestHandler<WithdrawCommand, WithdrawDto>
    {
        private readonly IApplicationDbContext _context;

        public WithdrawCommandHandlerIRequestHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<WithdrawDto> Handle(WithdrawCommand request, CancellationToken cancellationToken)
        {
            using (var dbContextTransaction = _context.Database.BeginTransaction(IsolationLevel.RepeatableRead))
            {
                var entity = await _context.Accounts
                .SingleOrDefaultAsync(x => x.Id == request.AccountId, cancellationToken);

                if (entity == null)
                {
                    throw new NotFoundException(nameof(Account), request.AccountId);
                }

                entity.Amount -= request.Amount;

                await _context.SaveChangesAsync(cancellationToken);

                await dbContextTransaction.CommitAsync();

                return new WithdrawDto
                {
                    AccountId = request.AccountId,
                    WithdrawAmount = request.Amount,
                    Balance = entity.Amount
                };
            }
        }
    }
}
