using HomeBankingMindHub.Models;
using HomeBankingNetMvc.Models;
using HomeBankingNetMvc.Repositories.Interfaces;

namespace HomeBankingNetMvc.Repositories.Implementation
{
    public class ClientLoanRepository :RepositoryBase<ClientLoan>, IClientLoanRepository
    {
        public ClientLoanRepository(HomeBankingContext _homeBankingContext):base(_homeBankingContext)
        {
            
        }
        public void Save(ClientLoan clientLoan)
        {
            try
            {
                Create(clientLoan);
                SaveChanges();                

            }catch(Exception ex)
            {
                throw new Exception("Error al guardar el clientLoan" + ex.Message);
            }
        }
    }
}
