using CIR.Core.Entities.GlobalConfiguration;
using Microsoft.AspNetCore.Mvc;

namespace CIR.Core.Interfaces.GlobalConfiguration
{
    public interface IGlobalConfigurationFontsRepository
    {
        Task<IActionResult> GetGlobalConfigurationFonts();
        Task<IActionResult> CreateGlobalConfigurationFonts(GlobalConfigurationFonts globalConfigurationFonts);
        Task<IActionResult> UpdateGlobalConfigurationFonts(List<GlobalConfigurationFonts> globalConfigurationFonts);
    }
}
