using HomeBankingMindHub.Models;
using HomeBankingNetMvc.Models.DTOs;
using HomeBankingNetMvc.Models;
using HomeBankingNetMvc.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HomeBankingNetMvc.Services.Interfaces;

namespace HomeBankingNetMvc.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private IClientRepository _clientRepository;
        private IAccountRepository _accountRepository;
        private ITransactionServices _transactionServices;

        public TransactionsController(IClientRepository clientRepository, IAccountRepository accountRepository, ITransactionServices transactionServices)
        {
            _clientRepository = clientRepository;
            _accountRepository = accountRepository;
            _transactionServices = transactionServices;
        }

        [HttpPost]
        public IActionResult Post([FromBody] TransferDTO transferDTO)
        {
            try
            {
                string email = User.FindFirst("Client") != null ? User.FindFirst("Client").Value : string.Empty;
                if (email == string.Empty)
                {
                    return Forbid("Email vacío");
                }
                _transactionServices.Post(transferDTO, email);




                return Ok("Creado con éxito");

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);

            }

        }

    }
}
