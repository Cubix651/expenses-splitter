namespace ExpensesSplitter.WebApi.Database.Models
{
    public class Settlement
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string IdOwner { get; set; }
        public User owner { get; set; }
    }
}