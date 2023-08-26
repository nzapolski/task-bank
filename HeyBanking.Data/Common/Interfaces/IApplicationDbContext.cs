using HeyBanking.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HeyBanking.App.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Account> Accounts { get; }

        DbSet<User> Users { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
