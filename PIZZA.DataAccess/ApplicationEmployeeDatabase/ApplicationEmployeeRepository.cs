using Dapper;
using PIZZA.Models.Database;
using PIZZA.Models.User;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace PIZZA.DataAccess.ApplicationEmployeeDatabase
{
    public class ApplicationEmployeeRepository : DatabaseController, IApplicationEmployeeRepository
    {
        public ApplicationEmployeeRepository(DatabaseConnectionConfiguration databaseConnectionConfiguration) : base(databaseConnectionConfiguration) { }
        public async Task<List<EmployeeModel>> GetEmployees()
        {
            IEnumerable<EmployeeModel> output;
            using (var cnn = DbConnection)
            {
                var procedure = "[GetUsers]";
                output = (await cnn.QueryAsync<EmployeeModel, ApplicationRole, EmployeeModel>(
                    procedure,
                    (employee, role) => { employee.Roles.Add(role); return employee; },
                    commandType: CommandType.StoredProcedure
                    )).GroupBy(e => e.ID)
                    .Select(
                    group =>
                    {
                        var combined = group.First();
                        combined.Roles = group.Select(owner => owner.Roles.Single()).ToList();
                        return combined;
                    }).ToList();
            }
            return output.AsList();
        }

        public async Task<EmployeeModel> FindById(string userId)
        {
            EmployeeModel output;
            using (var cnn = DbConnection)
            {
                var procedure = "[FindUserById]";
                var parameters = new
                {
                    ID = userId
                };
                output = await cnn.QuerySingleOrDefaultAsync<EmployeeModel>(procedure, parameters, commandType: CommandType.StoredProcedure);
            }
            return output;
        }
    }
}
