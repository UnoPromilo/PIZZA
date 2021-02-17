using PIZZA.Models.Database;
using System.Threading.Tasks;

namespace PIZZA.DataAccess.ApplicationRoleDatabase
{
    public interface IApplicationRoleRepository
    {
        Task<int> Create(ApplicationRole role);
        Task Delete(ApplicationRole role);
        Task<ApplicationRole> FindById(string roleId);
        Task<ApplicationRole> FindByName(string normalizedRoleName);
        Task<int> Update(ApplicationRole user);
    }
}
