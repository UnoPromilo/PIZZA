using Microsoft.AspNetCore.Components;
using PIZZA.Models.User;

namespace PIZZA.WebAssembly.Shared.Users
{
    public partial class Record
    {
        [Parameter]
        public EmployeeModel Employee { get; set; }

        public Record()
        {

        }
    }
}
