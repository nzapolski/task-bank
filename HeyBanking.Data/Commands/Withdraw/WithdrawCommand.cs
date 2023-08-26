using HeyBanking.App.Common.Exceptions;
using HeyBanking.App.Common.Interfaces;
using HeyBanking.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HeyBanking.App.Commands.Withdraw
{
    public class WithdrawCommand: IRequest<decimal>
    {
        public Guid AccountId { get; set; }

        public decimal Ammount { get; set; }
    }

    public class WithdrawCommandHandlerIRequestHandler : IRequestHandler<WithdrawCommand, decimal>
    {
        private readonly IApplicationDbContext _context;

        public WithdrawCommandHandlerIRequestHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<decimal> Handle(WithdrawCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Accounts
                .SingleOrDefaultAsync(x => x.Id == request.AccountId, cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Account), request.AccountId);
            }

            entity.Ammount -= request.Ammount;

            await _context.SaveChangesAsync(cancellationToken);

            return entity.Ammount;
        }
    }
}
