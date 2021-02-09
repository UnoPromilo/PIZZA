using PIZZA.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIZZA.DataAccess.ApplicationUserDatabase
{
    public interface IApplicationUserRepository
    {
        Task AddToRole(ApplicationUser user, string roleName);
        Task<int> Create(ApplicationUser user);
        Task Delete(ApplicationUser user);
        Task<ApplicationUser> FindByEmail(string normalizedEmail);
        Task<ApplicationUser> FindById(string userId);
        Task<ApplicationUser> FindByName(string normalizedUserName);
        Task<IList<string>> GetRoles(ApplicationUser user);
        Task<string> GetSecurityStamp(ApplicationUser applicationUser);
        Task<IList<ApplicationUser>> GetUsersInRole(string roleName);
        Task<bool> IsInRole(ApplicationUser user, string roleName);
        Task RemoveFromRole(ApplicationUser user, string roleName);
        Task<int> Update(ApplicationUser user);
        Task UpdateSecurityStamp(ApplicationUser applicationUser, string securityStamp);
    }
}
