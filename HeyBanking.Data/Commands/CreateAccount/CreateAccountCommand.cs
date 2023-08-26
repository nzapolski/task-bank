using HeyBanking.App.Common.Interfaces;
using HeyBanking.Domain.Entities;
using MediatR;

namespace HeyBanking.App.Commands.CreateAccount
{
    public class CreateAccountCommand : IRequest<Guid>
    {
        public decimal InitialDeposit { get; set; }
    }

    public class CreateTodoItemCommandHandler : IRequestHandler<CreateAccountCommand, Guid>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUserService _currentUser;

        public CreateTodoItemCommandHandler(IApplicationDbContext context, ICurrentUserService currentUser)
        {
            _context = context;
            _currentUser = currentUser;
        }

        public async Task<Guid> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
        {
            var account = new Account
            {
                Id = Guid.NewGuid(),
                Ammount = request.InitialDeposit,
                OwnerId = _currentUser.UserId,
                CreatedAt = DateTimeOffset.UtcNow
            };

            _context.Accounts.Add(account);

            await _context.SaveChangesAsync(cancellationToken);

            return account.Id;
        }
    }
}