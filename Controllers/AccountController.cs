using AutoMapper;
using HomeBankingMindHub.Models;
using HomeBankingNetMvc.Models;
using HomeBankingNetMvc.Models.DTOs;
using HomeBankingNetMvc.Repositories.Implementation;
using HomeBankingNetMvc.Repositories.Interfaces;
using HomeBankingNetMvc.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Policy;
using System.Text.Json;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace HomeBankingNetMvc.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        public IAccountRepository _accountRepository;
        public IClientRepository _clientRepository;
        private IMapper _mapper;
        public AccountController(IAccountRepository accountRepository, IMapper mapper, IClientRepository clientRepository)
        {
            _accountRepository = accountRepository;
            _mapper = mapper;
            _clientRepository = clientRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var accounts = _accountRepository.GetAllAccounts();
                

                var mappedAccounts = _mapper.Map<IEnumerable<AccountDTO>>(accounts);
                return Ok(mappedAccounts);

                

            }

            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }


            
        }


        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            try
            {
                var account = _accountRepository.FindById(id);
                if (account == null)
                {
                    return Forbid();
                }

                var mappedAccount = _mapper.Map<AccountDTO>(account);
                return Ok(mappedAccount);

              
            }

            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }

        [HttpPost("current")]

        public IActionResult Create()
        {
            try
            {

                string email = User.FindFirst("Client") != null ? User.FindFirst("Client").Value : string.Empty;
                var _client = _clientRepository.FindByEmail(email);
                var accountsQty = _accountRepository.GetAccountsByClient(_client.Id).Count();
                if (accountsQty >= 3)
                {
                    return StatusCode(403,"No se pueden crear mas de 3 cuentas");
                }
                //var mappedAccount = _mapper.Map<Account>(account);

                var accNumber=AccountNumberGenerator.GenerarNumeroCuenta();

                var mappedAccount = new Account()
                {
                    Number = accNumber,
                    CreationDate = DateTime.Now,
                    Balance = 0,
                    ClientId = _client.Id
                };
                
                _accountRepository.Save(mappedAccount);


                return StatusCode(201, "Creada");

            }
            catch
            {
                throw new Exception("Error al crear cuenta");
            }
        }

        [HttpGet("currents/Account")]
        public IActionResult GetAccountsById()
        {
            try
            {
                string email = User.FindFirst("Client") != null ? User.FindFirst("Client").Value : string.Empty;
                var _client = _clientRepository.FindByEmail(email);
                
                return Ok(_accountRepository.GetAccountsByClient(_client.Id));
            }
            catch
            {
                throw new Exception("Error al traer cuentas por id");
            }
        }

     

     


        

    }
}
