using HomeBankingMindHub.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HomeBankingNetMvc.Services.Interfaces
{
    public interface IAuthServices
    {
        public Task<ClaimsPrincipal> Login([FromBody] Client client);

       
    }
}
