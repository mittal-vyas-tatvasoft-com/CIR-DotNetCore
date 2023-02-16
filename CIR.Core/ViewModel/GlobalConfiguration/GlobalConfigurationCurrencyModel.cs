using System.ComponentModel.DataAnnotations;

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
