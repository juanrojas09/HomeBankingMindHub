using AutoMapper;
using HomeBankingMindHub.Models;
using HomeBankingNetMvc.Models;
using HomeBankingNetMvc.Models.DTOs;

namespace HomeBankingNetMvc.Utils
{
    public class AutoMapperProfiles:Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Client, ClientDTO>().ReverseMap();
            CreateMap<ClientDTO, Client>();
            CreateMap<Account, AccountDTO>().ReverseMap(); ;
            CreateMap<AccountDTO, Account>();
            CreateMap<ClientLoan, ClientLoanDTO>().ReverseMap(); ;
            CreateMap<Card, CardDTO>().ReverseMap();
            CreateMap<ClientDTOReq, Client>();
            
        }
    }
}
