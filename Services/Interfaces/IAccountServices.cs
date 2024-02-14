using HomeBankingNetMvc.Models;
using Microsoft.AspNetCore.Mvc;

namespace HomeBankingNetMvc.Services.Interfaces
{
    public interface IAccountServices
    {
        public void Create(string email);
        public IEnumerable<Account> GetAccountsById(string email);
    }
}
