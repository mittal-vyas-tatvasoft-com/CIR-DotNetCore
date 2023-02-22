using CIR.Core.Interfaces.Common;
using Microsoft.AspNetCore.Mvc;

namespace CIR.Application.Services.Common
{
    public class CommonService : ICommonService
    {
        private readonly ICommonRepository commonRepository;

        public CommonService(ICommonRepository commonRepository)
        {
            this.commonRepository = commonRepository;
        }

        public async Task<IActionResult> GetCurrencies()
        {
            return await commonRepository.GetCurrencies();
        }

        public async Task<IActionResult> GetCountries()
        {
            return await commonRepository.GetCountries();
        }
        public async Task<IActionResult> GetCultures()
        {
            return await commonRepository.GetCultures();
        }
    }
}
