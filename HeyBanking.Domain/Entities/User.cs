namespace HeyBanking.Domain.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string ExternalId { get; set; }
        public string Name { get; set; }
    }
}
