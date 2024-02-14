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
using System.Text.RegularExpressions;

namespace HomeBankingNetMvc.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {


        private readonly IClientServices _clientServices;
        private readonly ICardServices _cardServices;
        private readonly IAccountServices _accountServices;
        private IMapper _mapper;

        public ClientsController(IClientServices clientServices, IMapper mapper, ICardServices cardServices, IAccountServices accountServices)
        {
            _clientServices = clientServices;
            _mapper = mapper;
            _cardServices = cardServices;
            _accountServices = accountServices;
        }

        [HttpGet]
        public IActionResult Get()

        {
            try
            {
                var clients = _clientServices.Get();
                return Ok(clients);

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
                var client = _clientServices.Get(id);
            
                return Ok(client);

               
            }

            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }    

        }


        [HttpPost("CreateClient")]

        public IActionResult Create([FromBody] ClientDTOReq clientDTO)
        {
            try
            {
                var client = _mapper.Map<Client>(clientDTO);

                if (!ClientsValidation.IsValidEmail(clientDTO.Email))
                {
                    return StatusCode(403, "Email inválido");
                }


                if (!ClientsValidation.AreClientDataValid(client))
                {
                    return StatusCode(403, "Datos inválidos");
                }
                _clientServices.Create(clientDTO);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [HttpGet("current")]
        public IActionResult GetCurrent()
        {
            try
            {
                string email = User.FindFirst("Client") != null ? User.FindFirst("Client").Value : string.Empty;
                if (email == string.Empty)
                {
                    return Forbid();
                }
                
                var client = _clientServices.GetCurrent(email);

          
                return Ok(client);

               
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] Client client)
        {
            try
            {
                //validamos datos antes
                if (!ClientsValidation.IsValidEmail(client.Email))
                {
                    return StatusCode(403, "Email inválido");
                }
                
                if (!ClientsValidation.AreClientDataValid(client))
                {
                    return StatusCode(403, "Datos inválidos");
                }

              
                 _clientServices.Post(client);
                return Created("", client);

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        //tarjetas

        [HttpPost("current/cards")]
        public IActionResult Create(CreateCardDTO cardData)
        {
            try
            {


                string email = User.FindFirst("Client") != null ? User.FindFirst("Client").Value : string.Empty;
                 _cardServices.Create(cardData, email);

                return StatusCode(201, "Created");


            }
            catch
            {
                throw new Exception("Error al crear tarjeta");
            }

        }


        [HttpGet("current/cards")]
        public IActionResult getCardsByClientId()
        {
            try
            {
                string email = User.FindFirst("Client") != null ? User.FindFirst("Client").Value : string.Empty;
                var cards = _cardServices.getCardsByClientId(email);
                return Ok(cards);

            }
            catch
            {
                throw new Exception("Error al traer las tarjetas asociadas al cliente");
            }
        }


        //cuentas

        [HttpPost("current")]

        public IActionResult Create()
        {
            try
            {

                string email = User.FindFirst("Client") != null ? User.FindFirst("Client").Value : string.Empty;
                _accountServices.Create(email);


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
                var acc = _accountServices.GetAccountsById(email);



                return Ok(acc);
            }
            catch
            {
                throw new Exception("Error al traer cuentas por id");
            }
        }







    }
}
