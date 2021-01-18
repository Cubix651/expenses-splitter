using System.Linq;
using AutoMapper;

namespace ExpensesSplitter.WebApi.Infrastructure
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Database.Models.Expense, Models.Expense>()
                .ForMember(e => e.Participants, opt => opt
                    .MapFrom(e => e.Participations.Select(p => p.SettlementUser)));
            CreateMap<Models.NewExpense, Database.Models.Expense>()
                .ForMember(e => e.Participations, opt => opt
                    .Ignore());

            CreateMap<Database.Models.SettlementUser, Models.SettlementUser>()
                .ForMember(dest => dest.DisplayName, opt => opt.MapFrom(
                    src => src.DisplayName ?? src.User.Name));
            CreateMap<Models.NewSettlementUser, Database.Models.SettlementUser>();
            
            CreateMap<Database.Models.Transaction, Models.Transaction>();
            CreateMap<Models.NewTransaction, Database.Models.Transaction>();
        }   
    }
}