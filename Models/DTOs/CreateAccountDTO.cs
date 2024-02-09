using HomeBankingMindHub.Models;

namespace HomeBankingNetMvc.Models.DTOs
{
    public class CreateAccountDTO
    {
        public string Number { get; set; }

        public DateTime CreationDate { get; set; }

        public double Balance { get; set; }

        public Client Clients { get; set; }
        public long ClientId { get; set; }
    }
}
