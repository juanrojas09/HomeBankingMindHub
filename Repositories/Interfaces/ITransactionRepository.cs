using HomeBankingNetMvc.Models;

namespace HomeBankingNetMvc.Repositories.Interfaces
{
    public interface ITransactionRepository
    {
        void Save(Transactions transaction);
        Transactions FindByNumber(long id);
    }
}
