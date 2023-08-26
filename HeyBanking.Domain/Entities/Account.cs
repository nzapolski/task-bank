namespace HeyBanking.Domain.Entities
{
    public class Account
    {
        public Guid Id { get; set; }
        
        public Guid OwnerId { get; set; }

        public decimal Amount { get; set; }

        public DateTimeOffset CreatedAt { get; set; }


        public User Owner { get; set; }
    }
}
