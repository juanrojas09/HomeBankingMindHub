﻿using HomeBankingMindHub.Models;

namespace HomeBankingNetMvc.Models
{
    public class ClientLoan
    {
        public long Id { get; set; }
        public double Amount { get; set; }
        public string Payments { get; set; }
        public long ClientId { get; set; }
        public long LoanId { get; set; }
        public Client Client { get; set; }
        public Loan Loan { get; set; }



    }
}
