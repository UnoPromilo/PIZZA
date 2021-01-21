using System;
using System.Collections.Generic;
using System.Text;

namespace PIZZA.Models.Results
{
    public class RegistrationResult
    {
        public bool Successful { get; set; }
        public IEnumerable<string> Errors { get; set; }
    }
}
