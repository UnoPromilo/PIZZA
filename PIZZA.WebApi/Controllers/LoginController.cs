using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PIZZA.Models.Authentication;
using PIZZA.Models.Database;
using PIZZA.Models.Results;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PIZZA.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly CustomSettings _configuration;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public LoginController(CustomSettings configuration,
                               SignInManager<ApplicationUser> signInManager)
        {
            _configuration = configuration;
            _signInManager = signInManager;
        }

        /// <summary>
        /// Login user using username and pasword
        /// </summary>
        /// <param name="login">Details about user</param>
        /// <returns>
        /// Jwt Security Token
        /// </returns>
        /// <response code="202">Success.</response>
        /// <response code="401">Authentication credentials are invalid.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Post([FromBody] LoginModel login)
        {
            var result = await _signInManager.PasswordSignInAsync(login.UserName, login.Password, false, false);

            if (!result.Succeeded) return Unauthorized(new LoginResult { Successful = false, Error = "Nazwa użytkownika lub hasło są niepoprawne." });


            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, login.UserName));
            var user = await _signInManager.UserManager.FindByNameAsync(login.UserName);
            var roles = await _signInManager.UserManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.JwtSecurityKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiry = DateTime.Now.AddDays(Convert.ToInt32(_configuration.JwtExpiryInDays));

            var token = new JwtSecurityToken(
                _configuration.JwtIssuer,
                _configuration.JwtAudience,
                claims,
                expires: expiry,
                signingCredentials: creds
            
            );
            return Accepted(new LoginResult { Successful = true, Token = new JwtSecurityTokenHandler().WriteToken(token) });
        }
    }
}
