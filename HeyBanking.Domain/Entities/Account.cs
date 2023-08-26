namespace HeyBanking.Domain.Entities
{
    public class Account
    {
        public Guid Id { get; set; }
        
        public Guid OwnerId { get; set; }

        public decimal Ammount { get; set; }

        public DateTimeOffset CreatedAt { get; set; }
    }
}
