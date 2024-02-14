﻿using HomeBankingNetMvc.Models.DTOs;
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

                if (cardQty.Count() >= 3)
                {
                    throw new Exception("Limite de tarjetas excedidos");
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