using HeyBanking.Domain.Entities;
using HeyBanking.Infrastructure.DummyData;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace HeyBanking.Infrastructure.Persistence
{
    public class ApplicationDbContextInitialiser
    {
        private readonly ILogger<ApplicationDbContextInitialiser> _logger;
        private readonly ApplicationDbContext _context;

        public ApplicationDbContextInitialiser(ILogger<ApplicationDbContextInitialiser> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task InitialiseAsync()
        {
             await _context.Database.MigrateAsync();
        }

        public async Task SeedAsync()
        {
            try
            {
                await TrySeedAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while seeding the database.");
                throw;
            }
        }

        public async Task TrySeedAsync()
        {
            if (!_context.Users.Any())
            {
                _context.Users.Add(new User
                {
                    Id = DataConstants.DummyUser.Id,
                    Name = DataConstants.DummyUser.Name,
                    ExternalId = DataConstants.DummyUser.ExternalId
                });

                await _context.SaveChangesAsync();
            }
        }
    }
}
