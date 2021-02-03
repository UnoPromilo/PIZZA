using Dapper;
using PIZZA.Models.Database;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIZZA.DataAccess.ApplicationRoleDatabase
{
    public class ApplicationRoleRepository : DatabaseController, IApplicationRoleRepository
    {
        public ApplicationRoleRepository(DatabaseConnectionConfiguration databaseConnectionConfiguration) : base(databaseConnectionConfiguration) { }
        public async Task<int> Create(ApplicationRole role)
        {
            int id = 0;
            using (var cnn = DbConnection)
            {
                var procedure = "[CreateRole]";

                id = await cnn.QuerySingleOrDefaultAsync<int>(procedure, role, commandType: CommandType.StoredProcedure);
            }
            return id;
        }

        public async Task<int> Update(ApplicationRole role)
        {
            int output;
            using (var cnn = DbConnection)
            {
                var procedure = "[UpdateRole]";
                output = await cnn.ExecuteAsync(procedure, role, commandType: CommandType.StoredProcedure);
            }
            return output;
        }

        public async Task Delete(ApplicationRole role)
        {
            using (var cnn = DbConnection)
            {
                var sql = $"DELETE FROM [ApplicationRole] WHERE [ID] = @{nameof(ApplicationRole.ID)}";
                await cnn.ExecuteAsync(sql, role);
            }
        }
        public async Task<ApplicationRole> FindById(string roleId)
        {
            ApplicationRole output;
            using (var cnn = DbConnection)
            {
                var procedure = "[FindRoleById]";
                var parameters = new
                {
                    ID = roleId
                };
                output = await cnn.QuerySingleOrDefaultAsync<ApplicationRole>(procedure, parameters, commandType: CommandType.StoredProcedure);
            }
            return output;
        }
        public async Task<ApplicationRole> FindByName(string normalizedRoleName)
        {
            ApplicationRole output;
            using (var cnn = DbConnection)
            {
                var procedure = "[FindRoleByName]";
                var parameters = new
                {
                    NormalizedRoleName = normalizedRoleName
                };
                output = await cnn.QuerySingleOrDefaultAsync<ApplicationRole>(procedure, parameters, commandType: CommandType.StoredProcedure);
            }
            return output;
        }

    }
}
