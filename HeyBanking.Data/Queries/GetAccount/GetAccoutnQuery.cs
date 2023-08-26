using AutoMapper;
using HeyBanking.App.Common.Exceptions;
using HeyBanking.App.Common.Interfaces;
using HeyBanking.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HeyBanking.App.Queries.GetAccount
{
    public class GetAccoutnQuery : IRequest<AccountDto>
    {
        public Guid AccountId { get; init; }
    }

    public class GetAccoutnsQueryHandler : IRequestHandler<GetAccoutnQuery, AccountDto>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUser;

        public GetAccoutnsQueryHandler(IApplicationDbContext context, IMapper mapper, ICurrentUserService currentUser)
        {
            _dbContext = context;
            _mapper = mapper;
            _currentUser = currentUser;
        }

        public async Task<AccountDto> Handle(GetAccoutnQuery request, CancellationToken cancellationToken)
        {
            var account = await _dbContext.Accounts
                .SingleOrDefaultAsync(x => x.Id == request.AccountId, cancellationToken);

            if (account == null)
            {
                throw new NotFoundException(nameof(Account), request.AccountId);
            }

            var dto = _mapper.Map<AccountDto>(account);

            return dto;
        }
    }
}
