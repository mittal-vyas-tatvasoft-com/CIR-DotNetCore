using CIR.Core.Entities.GlobalConfiguration;
using CIR.Core.ViewModel.GlobalConfiguration;
using Microsoft.AspNetCore.Mvc;

namespace CIR.Core.Interfaces.GlobalConfiguration
{
    public interface IGlobalConfigurationCurrenciesRepository
    {
        Task<IActionResult> GetGlobalConfigurationCurrenciesCountryWise(int countryId);
        Task<IActionResult> CreateOrUpdateGlobalConfigurationCurrencies(List<GlobalConfigurationCurrencyModel> globalConfigurationCurrencyModels);
        Task<Boolean> CurrencyExists(string codeName);
        Task<IActionResult> AddNewCurrency(Currency currency);
    }
}
