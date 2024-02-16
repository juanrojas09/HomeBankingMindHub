using HomeBankingNetMvc.Models;
using HomeBankingNetMvc.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace HomeBankingNetMvc.Services.Interfaces
{
    public interface ITransactionServices
    {
        public void Post([FromBody] TransferDTO transferDTO, string email);

        public void SaveLoanMovement(Transactions transaction);
    }
}
