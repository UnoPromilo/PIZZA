﻿using Microsoft.AspNetCore.Components;
using PIZZA.Models.User;
using System.Linq;

namespace PIZZA.WebAssembly.Shared.Users
{
    public partial class Record
    {
        [Parameter]
        public EmployeeModel Employee { get; set; }

        public Record()
        {

        }

        public bool IsAdmin { 
            get
            {
                return Employee.Roles.Where(r => r.NormalizedName == "ADMIN").Count() > 0;
            }
        }
        public bool IsManager
        {
            get
            {
                return Employee.Roles.Where(r => r.NormalizedName == "MANAGER").Count() > 0;
            }
        }
    }
}
