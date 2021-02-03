using PIZZA.Models.Authentication;
using PIZZA.Models.Results;
using System.Threading.Tasks;

namespace PIZZA.WebAssembly.Api.Services
{
    public interface IAuthService
    {
        Task<LoginResult> Login(LoginModel loginModel);
        Task Logout();
        Task<RegistrationResult> Register(RegistrationModel registerModel);
    }
}
