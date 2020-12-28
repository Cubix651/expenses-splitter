using AutoMapper;

namespace ExpensesSplitter.WebApi.Infrastructure
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Database.Models.Expense, Models.Expense>()
                .ReverseMap();
            CreateMap<Models.NewExpense, Database.Models.Expense>();

            CreateMap<Database.Models.SettlementUser, Models.SettlementUser>()
                .ReverseMap();
            CreateMap<Models.NewSettlementUser, Database.Models.SettlementUser>();
        }   
    }
}