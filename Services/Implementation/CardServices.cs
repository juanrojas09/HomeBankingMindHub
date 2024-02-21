using HomeBankingNetMvc.Models.DTOs;
using HomeBankingNetMvc.Models;
using HomeBankingNetMvc.Services.Interfaces;
using HomeBankingNetMvc.Utils;
using Microsoft.AspNetCore.Mvc;
using HomeBankingNetMvc.Repositories.Interfaces;
using AutoMapper;

namespace HomeBankingNetMvc.Services.Implementation
{
    public class CardServices:ICardServices
    {
        private ICardRepository _cardRepository;
        private IClientRepository _clientRepository;
        private IMapper _mapper;

        public CardServices(IMapper mapper, IClientRepository clientRepository, ICardRepository cardRepository)
        {
            _mapper = mapper;
            _clientRepository = clientRepository;
            _cardRepository = cardRepository;
        }

        public void Create(CreateCardDTO cardData, string email)
        {
            try
            {
                var _client = _clientRepository.FindByEmail(email);
                var cardQty = _cardRepository.GetCardsByCLientId(_client.Id);

                var debitCount = cardQty.Count(x => x.Type == CardType.DEBIT);
                var creditCount = cardQty.Count(x => x.Type == CardType.CREDIT);

                if ((cardData.Type == "DEBIT" && debitCount >= 3) ||
                          (cardData.Type == "CREDIT" && creditCount >= 3))
                {
                    throw new Exception("No se pueden crear mas de 3 tarjetas por tipo.");
                }

           




                if (Enum.TryParse(cardData.Color, out CardColor color) && Enum.TryParse(cardData.Type, out CardType type) && cardQty.Any(x => x.Color == color && x.Type == type))
                {
                    throw new Exception("No se pueden añadir 2 tarjetas del mismo tipo con el mismo color");
                }

              
                var card = _mapper.Map<Card>(cardData);
                card.CardHolder = _client.FirstName + " " + _client.LastName;
                card.Number = CardDataGenerator.GenerarNumeroTarjeta();
                card.Cvv = CardDataGenerator.GenerarCVV();
                card.ThruDate = DateTime.Now.AddYears(4);
                card.FromDate = DateTime.Now;
                card.ClientId = _client.Id;

                _cardRepository.Save(card);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al crear una tarjeta");
            }
        }

        public IEnumerable<Card> getCardsByClientId(string email)
        {
            try
            {
                var _client = _clientRepository.FindByEmail(email);
                var cards = _cardRepository.GetCardsByCLientId(_client.Id);
                return cards;

            }
            catch(Exception ex)
            {
                throw new Exception("Error al traer las tarjetas");
            }
        }
    }
}
