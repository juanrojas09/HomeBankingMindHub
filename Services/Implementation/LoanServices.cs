using HomeBankingNetMvc.Models;
using HomeBankingNetMvc.Repositories.Interfaces;
using HomeBankingNetMvc.Services.Interfaces;

namespace HomeBankingNetMvc.Services.Implementation
{
    public class LoanServices : ILoanServices
    {
        private readonly ILoanRepository _loanRepository;
        public LoanServices(ILoanRepository loanRepository)
        {
            _loanRepository = loanRepository;
        }
        public Task<Loan> FindById(long id)
        {
            var loan= _loanRepository.FindById(id);

            return loan;
        }

        public IEnumerable<Loan> GetAll()
        {
            var loans = _loanRepository.GetAll();
            return loans;
        }
    }
}
