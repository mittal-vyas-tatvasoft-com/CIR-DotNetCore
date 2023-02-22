using CIR.Core.Entities.GlobalConfiguration;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIR.Core.Interfaces.GlobalConfiguration
{
    public interface IGlobalConfigurationMessagesRepository
    {
        Task<IActionResult> GetGlobalConfigurationMessagesList(int cultureId);
        Task<IActionResult> CreateOrUpdateGlobalConfigurationMessages(List<GlobalConfigurationMessage> globalConfigurationMessages);
    }
}
