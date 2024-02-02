﻿using HomeBankingMindHub.Models;

namespace HomeBankingNetMvc.Repositories.Interfaces
{
    public interface IClientRepository
    {
        IEnumerable<Client> GetAllClients();
        void Save(Client client);
        Client FindById(long id);
    }
}