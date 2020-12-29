using AutoMapper;

namespace ExpensesSplitter.WebApi.Infrastructure
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Database.Models.Expense, Models.Expense>();
            CreateMap<Models.NewExpense, Database.Models.Expense>();

            CreateMap<Database.Models.SettlementUser, Models.SettlementUser>()
                .ForMember(dest => dest.DisplayName, opt => opt.MapFrom(
                    src => src.DisplayName ?? src.User.Name));
            CreateMap<Models.NewSettlementUser, Database.Models.SettlementUser>();
        }   
    }
}