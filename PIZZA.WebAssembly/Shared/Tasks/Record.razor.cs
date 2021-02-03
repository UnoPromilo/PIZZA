using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using PIZZA.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PIZZA.WebAssembly.Shared.Tasks
{
    public partial class Record
    {
        [Parameter]
        public Employee Employee { get; set; }

        public Record()
        {

        }
    }
}
