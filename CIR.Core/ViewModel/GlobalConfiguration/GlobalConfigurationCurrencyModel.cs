﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIR.Core.ViewModel.GlobalConfiguration
{
    public class GlobalConfigurationCurrencyModel
    {
        public long Id { get; set; }

        public long CountryId { get; set; }

        public long CurrencyId { get; set; }

        public bool Enabled { get; set; }
        public string CountryName { get; set; }
        public string CodeName { get; set; }
    }
}
