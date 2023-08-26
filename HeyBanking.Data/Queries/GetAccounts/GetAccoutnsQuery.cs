using AutoMapper;
using AutoMapper.QueryableExtensions;
using HeyBanking.App.Common.Interfaces;
using HeyBanking.App.Common.Models;
using HeyBanking.App.Extensions;
using HeyBanking.App.Queries.GetAccount;
using MediatR;

namespace HeyBanking.App.Queries.GetAccounts
{
    public class GetAccoutnsQuery : IRequest<PagedResult<AccountDto>>
    {
        public int PageNumber { get; init; } = 0;

        public int PageSize { get; init; } = 10;
    }

    public class GetAccoutnsQueryHandler : IRequestHandler<GetAccoutnsQuery, PagedResult<AccountDto>>
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

        public async Task<PagedResult<AccountDto>> Handle(GetAccoutnsQuery request, CancellationToken cancellationToken)
        {
            return await _dbContext.Accounts
                .Where(x => x.OwnerId == _currentUser.UserId)
                .OrderBy(x => x.CreatedAt)
                .ProjectTo<AccountDto>(_mapper.ConfigurationProvider)
                .PagedResultAsync(request.PageNumber, request.PageSize);
        }
    }
}
