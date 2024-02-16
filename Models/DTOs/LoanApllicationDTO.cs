namespace HomeBankingNetMvc.Models.DTOs
{
    public class LoanApllicationDTO
    {
        public long LoanID { get; set; }
        public double Amount { get; set; }
        public string Payments { get; set; }
        public string ToAccountNumber { get; set; }
    }
}
