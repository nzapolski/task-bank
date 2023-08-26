using System.Security.Claims;
using HeyBanking.App.Common.Interfaces;
using HeyBanking.Infrastructure.DummyData;

namespace HeyBanking.API.Models
{
    public class DummyUserService : ICurrentUserService
    {
        public Guid UserId => DataConstants.DummyUser.Id;
    }
}
