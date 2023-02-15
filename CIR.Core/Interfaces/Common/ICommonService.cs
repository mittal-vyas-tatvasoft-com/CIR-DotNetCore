using Microsoft.AspNetCore.Mvc;

namespace CIR.Core.Interfaces.Common
{
    public interface ICommonService
    {
        Task<IActionResult> GetCountries();
        Task<IActionResult> GetCultures();
    }
}
