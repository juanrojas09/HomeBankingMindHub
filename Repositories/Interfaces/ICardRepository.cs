using HomeBankingNetMvc.Models;

namespace HomeBankingNetMvc.Repositories.Interfaces
{
    public interface ICardRepository
    {

        void Save(Card card);
        IEnumerable<Card> GetCardsByCLientId(long clientId);

    }
}
