using HomeBankingNetMvc.Models;
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

        public AccountServices(IClientRepository clientRepository, IAccountRepository accountRepository)
        {
            _clientRepository = clientRepository;
            _accountRepository = accountRepository;
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
