using AutoMapper;
using HeyBanking.App.Queries.GetAccount;
using HeyBanking.Domain.Entities;

namespace HeyBanking.App.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Account, AccountDto>();
        }
    }
}
