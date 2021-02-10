using System.Collections.Generic;

namespace PIZZA.Models.Results
{
    public class RegistrationResult
    {
        public int ID { get; set; }
        public bool Successful { get; set; }
        public IEnumerable<string> Errors { get; set; }
    }
}
