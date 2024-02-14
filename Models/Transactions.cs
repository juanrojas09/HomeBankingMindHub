﻿namespace HomeBankingNetMvc.Models
{
    public class Transactions
    {
        public long Id { get; set; }
        public TransactionType Type { get; set; }
        public DateTime Date { get; set; }
        public double Amount { get; set; }
        public string Description { get; set; }

        public Account Account { get; set; }
        public long AccountId { get; set; }


    }
}