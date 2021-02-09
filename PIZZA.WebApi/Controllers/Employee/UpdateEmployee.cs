using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PIZZA.DataAccess.ApplicationEmployeeDatabase;
using PIZZA.DataAccess.ApplicationUserDatabase;
using PIZZA.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PIZZA.WebApi.Controllers.Employee
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class UpdateEmployee : ControllerBase
    {
        private readonly IApplicationEmployeeRepository _applicationEmployeeRepository;
        private readonly IApplicationUserRepository _applicationUserRepository;
        public UpdateEmployee(IApplicationEmployeeRepository applicationEmployeeRepository,
                              IApplicationUserRepository applicationUserRepository)
        {
            _applicationEmployeeRepository = applicationEmployeeRepository;
            _applicationUserRepository = applicationUserRepository;
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] EmployeeModel model)
        {
            var user = await _applicationUserRepository.FindById(model.ID.ToString());

            if (user == default) return NotFound();

            user.UserName = model.UserName;
            user.Email = model.Email;
            user.PhoneNumber = model.PhoneNumber;
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.AddressLine = model.AddressLine;
            user.PostalCode = model.PostalCode;
            user.Town = model.Town;

            await _applicationUserRepository.Update(user);

            return Ok();
        }
    }
}
