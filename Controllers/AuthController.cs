using HomeBankingMindHub.Models;
using HomeBankingNetMvc.Repositories.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using HomeBankingNetMvc.Utils;
using HomeBankingNetMvc.Services.Interfaces;

namespace HomeBankingNetMvc.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IAuthServices _authServices;
        
        public AuthController(IAuthServices authServices)
        {
            
            _authServices = authServices;
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] Client client)
        {
            try
            {
                if (client == null || string.IsNullOrWhiteSpace(client.Email) || string.IsNullOrWhiteSpace(client.Password))
                {
                    return BadRequest("El cliente debe tener un correo electrónico y una contraseña válidos.");
                }

                var loginTask = _authServices.Login(client);
                var loginResult = await loginTask;

                if (loginResult == null)
                    return Unauthorized();
                
                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(loginResult));

                return Ok();

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            try
            {
                await HttpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
