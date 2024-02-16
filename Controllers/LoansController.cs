using HomeBankingNetMvc.Models;
using HomeBankingNetMvc.Models.DTOs;
using HomeBankingNetMvc.Services.Interfaces;
using HomeBankingNetMvc.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HomeBankingNetMvc.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoansController : ControllerBase
    {

        private readonly IClientServices _clientServices;
        private readonly IAccountServices _accountServices;
        private readonly ILoanServices _loanServices;
        private readonly IClientLoanServices _clientLoanServices;
        private readonly ITransactionServices _transactionServices;
        private readonly ValidateLoanData validateLoanData;
        public LoansController(IClientServices clientServices, IAccountServices accountServices, IClientLoanServices clientLoanServices, ILoanServices loanServices, ITransactionServices transactionServices, ValidateLoanData validateLoanData)
        {
            _clientServices = clientServices;
            _accountServices = accountServices;
            _clientLoanServices = clientLoanServices;
            _loanServices = loanServices;
            _transactionServices = transactionServices;
            this.validateLoanData = validateLoanData;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var loans=_loanServices.GetAll();
                return StatusCode(200, loans);

            }catch
            {
                throw new Exception("Error al traer los loans");
            }
        }


        [HttpPost]
        public IActionResult Create(LoanApllicationDTO loanApllicationDTO)
        {
            string email = User.FindFirst("Client") != null ? User.FindFirst("Client").Value : string.Empty;
            try
            {
                if (loanApllicationDTO == null   )
                {
                    return StatusCode(403, "Datos de solicitud nulos");
                }
                //pasar logica al servicio

                switch (this.validateLoanData.ValidateLoan(loanApllicationDTO,email))
                {
                    case LoanValidationResult.Valid:                     
                        return Ok("Solicitud de préstamo creada exitosamente");
                    case LoanValidationResult.InvalidData:
                        return Forbid("Los datos de la solicitud de préstamo son inválidos");
                    case LoanValidationResult.InvalidLoan:
                        return Forbid("El préstamo especificado no existe");
                    case LoanValidationResult.InvalidAmount:
                        return Forbid("El monto solicitado excede el monto máximo del préstamo");
                    case LoanValidationResult.InvalidInstallments:
                        return Forbid("La cantidad de cuotas solicitadas no es válida");
                    case LoanValidationResult.InvalidDestinationAccount:
                        return Forbid("La cuenta de destino especificada no es válida");
                    case LoanValidationResult.InvalidOwnership:
                        return Forbid("La cuenta de destino no pertenece al cliente autenticado");
                    default:
                        return StatusCode(500, "Error interno del servidor");
                }

               

            }
            catch(Exception ex)
            {
                throw new Exception("Error al crear prestamo");
            }
        }
    }
}
