using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PIZZA.DataAccess.ApplicationEmployeeDatabase;
using PIZZA.DataAccess.ApplicationUserDatabase;
using PIZZA.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PIZZA.WebApi.Controllers.Employee
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class Employee : ControllerBase
    {
        private readonly IApplicationEmployeeRepository _applicationEmployeeRepository;
        private readonly IApplicationUserRepository _applicationUserRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        public Employee(IApplicationEmployeeRepository applicationEmployeeRepository,
                        IApplicationUserRepository applicationUserRepository,
                        UserManager<ApplicationUser> userManager)
        {
            _applicationEmployeeRepository = applicationEmployeeRepository;
            _applicationUserRepository = applicationUserRepository;
            _userManager = userManager;
        }


        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] string id)
        {
            var user = await _applicationEmployeeRepository.FindById(id);
            if (user == default) return NotFound();
            await _applicationEmployeeRepository.GetRoles(user);
            return Ok(user);
        }

        [HttpDelete]
        [Authorize(Roles = "Manager, Admin")]
        public async Task<IActionResult> Delete([FromQuery] string id)
        {
            var user = await _applicationUserRepository.FindById(id);
            if (user == default) return NotFound();

            //var tmp = await _userManager.UpdateSecurityStampAsync(user);


            await _applicationUserRepository.Delete(user);
            return Ok();
        }
    }
}
