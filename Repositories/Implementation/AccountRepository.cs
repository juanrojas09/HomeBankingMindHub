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

        public void Save(Account account)
        {
            try
            {
                if (account.Id == 0)
                {
                    Create(account);
                }
                else
                {
                    Update(account);
                }

                SaveChanges();

            }
            catch(Exception ex)
            {
                throw new Exception("Error al guardar un account" + ex.Message);
            }
        }

        public IEnumerable<Account> GetAccountsByClient(long clientId)
        {
            try
            {

                var Accounts = FindByCondition(x => x.ClientId == clientId)
                 .Include(account => account.Transactions)
                 .ToList();                

                return Accounts;

            }catch(Exception ex)
            {
                throw new Exception("Error al traer cuentas por cliente" + ex.Message);
            }
        }

        public Account GetAccountByNumber(string number)
        {
            try
            {
                var account = FindByCondition(x => x.Number.ToUpper() == number.ToUpper()).Include(account => account.Transactions).FirstOrDefault();
                return account;
                
            }
            catch(Exception ex)
            {
                throw new Exception("Error al traer cuenta por numero" + ex.Message);
            }
        }
    }
}
