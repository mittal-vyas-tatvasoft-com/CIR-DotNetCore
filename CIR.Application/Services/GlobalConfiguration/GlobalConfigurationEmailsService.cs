using CIR.Core.Entities.GlobalConfiguration;
using CIR.Core.Interfaces.GlobalConfiguration;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIR.Application.Services.GlobalConfiguration
{
    public class GlobalConfigurationEmailsService : IGlobalConfigurationEmailsService
    {
        private readonly IGlobalConfigurationEmailsRepository iGlobalConfigurationEmailsRepository;

        public GlobalConfigurationEmailsService(IGlobalConfigurationEmailsRepository globalConfigurationEmailsRepository)
        {
            iGlobalConfigurationEmailsRepository = globalConfigurationEmailsRepository;
        }


        public async Task<IActionResult> CreateOrUpdateGlobalConfigurationEmails(List<GlobalConfigurationEmails> globalConfigurationEmails)
        {
            return await iGlobalConfigurationEmailsRepository.CreateOrUpdateGlobalConfigurationEmails(globalConfigurationEmails);
        }
        public async Task<IActionResult> GetGlobalConfigurationEmailsDataList(int cultureId)
        {
            return await iGlobalConfigurationEmailsRepository.GetGlobalConfigurationEmailsDataList(cultureId);

        }
    }
}
