using HomeBankingNetMvc.Models;
using HomeBankingNetMvc.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace HomeBankingNetMvc.Services.Interfaces
{
    public interface ICardServices
    {
        public void Create(CreateCardDTO cardData, string email);
        public IEnumerable<Card> getCardsByClientId(string email);
    }
}
