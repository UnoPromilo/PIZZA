using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PIZZA.Models.Database;
using System.Threading.Tasks;

namespace PIZZA.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class TestAuthorizationStatus : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public TestAuthorizationStatus(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var roles = await _userManager.GetRolesAsync(user);
            return Ok(roles);
        }
    }
}
