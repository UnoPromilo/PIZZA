using System;
using System.Collections.Generic;
using System.Text;

namespace PIZZA.Models.Instalation
{
    public class FillDatabse
    {
        public bool FillDatabaseWithObjects { get; set; } = true;
        public bool FillDatabaseWithSampleData { get; set; } = false;
        public bool ClearDatabaseData { get; set; } = false;
    }
}
