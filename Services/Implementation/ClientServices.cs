using AutoMapper;
using HomeBankingMindHub.Models;
using HomeBankingNetMvc.Models;
using HomeBankingNetMvc.Models.DTOs;
using HomeBankingNetMvc.Repositories.Interfaces;
using HomeBankingNetMvc.Services.Interfaces;
using HomeBankingNetMvc.Utils;
using Microsoft.AspNetCore.Mvc;

namespace HomeBankingNetMvc.Services.Implementation
{
   
    public class ClientServices : IClientServices
    {

        private IClientRepository _clientRepository;
        private ICardRepository _cardRepository;
        public IAccountRepository _accountRepository;
        private IMapper _mapper;

        public ClientServices(IMapper mapper, IAccountRepository accountRepository, ICardRepository cardRepository, IClientRepository clientRepository)
        {
            _mapper = mapper;
            _accountRepository = accountRepository;
            _cardRepository = cardRepository;
            _clientRepository = clientRepository;
        }

        public void Create([FromBody] ClientDTOReq clientDTO)
        {
            try
            {

                var client = _mapper.Map<Client>(clientDTO);
                _clientRepository.Save(client);

            }
            catch (Exception ex)
            {
                throw new Exception("Error al crear cliente" + ex.Message);

            }
        }

      

        
        public IActionResult Create()
        {
            throw new NotImplementedException();
        }
        

        public List<ClientDTO> Get()
        {
            try
            {
                var clients = _clientRepository.GetAllClients();
                var mappedClients = _mapper.Map<IEnumerable<ClientDTO>>(clients);

                var clientsDTO = new List<ClientDTO>();

                return clientsDTO;

            }

            catch (Exception ex)
            {
                throw new Exception("Error al traer todos los clientes en capa de servicios" + ex.Message);
            }
        }

        public ClientDTO Get(long id)
        {
            try
            {
                var client = _clientRepository.FindById(id);
                var mappedCLients = _mapper.Map<ClientDTO>(client);
                if (client == null)
                {
                    throw new Exception("No existe ningun cliente con ese id");
                }
                return mappedCLients;


            }

            catch (Exception ex)
            {

                throw new Exception("Error al traer el cliente con id"+id);
            }
        }

      

        public ClientDTO GetCurrent(string email)
        {
            try
            {
                
                Client client = _clientRepository.FindByEmail(email);

                if (client == null)
                {
                    throw new Exception("Error, no existe un cliente con ese email");
                }

                var MappedClient = _mapper.Map<ClientDTO>(client);
                return MappedClient;


            }
            catch (Exception ex)
            {
                throw new Exception("Error al traer el cliente");
            }
        }

        public void Post([FromBody] Client client)
        {
            try
            {
                //buscamos si ya existe el usuario
                Client user = _clientRepository.FindByEmail(client.Email);

                if (user != null)
                {
                    throw new Exception("Error al encontrar cliente por email");
                }


                var hashedPass = Encrypt.HashPassword(client.Password);


                Client newClient = new Client
                {
                    Email = client.Email,
                    Password = hashedPass,
                    FirstName = client.FirstName,
                    LastName = client.LastName,
                };

                _clientRepository.Save(newClient);
            }
            catch(Exception ex)
            {
                throw new Exception("Error al crear un cliente");
            }
        }
    }
}
