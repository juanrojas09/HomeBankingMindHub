using HomeBankingMindHub.Models;
using HomeBankingNetMvc.Models;
using HomeBankingNetMvc.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HomeBankingNetMvc.Repositories.Implementation
{
    public class LoanRepository :RepositoryBase<Loan>, ILoanRepository
    {
        public LoanRepository(HomeBankingContext _homeBankingContext) :base(_homeBankingContext)
        {
            
        }
        public async Task<Loan> FindById(long id)
        {
            try
            {
                var loan = await FindByCondition(x=>x.Id==id).FirstOrDefaultAsync();
                return loan;

            }catch(Exception ex)
            {
                throw new Exception("Error al buscar por id" + ex.Message);
            }
        }

        public  IEnumerable<Loan> GetAll()
        {
            try
            {
                var loans =  FindAll();
                return loans;

            }catch(Exception ex)
            {
                throw new Exception("Error al listar los loans", ex);
            }
        }
    }
}
