using CIR.Core.Entities.GlobalConfiguration;
using Microsoft.AspNetCore.Mvc;

namespace CIR.Core.Interfaces.GlobalConfiguration
{
    public interface IGlobalConfigurationFontsServices
    {
        Task<IActionResult> GetGlobalConfigurationFonts();
        Task<IActionResult> UpdateGlobalConfigurationFonts(List<GlobalConfigurationFonts> globalConfigurationFonts);
        Task<IActionResult> CreateGlobalConfigurationFonts(GlobalConfigurationFonts globalConfigurationFonts);
    }
}
