using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PIZZA.DataAccess.ApplicationEmployeeDatabase;
using PIZZA.DataAccess.ApplicationUserDatabase;
using PIZZA.Models.User;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PIZZA.WebApi.Controllers.Employee
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class UpdateEmployee : ControllerBase
    {
        private readonly IApplicationEmployeeRepository _applicationEmployeeRepository;
        private readonly IApplicationUserRepository _applicationUserRepository;
        public UpdateEmployee(IApplicationEmployeeRepository applicationEmployeeRepository,
                              IApplicationUserRepository applicationUserRepository)
        {
            _applicationEmployeeRepository = applicationEmployeeRepository;
            _applicationUserRepository = applicationUserRepository;
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] EmployeeModel model)
        {
            var user = await _applicationUserRepository.FindById(model.ID.ToString());
            List<Task> tasks = new List<Task>();

            if (user == default) return NotFound();

            user.Email = model.Email;
            user.PhoneNumber = model.PhoneNumber;
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.AddressLine = model.AddressLine;
            user.PostalCode = model.PostalCode;
            user.Town = model.Town;

            tasks.Add(_applicationUserRepository.Update(user));

            var listOfCurrentRoles = await _applicationUserRepository.GetRoles(user);
            var listOfNewRoles = model.Roles;
            
            foreach(var item in FindDifferences(listOfNewRoles, listOfCurrentRoles))
                tasks.Add(_applicationUserRepository.AddToRole(user, item));
            
            foreach (var item in FindDifferences(listOfCurrentRoles, listOfNewRoles))
                tasks.Add(_applicationUserRepository.RemoveFromRole(user, item));

            await Task.WhenAll(tasks);

            return Ok();
        }

        private List<string> FindDifferences(IList<string> baseList, IList<string> secondList)
        {
            List<string> output = new();

            foreach (var item1 in baseList)
            {
                bool isInList = false;
                foreach (var item2 in secondList)
                {
                    if (item1.Normalize() == item2.Normalize())
                    {
                        isInList = true;
                        break;
                    }
                }
                if (!isInList)
                {
                    output.Add(item1);
                }
            }

            return output;
        }
    }
}
