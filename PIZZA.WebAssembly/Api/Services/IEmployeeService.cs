using PIZZA.Models.Authentication;
using PIZZA.Models.Results;
using PIZZA.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PIZZA.WebAssembly.Api.Services
{
    public interface IEmployeeService
    {
        Task<RegistrationResult> CreateEmployee(RegistrationModelComplete model);
        Task<bool> DeleteEmployee(string id);
        Task<EmployeeModel> GetEmployee(string id);
        Task<List<EmployeeModel>> GetEmployees(CancellationToken cancellationToken, string query);
        Task<bool> UpdateEmployee(EmployeeModel employeeModel);
    }
}
