using HomeBankingMindHub.Models;
using HomeBankingNetMvc.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace HomeBankingNetMvc.Services.Interfaces
{
    public interface IClientServices
    {
        public List<ClientDTO> Get();
        public ClientDTO Get(long id);
        public void Create([FromBody] ClientDTOReq clientDTO);
        public ClientDTO GetCurrent(string email);
        public void Post([FromBody] Client client);        
        
       
    }
}
