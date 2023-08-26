using HeyBanking.App.Common.Exceptions;
using HeyBanking.App.Common.Interfaces;
using HeyBanking.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HeyBanking.App.Commands.Deposit
{
    public class DepositCommand : IRequest
    {
        public Guid AccountId { get; set; }
        public decimal Deposit { get; set; }
    }

    public class DepositCommandHandler : IRequestHandler<DepositCommand>
    {
        private readonly IApplicationDbContext _context;

        public DepositCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Handle(DepositCommand request, CancellationToken cancellationToken)
        {
            var account = await _context.Accounts
                .SingleOrDefaultAsync(x => x.Id == request.AccountId, cancellationToken);

            if (account == null)
            {
                throw new NotFoundException(nameof(Account), request.AccountId);
            }

            account.Ammount += request.Deposit;

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
