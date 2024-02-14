﻿using HomeBankingMindHub.Models;
using HomeBankingNetMvc.Models.DTOs;
using HomeBankingNetMvc.Models;
using HomeBankingNetMvc.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using HomeBankingNetMvc.Repositories.Interfaces;

namespace HomeBankingNetMvc.Services.Implementation
{
    public class TransactionServices : ITransactionServices
    {
        private IClientRepository _clientRepository;
        private IAccountRepository _accountRepository;
        private ITransactionRepository _transactionRepository;
        public TransactionServices(IClientRepository clientRepository, IAccountRepository accountRepository, ITransactionRepository transactionRepository)
        {
            _clientRepository = clientRepository;
            _accountRepository = accountRepository;
            _transactionRepository = transactionRepository;
        }

       
        public void Post([FromBody] TransferDTO transferDTO,string email)
        {
            Client client = _clientRepository.FindByEmail(email);
            if (client == null)
            {
                throw new Exception("No existe el cliente");
            }

            if (transferDTO.FromAccountNumber == string.Empty || transferDTO.ToAccountNumber == string.Empty)
            {
                throw new Exception("Cuenta de origen o cuenta de destino no proporcionada.");
            }

            if (transferDTO.FromAccountNumber == transferDTO.ToAccountNumber)
            {
                throw new Exception("No se permite la transferencia a la misma cuenta.");
            }

            if (transferDTO.Amount == 0 || transferDTO.Description == string.Empty)
            {
                throw new Exception("Monto o descripción no proporcionados.");
            }

            //buscamos las cuentas
            Account fromAccount = _accountRepository.GetAccountByNumber(transferDTO.FromAccountNumber);
            if (fromAccount == null)
            {
                throw new Exception("Cuenta de origen no existe");
            }

            //controlamos el monto
            if (fromAccount.Balance < transferDTO.Amount)
            {
                throw new Exception("Fondos insuficientes");
            }

            //buscamos la cuenta de destino
            Account toAccount = _accountRepository.GetAccountByNumber(transferDTO.ToAccountNumber);
            if (toAccount == null)
            {
                throw new Exception("Cuenta de destino no existe");
            }

            //demas logica para guardado
            //comenzamos con la inserrción de las 2 transacciones realizadas
            //desde toAccount se debe generar un debito por lo tanto lo multiplicamos por -1
            _transactionRepository.Save(new Transactions
            {
                Type = TransactionType.DEBIT,
                Amount = transferDTO.Amount * -1,
                Description = transferDTO.Description + " " + toAccount.Number,
                AccountId = fromAccount.Id,
                Date = DateTime.Now,
            });

            //ahora una credito para la cuenta fromAccount
            _transactionRepository.Save(new Transactions
            {
                Type = TransactionType.CREDIT,
                Amount = transferDTO.Amount,
                Description = transferDTO.Description + " " + fromAccount.Number,
                AccountId = toAccount.Id,
                Date = DateTime.Now,
            });

            //seteamos los valores de las cuentas, a la ccuenta de origen le restamos el monto
            fromAccount.Balance = fromAccount.Balance - transferDTO.Amount;
            //actualizamos la cuenta de origen
            _accountRepository.Save(fromAccount);

            //a la cuenta de destino le sumamos el monto
            toAccount.Balance = toAccount.Balance + transferDTO.Amount;
            //actualizamos la cuenta de destino
            _accountRepository.Save(toAccount);
        }
    }
}
