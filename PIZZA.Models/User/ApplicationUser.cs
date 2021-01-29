using System;
using System.Collections.Generic;
using System.Text;

namespace PIZZA.Models.User
{
    public class ApplicationUser : IEmployee
    {
        public int ID { get; set; }

        public string UserName { get; set; }

        public string NormalizedUserName { get; set; }

        public string Email { get; set; }

        public string NormalizedEmail { get; set; }

        public bool EmailConfirmed { get; set; }

        public string PasswordHash { get; set; }

        public string PhoneNumber { get; set; }

        public bool PhoneNumberConfirmed { get; set; }

        public bool TwoFactorEnabled { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string AddressLine { get; set; }

        public string PostalCode { get; set; }

        public string Town { get; set; }
    }
}
