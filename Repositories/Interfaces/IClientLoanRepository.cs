using HomeBankingNetMvc.Models;

namespace HomeBankingNetMvc.Repositories.Interfaces
{
    public interface IClientLoanRepository
    {
        public void Save(ClientLoan clientLoan);
    }
}
