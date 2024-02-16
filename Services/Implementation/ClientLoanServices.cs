using HomeBankingNetMvc.Models;
using HomeBankingNetMvc.Repositories.Interfaces;
using HomeBankingNetMvc.Services.Interfaces;

namespace HomeBankingNetMvc.Services.Implementation
{
    public class ClientLoanServices : IClientLoanServices
    {
        private readonly IClientLoanRepository clientLoanRepository;
public ClientLoanServices(IClientLoanRepository clientLoanRepository)
        {
            this.clientLoanRepository = clientLoanRepository;
        }

        public void Save(ClientLoan clientLoan)
        {
            this.clientLoanRepository.Save(clientLoan);
        }
    }
}
