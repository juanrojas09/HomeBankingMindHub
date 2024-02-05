using HomeBankingMindHub.Models;
using HomeBankingNetMvc.Models;
using HomeBankingNetMvc.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HomeBankingNetMvc.Repositories.Implementation
{
    public class AccountRepository : RepositoryBase<Account>, IAccountRepository
    {
        public AccountRepository(HomeBankingContext homeBankingContext) : base(homeBankingContext)
        {
                
        }

        public Account FindById(long id)
        {
            try
            {
                return FindByCondition(x => x.Id == id).Include(account => account.Transactions).FirstOrDefault();

            }
            catch (Exception ex)
            {
                throw new Exception("Error al encontrar la cuenta con sus transacciones " + id + ex.Message);
            }
        }

        public IEnumerable<Account> GetAllAccounts()
        {
            try
            {
                return FindAll().Include(account => account.Transactions).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar todos las cuentas" + ex.Message);
            }
        }
    }
}
