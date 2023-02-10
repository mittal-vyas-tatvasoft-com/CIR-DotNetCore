using CIR.Core.Entities.GlobalConfiguration;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIR.Core.ViewModel.GlobalConfiguration
{
    public class GlobalConfigurationCurrencyModel
    {
        [Required]
        public long CountryId { get; set; }

        [Required]
        public long CurrencyId { get; set; }
        
        [Required]
        public bool Enabled { get; set; }
    }

}
