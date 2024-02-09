using HomeBankingMindHub.Models;
using HomeBankingNetMvc.Models;

namespace HomeBankingNetMvc.Repositories.Interfaces
{
    public interface IAccountRepository
    {
        IEnumerable<Account> GetAllAccounts();

        Account FindById(long id);

        void Save(Account account);
        IEnumerable<Account> GetAccountsByClient(long clientId);
        
        


    }
}
