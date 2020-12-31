namespace ExpensesSplitter.WebApi.Models
{
    public class SolutionTransaction
    {
        public SettlementUser From { get; set; }
        public SettlementUser To { get; set; }
        public decimal Amount { get; set; }
    }
}