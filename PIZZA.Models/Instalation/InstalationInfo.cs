﻿using System;
using System.Collections.Generic;
using System.Text;

namespace PIZZA.Models.Instalation
{
    public class InstalationInfo
    {
        public AdministratorUserCreationModel AdministratorUserCreationModel { get; set; }
        public SQLServerConfiguration SQLServerConfiguration { get; set; }
        public FillDatabse FillDatabse { get; set; }
    }
}
