using AutoMapper;
using HomeBankingMindHub.Models;
using HomeBankingNetMvc.Models;
using HomeBankingNetMvc.Models.DTOs;
using HomeBankingNetMvc.Repositories.Implementation;
using HomeBankingNetMvc.Repositories.Interfaces;
using HomeBankingNetMvc.Services.Interfaces;
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
        private readonly IAccountServices _accountServices;
        private IMapper _mapper;
        public AccountController(IAccountRepository accountRepository, IMapper mapper, IClientRepository clientRepository, IAccountServices accountServices)
        {
            _accountRepository = accountRepository;
            _mapper = mapper;
            _clientRepository = clientRepository;
            _accountServices = accountServices;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                
                return Ok(_accountServices.Get());                

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
          
                return Ok(_accountServices.Get(id));

              
            }

            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }

        [HttpGet("number/{number}")]
        public IActionResult GetAccountByNumber(string number)
        {
            try
            {
                var account = _accountServices.GetAccountByNumber(number);
                return StatusCode(201, account);         
            
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }







    }
}
