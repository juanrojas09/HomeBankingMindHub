using HomeBankingMindHub.Models;
using System.Text.Json.Serialization;

namespace HomeBankingNetMvc.Models.DTOs
{
    public class AccountDTO
    {
        
        public long Id { get; set; }

        public string Number { get; set; }

        public DateTime CreationDate { get; set; }

        public double Balance { get; set; }

        public Client Clients { get; set; }
        public long ClientId { get; set; }

        public ICollection<TransactionDTO> Transactions { get; set; }
    }
}

