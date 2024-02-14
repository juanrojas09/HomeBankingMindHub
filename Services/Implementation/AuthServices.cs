using HomeBankingMindHub.Models;
using HomeBankingNetMvc.Repositories.Interfaces;
using HomeBankingNetMvc.Services.Interfaces;
using HomeBankingNetMvc.Utils;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HomeBankingNetMvc.Services.Implementation
{
    public class AuthServices : IAuthServices
    {
        private IClientRepository _clientRepository;

        public AuthServices(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public async Task<ClaimsPrincipal> Login([FromBody] Client client)
        {
            try
            {
                Client user = _clientRepository.FindByEmail(client.Email);
                var hashedPass = Encrypt.HashPassword(client.Password);

                if (user == null || !String.Equals(user.Password, hashedPass))
                   throw new Exception("Error al loguear usuario");

                var claims = new List<Claim>
                {
                    new Claim("Client", user.Email),
                };

                var claimsIdentity = new ClaimsIdentity(
                    claims,
                    CookieAuthenticationDefaults.AuthenticationScheme
                    );
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);


                return claimsPrincipal;

            }
            catch (Exception ex)
            {
                throw new Exception("Error al loguear usuario" + ex.Message);
            }
        }

      
    }
}
