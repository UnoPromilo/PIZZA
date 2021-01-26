using Microsoft.AspNetCore.Components.Web;
using PIZZA.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PIZZA.WebAssembly.Pages
{
    public partial class Users
    {
        private Employee employee = new()
        {
            FirstName = "Jan",
            LastName = "Kowalski",
            Email = "jankowalski@gmail.com",
            PhoneNumber = "661872052"
        };

    }
}
