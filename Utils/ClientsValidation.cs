using HomeBankingMindHub.Models;
using System.Text.RegularExpressions;

namespace HomeBankingNetMvc.Utils
{
    public class ClientsValidation
    {

        public static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return false;
            }
            
            return Regex.IsMatch(email, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
        }

        public static bool AreClientDataValid(Client client)
        {
            return !string.IsNullOrEmpty(client.Email) &&
                   !string.IsNullOrEmpty(client.Password) &&
                   !string.IsNullOrEmpty(client.FirstName) &&
                   !string.IsNullOrEmpty(client.LastName);
        }
    }
}
