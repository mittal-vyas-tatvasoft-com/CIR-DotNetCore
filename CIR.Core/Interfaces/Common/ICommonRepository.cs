using Microsoft.AspNetCore.Mvc;

namespace CIR.Core.Interfaces.Common
{
    public interface ICommonRepository
    {
        Task<IActionResult> GetCountries();
    }
}
