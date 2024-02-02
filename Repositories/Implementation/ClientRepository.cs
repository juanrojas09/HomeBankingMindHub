using HomeBankingMindHub.Models;
using HomeBankingNetMvc.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HomeBankingNetMvc.Repositories.Implementation
{
    public class ClientRepository : RepositoryBase<Client>, IClientRepository
    {
        public ClientRepository(HomeBankingContext repositoryContext) : base(repositoryContext)
        {
        }

        public Client FindById(long id)
        {
            try
            {
                return FindByCondition(x => x.Id == id).Include(client => client.Accounts).FirstOrDefault();
                
            }catch(Exception ex)
            {
                throw new Exception("Error al encontrar el cliente con el id " + id + ex.Message);
            }
        }

        public  IEnumerable<Client> GetAllClients()
        {
            try
            {
                return  FindAll().Include(client=>client.Accounts).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar todos los clientes" + ex.Message);
            }
        }

        public void Save(Client client)
        {
            try
            {
                Create(client);
                SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar el cliente" + ex.Message);
            }
        }
    }
}
