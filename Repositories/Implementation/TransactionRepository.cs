using HomeBankingMindHub.Models;
using HomeBankingNetMvc.Models;
using HomeBankingNetMvc.Repositories.Interfaces;
using System.Transactions;

namespace HomeBankingNetMvc.Repositories.Implementation
{
    public class TransactionRepository : RepositoryBase<Transactions>, ITransactionRepository
    {

        public TransactionRepository(HomeBankingContext repositoryContext) : base(repositoryContext)
        {
        }


        public Transactions FindByNumber(long id)
        {
            return FindByCondition(transaction => transaction.Id == id).FirstOrDefault();
        }

        public void Save(Transactions transaction)
        {
            Create(transaction);
            SaveChanges();
        }

        
    }
}
