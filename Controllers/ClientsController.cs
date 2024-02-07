using AutoMapper;
using HomeBankingMindHub.Models;
using HomeBankingNetMvc.Models.DTOs;
using HomeBankingNetMvc.Repositories.Interfaces;
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
    }
}
