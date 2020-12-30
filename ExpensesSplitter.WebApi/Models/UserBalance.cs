namespace ExpensesSplitter.WebApi.Models
{
    public class UserBalance
    {
        public SettlementUser User { get; set; }
        public decimal Balance { get; set; }

        public static UserBalance operator -(UserBalance userBalance)
        {
            return new UserBalance
            {
                User = userBalance.User,
                Balance = -userBalance.Balance
            };
        }
    }
}