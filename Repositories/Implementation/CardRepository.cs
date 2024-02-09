using HomeBankingMindHub.Models;
using HomeBankingNetMvc.Models;
using HomeBankingNetMvc.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HomeBankingNetMvc.Repositories.Implementation
{
    public class CardRepository : RepositoryBase<Card>, ICardRepository
    {

        

        public CardRepository(HomeBankingContext homeBankingContext) :base(homeBankingContext)
        {
          
        }

        public IEnumerable<Card> GetCardsByCLientId(long clientId)
        {

            try
            {

                var Cards = FindByCondition(x => x.ClientId == clientId)
                 .ToList();

                return Cards;

            }
            catch (Exception ex)
            {
                throw new Exception("Error al traer tarjetas por cliente" + ex.Message);
            }
           
        }

        public void Save(Card card)
        {
            try
            {
                Create(card);
                SaveChanges();

            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar una tarjeta" + ex.Message);
            }
        }
    }
}
