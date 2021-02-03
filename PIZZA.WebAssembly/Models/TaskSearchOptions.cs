using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PIZZA.WebAssembly.Models
{
    public class TaskSearchOptions
    {
        public bool ShowOnlyNotFinished { get; set; } = true;
        public bool ShowUnassignedToMe { get; set; } = false;
    }
}
