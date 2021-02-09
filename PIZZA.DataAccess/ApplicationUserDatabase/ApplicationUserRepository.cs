using System.Threading.Tasks;
using System.Threading;
using PIZZA.Models.Database;
using Dapper;
using System.Data;
using System.Collections.Generic;

namespace PIZZA.DataAccess.ApplicationUserDatabase
{
    public class ApplicationUserRepository : DatabaseController, IApplicationUserRepository
    {
        public ApplicationUserRepository(DatabaseConnectionConfiguration databaseConnectionConfiguration) : base(databaseConnectionConfiguration) { }

        public async Task<int> Create(ApplicationUser user)
        {
            int id = 0;
            using (var cnn = DbConnection)
            {
                var procedure = "[CreateUser]";
                var parameter = new
                {
                    user.UserName,
                    user.NormalizedUserName,
                    user.Email,
                    user.NormalizedEmail,
                    user.EmailConfirmed,
                    user.PasswordHash,
                    user.ForcePasswordChangeWhileNextLogin,
                    user.PhoneNumber,
                    user.PhoneNumberConfirmed,
                    user.TwoFactorEnabled,
                    user.SecurityStamp,
                    user.FirstName,
                    user.LastName,
                    user.AddressLine,
                    user.PostalCode,
                    user.Town
                };
                id = await cnn.QuerySingleOrDefaultAsync<int>(procedure, parameter, commandType: CommandType.StoredProcedure);
            }
            return id;
        }

        public async Task Delete(ApplicationUser user)
        {
            using (var cnn = DbConnection)
            {
                var sql = $"DELETE FROM [ApplicationUser] WHERE [ID] = @{nameof(ApplicationUser.ID)}";
                await cnn.ExecuteAsync(sql, user);
            }
        }

        public async Task<ApplicationUser> FindById(string userId)
        {
            ApplicationUser output;
            using (var cnn = DbConnection)
            {
                var procedure = "[FindUserById]";
                var parameters = new
                {
                    ID = userId
                };
                output = await cnn.QuerySingleOrDefaultAsync<ApplicationUser>(procedure, parameters, commandType: CommandType.StoredProcedure);
            }
            return output;
        }
        public async Task<ApplicationUser> FindByName(string normalizedUserName)
        {
            ApplicationUser output;
            using (var cnn = DbConnection)
            {
                var procedure = "[FindUserByName]";
                var parameters = new
                {
                    NormalizedUserName = normalizedUserName
                };
                output = await cnn.QuerySingleOrDefaultAsync<ApplicationUser>(procedure, parameters, commandType: CommandType.StoredProcedure);
            }
            return output;
        }

        public async Task<ApplicationUser> FindByEmail(string normalizedEmail)
        {
            ApplicationUser output;
            using (var cnn = DbConnection)
            {
                var procedure = "[FindUserByEmail]";
                var parameters = new
                {
                    NormalizedEmail = normalizedEmail
                };
                output = await cnn.QuerySingleOrDefaultAsync<ApplicationUser>(procedure, parameters, commandType: CommandType.StoredProcedure);
            }
            return output;
        }

        public async Task<int> Update(ApplicationUser user)
        {
            int output;
            using (var cnn = DbConnection)
            {
                var procedure = "[UpdateUser]";
                output = await cnn.ExecuteAsync(procedure, user, commandType: CommandType.StoredProcedure);
            }
            return output;
        }

        public async Task AddToRole(ApplicationUser user, string roleName)
        {
            using (var cnn = DbConnection)
            {
                var procedure = "[AddUserToRole]";
                var parameters = new
                {
                    UserID = user.ID,
                    RoleName = roleName
                };
                await cnn.ExecuteAsync(procedure, parameters, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task RemoveFromRole(ApplicationUser user, string roleName)
        {
            using (var cnn = DbConnection)
            {
                var procedure = "[RemoveUserFromRole]";
                var parameters = new
                {
                    UserID = user.ID,
                    RoleName = roleName
                };
                await cnn.ExecuteAsync(procedure, parameters, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<IList<string>> GetRoles(ApplicationUser user)
        {
            IList<string> output;
            using (var cnn = DbConnection)
            {
                var procedure = "[GetUserRoles]";
                var parameters = new
                {
                    UserID = user.ID
                };
                output = (await cnn.QueryAsync<string>(procedure, parameters, commandType: CommandType.StoredProcedure)).AsList();
            }
            return output;
        }

        public async Task<bool> IsInRole(ApplicationUser user, string roleName)
        {
            bool output;
            using (var cnn = DbConnection)
            {
                var procedure = "[IsUserInRole]";
                var parameter = new
                {
                    UserID = user.ID,
                    RoleName = roleName
                };
                output = await cnn.QuerySingleAsync<bool>(procedure, parameter, commandType: CommandType.StoredProcedure);
            }
            return output;
        }

        public async Task<IList<ApplicationUser>> GetUsersInRole(string roleName)
        {
            IEnumerable<ApplicationUser> output;
            using (var cnn = DbConnection)
            {
                var procedure = "[GetUsersInRole]";
                var parameter = new
                {
                    RoleName = roleName
                };
                output = await cnn.QueryAsync<ApplicationUser>(procedure, parameter, commandType: CommandType.StoredProcedure);
            }
            return output.AsList();
        }

        public async Task<string> GetSecurityStamp(ApplicationUser applicationUser)
        {
            string output;
            using (var cnn = DbConnection)
            {
                var procedure = "[GetSecurityStamp]";
                var parameters = new
                {
                    applicationUser.ID
                };
                output = await cnn.QuerySingleOrDefaultAsync<string>(procedure, parameters, commandType: CommandType.StoredProcedure);
            }
            applicationUser.SecurityStamp = output;
            return output;
        }

        public async Task UpdateSecurityStamp(ApplicationUser applicationUser, string securityStamp)
        {
            using (var cnn = DbConnection)
            {
                var procedure = "[UpdateSecurityStamp]";
                var parameters = new
                {
                    ID = applicationUser.ID,
                    SecurityStamp = securityStamp
                };
                await cnn.ExecuteAsync(procedure, parameters, commandType: CommandType.StoredProcedure);
                applicationUser.SecurityStamp = securityStamp;
            }
        }
    }
}
