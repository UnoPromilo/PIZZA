using Microsoft.AspNetCore.Components;
using PIZZA.Models.Task;

namespace PIZZA.WebAssembly.Shared.Tasks
{
    public partial class Record
    {
        [Parameter]
        public TaskModelWithActualStateAndCreator Model { get; set; }

        public Record()
        {

        }
    }
}
