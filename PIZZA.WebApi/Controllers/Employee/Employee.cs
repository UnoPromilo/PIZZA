using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PIZZA.DataAccess.ApplicationEmployeeDatabase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PIZZA.WebApi.Controllers.Employee
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Manager, Admin")]
    public class Employee : ControllerBase
    {
        private readonly IApplicationEmployeeRepository _applicationEmployeeRepository;
        public Employee(IApplicationEmployeeRepository applicationEmployeeRepository)
        {
            _applicationEmployeeRepository = applicationEmployeeRepository;
        }
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] string id)
        {
            var user = await _applicationEmployeeRepository.FindById(id);
            if (user == default) return NotFound();
            else return Ok(user);
        }
    }
}
