using AutoMapper;
using HomeBankingMindHub.Models;
using HomeBankingNetMvc.Models;
using HomeBankingNetMvc.Models.DTOs;
using HomeBankingNetMvc.Repositories.Implementation;
using HomeBankingNetMvc.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace HomeBankingNetMvc.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        public IAccountRepository _accountRepository;
        private IMapper _mapper;
        public AccountController(IAccountRepository accountRepository, IMapper mapper)
        {
            _accountRepository = accountRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var accounts = _accountRepository.GetAllAccounts();
                //var accountDTOs = new List<AccountDTO>();

                var mappedAccounts = _mapper.Map<IEnumerable<AccountDTO>>(accounts);
                return Ok(mappedAccounts);

                //foreach (Account account in accounts)
                //{
                //    var newAccountDTO = new AccountDTO
                //    {
                //        Id = account.Id,
                //        Number = account.Number,
                //        CreationDate = account.CreationDate,
                //        Balance = account.Balance,
                //        Transactions = account.Transactions.Select(tr => new TransactionDTO
                //        {
                //            Id = tr.Id,
                //            Type = tr.Type,
                //            Date = tr.Date,
                //            Amount = tr.Amount,
                //            Description=tr.Description,
                            
                            
                            

                //        }).ToList()

                //    };
                //    accountDTOs.Add(newAccountDTO);
                //}
                //return Ok(accountDTOs);

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

                //var newAccountDTO = new AccountDTO
                //{
                //    Id = account.Id,
                //    Number = account.Number,
                //    CreationDate = account.CreationDate,
                //    Balance = account.Balance,
                //    Transactions = account.Transactions.Select(tr => new TransactionDTO
                //    {
                //        Id = tr.Id,
                //        Type = tr.Type,
                //        Date = tr.Date,
                //        Amount = tr.Amount,
                //        Description = tr.Description,




                //    }).ToList()
                //};
                //return Ok(newAccountDTO);
            }

            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }
    }
}
