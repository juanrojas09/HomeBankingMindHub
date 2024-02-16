using HomeBankingNetMvc.Models;

namespace HomeBankingNetMvc.Services.Interfaces
{
    public interface ILoanServices
    {
        public IEnumerable<Loan> GetAll();
        public Task<Loan> FindById(long id);
    }
}
