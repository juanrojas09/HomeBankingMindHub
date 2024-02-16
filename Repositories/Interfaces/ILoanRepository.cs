using HomeBankingNetMvc.Models;
using HomeBankingNetMvc.Models.DTOs;

namespace HomeBankingNetMvc.Repositories.Interfaces
{
    public interface ILoanRepository
    {
        public IEnumerable<Loan> GetAll();
        public Task<Loan> FindById(long id);

    }
}
