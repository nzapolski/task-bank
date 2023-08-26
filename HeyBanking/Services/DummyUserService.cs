using System.Security.Claims;
using HeyBanking.App.Common.Interfaces;

namespace HeyBanking.API.Models
{
    public class DummyUserService : ICurrentUserService
    {
        public Guid UserId => Guid.Empty;
    }
}
