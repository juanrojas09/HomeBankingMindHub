using AutoMapper;
using HomeBankingMindHub.Models;
using HomeBankingNetMvc.Models.DTOs;
using HomeBankingNetMvc.Repositories.Interfaces;
using HomeBankingNetMvc.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HomeBankingNetMvc.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private IClientRepository _clientRepository;
        private IMapper _mapper;

        public ClientsController(IClientRepository clientRepository, IMapper mapper)
        {
            _clientRepository = clientRepository;
            _mapper = mapper;
        }



        [HttpGet]
        public IActionResult Get()

        {
            try
            {
                var clients = _clientRepository.GetAllClients();
                var mappedClients = _mapper.Map<IEnumerable<ClientDTO>>(clients);
                
                var clientsDTO = new List<ClientDTO>();

                return Ok(mappedClients);              

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
                var client = _clientRepository.FindById(id);
                var mappedCLients = _mapper.Map<ClientDTO>(client);
                if (client == null)
                {
                    return Forbid();
                }
                return Ok(mappedCLients);

               
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
                //var client = new Client()
                //{
                //    Email = clientDTO.Email,
                //    FirstName = clientDTO.FirstName,
                //    LastName = clientDTO.LastName,

                //};
                var client = _mapper.Map<Client>(clientDTO);
                _clientRepository.Save(client);
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

                Client client = _clientRepository.FindByEmail(email);

                if (client == null)
                {
                    return Forbid();
                }

                var MappedClient = _mapper.Map<ClientDTO>(client);
                return Ok(MappedClient);

               
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
                //se puede validar en el modelado.. desp refactorizar
                if (String.IsNullOrEmpty(client.Email) || String.IsNullOrEmpty(client.Password) || String.IsNullOrEmpty(client.FirstName) || String.IsNullOrEmpty(client.LastName))
                    return StatusCode(403, "datos inválidos");

                //buscamos si ya existe el usuario
                Client user = _clientRepository.FindByEmail(client.Email);

                if (user != null)
                {
                    return StatusCode(403, "Email está en uso");
                }

               
               var hashedPass= Encrypt.HashPassword(client.Password);             
                                        
                

             
                Client newClient = new Client
                {
                    Email = client.Email,
                    Password = hashedPass,
                    FirstName = client.FirstName,
                    LastName = client.LastName,
                };

                _clientRepository.Save(newClient);
                return Created("", newClient);

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }




    }
}
