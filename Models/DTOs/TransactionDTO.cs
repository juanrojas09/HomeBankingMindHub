using System.Text.Json.Serialization;

namespace HomeBankingNetMvc.Models.DTOs
{
    public class TransactionDTO
    {
        
        public long Id { get; set; }
        public string Type { get; set; }
        public DateTime Date { get; set; }
        public double Amount { get; set; }
        public string Description { get; set; }

        public Account Account { get; set; }
        [JsonIgnore]
        public long AccountId { get; set; }
    }
}
