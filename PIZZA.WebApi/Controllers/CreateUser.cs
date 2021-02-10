using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PIZZA.Models.Authentication;
using PIZZA.Models.Database;
using PIZZA.Models.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PIZZA.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles ="Admin")]
    public class CreateUser : ControllerBase
    {
        private static UserModel LoggedOutUser = new UserModel { IsAuthenticated = false };
        private readonly UserManager<ApplicationUser> _userManager;
        public CreateUser(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] RegistrationModelComplete model)
        {
            var newUser = new ApplicationUser
            {
                UserName = model.UserName,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                FirstName = model.FirstName,
                LastName = model.LastName,
                AddressLine = model.AddressLine,
                PostalCode = model.PostalCode,
                Town = model.Town
            };

            newUser.SecurityStamp = Guid.NewGuid().ToString();
            var result = await _userManager.CreateAsync(newUser, model.Password);

            
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(x => x.Description);

                return BadRequest(new RegistrationResult { Successful = false, Errors = errors });

            }
            

            return Ok(new RegistrationResult {ID = newUser.ID, Successful = true });
        }
    }
}
