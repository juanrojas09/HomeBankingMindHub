using AutoMapper;
using HomeBankingMindHub.Models;
using HomeBankingNetMvc.Models;
using HomeBankingNetMvc.Models.DTOs;
using HomeBankingNetMvc.Repositories.Interfaces;
using HomeBankingNetMvc.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HomeBankingNetMvc.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardController : ControllerBase
    {
        private IClientRepository _clientRepository;
        private ICardRepository _cardRepository;
        private IMapper _mapper;

        public CardController(IClientRepository clientRepository,  IMapper mapper, ICardRepository cardRepository)
        {
            _clientRepository = clientRepository;            
            _mapper = mapper;
            _cardRepository = cardRepository;
        }

        //hablar de esto con eduardo


        //[HttpPost("current/cards")]
        //public IActionResult Create(CreateCardDTO cardData)
        //{
        //    try
        //    {


        //        string email = User.FindFirst("Client") != null ? User.FindFirst("Client").Value : string.Empty;
        //        var _client = _clientRepository.FindByEmail(email);
        //        var cardQty=_cardRepository.GetCardsByCLientId(_client.Id);

        //        if (cardQty.Count() >= 3)
        //        {
        //            return StatusCode(403, "Limit exceeded");
        //        }
        //        var card = _mapper.Map<Card>(cardData);
        //        card.CardHolder = _client.FirstName + " " + _client.LastName;
        //        card.Number = CardDataGenerator.GenerarNumeroTarjeta();
        //        card.Cvv = CardDataGenerator.GenerarCVV();
        //        card.ThruDate = DateTime.Now.AddYears(4);
        //        card.FromDate = DateTime.Now;
        //        card.ClientId = _client.Id;

        //        _cardRepository.Save(card);

        //        return StatusCode(201,"Created");


        //    }
        //    catch
        //    {
        //        throw new Exception("Error al crear tarjeta");
        //    }

        //}

        //[HttpGet("current/cards")]
        //public IActionResult getCardsByClientId()
        //{
        //    try {
        //        string email = User.FindFirst("Client") != null ? User.FindFirst("Client").Value : string.Empty;
        //        var _client = _clientRepository.FindByEmail(email);
        //        var cards = _cardRepository.GetCardsByCLientId(_client.Id);
        //        return Ok(cards);

        //    }
        //    catch
        //    {
        //        throw new Exception("Error al traer las tarjetas asociadas al cliente" );
        //    }
        //}

     
    }
}
