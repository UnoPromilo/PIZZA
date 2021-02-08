using PIZZA.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PIZZA.WebAssembly.Api.Services
{
    public interface IEmployeeService
    {
        Task<EmployeeModel> GetEmployee(string id);
        Task<List<EmployeeModel>> GetEmployees();
    }
}
