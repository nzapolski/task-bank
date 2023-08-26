namespace HeyBanking.App.Commands.Withdraw
{
    public class WithdrawDto
    {
        public Guid AccountId { get; set; }
        public decimal WithdrawAmount { get; set; }
        public decimal Balance { get; set;}
    }
}
