namespace ExpensesSplitter.WebApi.Models
{
    public class NewExpense
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Amount {get; set; }
    }
}