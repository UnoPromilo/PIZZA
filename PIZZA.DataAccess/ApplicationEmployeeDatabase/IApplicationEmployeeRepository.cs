using PIZZA.Models.User;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PIZZA.DataAccess.ApplicationEmployeeDatabase
{
    public interface IApplicationEmployeeRepository
    {
        Task<EmployeeModel> FindById(string userId);
        Task<List<EmployeeModel>> GetEmployees(string query);
        Task<IList<string>> GetRoles(EmployeeModel employee);
    }
}
