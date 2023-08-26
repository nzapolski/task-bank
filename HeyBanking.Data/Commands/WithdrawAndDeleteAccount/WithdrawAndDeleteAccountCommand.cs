using HeyBanking.App.Common.Exceptions;
using HeyBanking.App.Common.Interfaces;
using HeyBanking.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HeyBanking.App.Commands.DeleteAccount
{
    public class WithdrawAndDeleteAccountCommand : IRequest<decimal>
    {
        public Guid AccountId { get; set; }
    }

    public class WithdrawAndDeleteAccountCommandHandler : IRequestHandler<WithdrawAndDeleteAccountCommand, decimal>
    {
        private readonly IApplicationDbContext _context;

        public WithdrawAndDeleteAccountCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<decimal> Handle(WithdrawAndDeleteAccountCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Accounts
                .SingleOrDefaultAsync(x => x.Id == request.AccountId, cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Account), request.AccountId);
            }

            _context.Accounts.Remove(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return entity.Ammount;
        }
    }
}
