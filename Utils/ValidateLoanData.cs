using AutoMapper;
using HomeBankingNetMvc.Models;
using HomeBankingNetMvc.Models.DTOs;
using HomeBankingNetMvc.Services.Interfaces;
using Microsoft.Identity.Client;

namespace HomeBankingNetMvc.Utils
{
    public class ValidateLoanData
    {
        private  IClientServices _clientServices;
        private  IAccountServices _accountServices;
        private  ILoanServices _loanServices;
        private  IClientLoanServices _clientLoanServices;
        private  ITransactionServices _transactionServices;
        private  IMapper _mapper;


        public ValidateLoanData(IClientServices clientServices, IAccountServices accountServices, ILoanServices loanServices, IClientLoanServices clientLoanServices, ITransactionServices transactionServices, IMapper mapper)
        {
            _clientServices = clientServices;
            _accountServices = accountServices;
            _loanServices = loanServices;
            _clientLoanServices = clientLoanServices;
            _transactionServices = transactionServices;
            _mapper = mapper;
        }
        public  LoanValidationResult ValidateLoan(LoanApllicationDTO loanApllicationDTO,string email)
        {


            //valido que los datos no sean nulos o vacios
            if (string.IsNullOrEmpty(loanApllicationDTO.ToAccountNumber) ||
                loanApllicationDTO.Amount <= 0 ||
                loanApllicationDTO.Payments == null )
            {
                return LoanValidationResult.InvalidData;
            }

            
            //validacion de que el prestamo existe
            var loan = _loanServices.FindById(loanApllicationDTO.LoanID);
            if (loan == null)
            {
                return LoanValidationResult.InvalidLoan;
            }

            //validamos que el monto solicitado no exceda el monto maximo del prestamo
            if (loanApllicationDTO.Amount > loan.Result.MaxAmount)
            {
                return LoanValidationResult.InvalidAmount;
            }

            //validamos que la cantidad de cuotas se encuentre entre las disponibles en el prestamo
            //convierto en una lista donde divido por comas, luego verifico que el elemnto este en la lista, sino
            var availablePayments = loan.Result.Payments.Split(',').ToList();
            if (!availablePayments.Contains(loanApllicationDTO.Payments))
            {
                return LoanValidationResult.InvalidInstallments;
            }

            //validamos que la cuenta de destino exista
            var account = _accountServices.GetAccountByNumber(loanApllicationDTO.ToAccountNumber);
            if (account == null)
            {
                return LoanValidationResult.InvalidDestinationAccount;
            }

            //validar que la cuenta de destino pertenezca al cliente autenticado
            //del controller traigo el mail, me traigo el current user que esta autenticado
            //mapeo las cuentas del cliente y si no existe una cuenta que tenga el numero de destino, devuelve invalido
            var client = _clientServices.GetCurrent(email);
            if (client.Accounts.All(a => a.Number != loanApllicationDTO.ToAccountNumber))
            {
                return LoanValidationResult.InvalidDestinationAccount;
            }

            //caso exito
            //Se debe crear una solicitud de préstamo con el monto solicitado sumando el 20% del mismo
            var amountTax = loanApllicationDTO.Amount * 0.20;
            var totalAmount = amountTax + loanApllicationDTO.Amount;

            var clientLoan = new ClientLoan()
            {
                 ClientId= client.Id,
                Amount = totalAmount,
                Payments = loanApllicationDTO.Payments,
                LoanId = loanApllicationDTO.LoanID,


            };
            
            _clientLoanServices.Save(clientLoan);

            //creo la transaccion
            //Se debe crear una transacción “CREDIT” asociada a la cuenta de destino (el monto debe quedar positivo)
            //con la descripción concatenando el nombre del préstamo y la frase “loan approved”
            var dataTransf = new TransferDTO()
            {
                Amount = totalAmount,
                Description = $"{loan.Result.Name} loan approved",
                ToAccountNumber = loanApllicationDTO.ToAccountNumber,
                FromAccountNumber = loanApllicationDTO.ToAccountNumber,

            };

            var transaction = new Transactions()
            {
                Type = TransactionType.CREDIT,
                Amount = loanApllicationDTO.Amount,
                Description = $"{loan.Result.Name} loan approved",
                AccountId = account.Id,
                Date = DateTime.Now
            };
            account.Balance += loanApllicationDTO.Amount;
            var mappedAccount = _mapper.Map<Account>(account);
            _accountServices.Save(mappedAccount);


             _transactionServices.SaveLoanMovement(transaction);

             //Se debe actualizar la cuenta de destino sumando el monto solicitado.


            return LoanValidationResult.Valid;
        }
    }
}
