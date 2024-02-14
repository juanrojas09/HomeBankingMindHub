using AutoMapper;
using HomeBankingNetMvc.Models;
using HomeBankingNetMvc.Models.DTOs;
using HomeBankingNetMvc.Repositories.Interfaces;
using HomeBankingNetMvc.Services.Interfaces;
using HomeBankingNetMvc.Utils;
using Microsoft.AspNetCore.Mvc;

namespace HomeBankingNetMvc.Services.Implementation
{
    public class AccountServices : IAccountServices
    {
        private readonly IClientRepository _clientRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly IMapper _mapper;

        public AccountServices(IClientRepository clientRepository, IAccountRepository accountRepository, IMapper mapper)
        {
            _clientRepository = clientRepository;
            _accountRepository = accountRepository;
            _mapper = mapper;
        }

        public void Create(string email)
        {
            try
            {

                
                var _client = _clientRepository.FindByEmail(email);
                var accountsQty = _accountRepository.GetAccountsByClient(_client.Id).Count();
                if (accountsQty >= 3)
                {
                    throw new Exception("Limite ecedido");
                }
                //var mappedAccount = _mapper.Map<Account>(account);

                var accNumber = AccountNumberGenerator.GenerarNumeroCuenta();

                var mappedAccount = new Account()
                {
                    Number = accNumber,
                    CreationDate = DateTime.Now,
                    Balance = 0,
                    ClientId = _client.Id
                };

                _accountRepository.Save(mappedAccount);

            }
            catch(Exception ex)
            {
                throw new Exception("Error al crear cuenta" + ex.Message);
            }
        }

        public IEnumerable<AccountDTO> Get()
        {
            var accounts = _accountRepository.GetAllAccounts();
            var mappedAccounts = _mapper.Map<IEnumerable<AccountDTO>>(accounts);
            return mappedAccounts;
        }

        public AccountDTO Get(long id)
        {
            var account = _accountRepository.FindById(id);
            if (account == null)
            {
                throw new Exception("Error, no hay cuentas con ese id");
            }

            var mappedAccount = _mapper.Map<AccountDTO>(account);
            return mappedAccount;
        }

        public AccountDTO GetAccountByNumber(string number)
        {
            try
            {
                var account = _accountRepository.GetAccountByNumber(number);
                var mappedAccount = _mapper.Map<AccountDTO>(account);

                return mappedAccount;

            }catch(Exception ex)
            {
                throw new Exception("Error al traer cuenta" + ex.Message);
            }
        }
        
        public IEnumerable<Account> GetAccountsById(string email)
        {
            try
            {
                var _client = _clientRepository.FindByEmail(email);
                var accounts= _accountRepository.GetAccountsByClient(_client.Id);
             
                return accounts;

            }
            catch(Exception ex)
            {
                throw new Exception("Error al traer cuentas por id");
            }
        }

   
    }
}
