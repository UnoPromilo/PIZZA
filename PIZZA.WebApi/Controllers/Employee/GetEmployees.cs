using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PIZZA.DataAccess.ApplicationEmployeeDatabase;
using PIZZA.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PIZZA.WebApi.Controllers.Employee
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles ="Admin, Manager")]
    public class GetEmployees : ControllerBase
    {
        private readonly IApplicationEmployeeRepository _applicationEmployeeRepository;
        public GetEmployees(IApplicationEmployeeRepository applicationEmployeeRepository)
        {
            _applicationEmployeeRepository = applicationEmployeeRepository;
        }
        [HttpGet]
        public async Task<List<EmployeeModel>> Get([FromQuery]string query = null)
        {
            return await _applicationEmployeeRepository.GetEmployees(query);
        }
    }
}
