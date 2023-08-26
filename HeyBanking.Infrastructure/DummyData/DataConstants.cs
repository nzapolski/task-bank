namespace HeyBanking.Infrastructure.DummyData
{
    public static class DataConstants
    {
        public static class DummyUser
        {
            public static Guid Id => Guid.Parse("11111111-1111-1111-1111-111111111111");
            public static string Name => "Dummy user";
            public static string ExternalId => "-";
        }

        public static class Database
        {
            public static string FileName = "HeyBanking.db";
        }
    }
}
