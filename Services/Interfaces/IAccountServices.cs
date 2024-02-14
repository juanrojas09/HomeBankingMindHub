using HomeBankingNetMvc.Models;
using HomeBankingNetMvc.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace HomeBankingNetMvc.Services.Interfaces
{
    public interface IAccountServices
    {
        public IEnumerable<AccountDTO> Get();
        public AccountDTO Get(long id);
        public void Create(string email);
        public IEnumerable<Account> GetAccountsById(string email);
        public AccountDTO GetAccountByNumber(string number);
    }
}
