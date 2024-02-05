﻿using System.Text.Json.Serialization;

namespace HomeBankingNetMvc.Models.DTOs
{
    public class ClientDTOReq
    {
        [JsonIgnore]

        public long Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }
        [JsonIgnore]
        public ICollection<AccountDTO> Accounts { get; set; }
    }
}