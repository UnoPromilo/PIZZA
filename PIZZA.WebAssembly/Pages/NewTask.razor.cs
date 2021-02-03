using PIZZA.Models.Task;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PIZZA.WebAssembly.Pages
{
    public partial class NewTask
    {
        public CreateTaskModel CreateTask { get; set; } = new();
    }
}
