using CIR.Core.Interfaces.Utilities;
using CIR.Core.ViewModel.Utilities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIR.Application.Services.Utilities
{
    public class SystemSettingsLookupsService : ISystemSettingsLookupsService
    {
        #region PROPERTIES
        private readonly ISystemSettingsLookupsRepository lookupsRepository;
        #endregion

        #region CONSTRUCTOR
        public SystemSettingsLookupsService(ISystemSettingsLookupsRepository lookupsRepository)
        {
            lookupsRepository = lookupsRepository;
        }
        #endregion

        #region METHODS

        public Task<IActionResult> CreateOrUpdateLookupItem(LookupItemsTextModel lookupItemsTextModel)
        {
            return lookupsRepository.CreateOrUpdateLookupItem(lookupItemsTextModel);
        }

        public async Task<bool> LookupItemExists(long cultureId, long lookupItemId)
        {
            return await lookupsRepository.LookupItemExists(cultureId, lookupItemId);
        }

        public Task<IActionResult> GetAllLookupsItems(long cultureId, string code, string? searchLookupItems, bool sortAscending = true)
        {
            return lookupsRepository.GetAllLookupsItems(cultureId, code, searchLookupItems, sortAscending);
        }

        public Task<IActionResult> GetLookupById(int id)
        {
            return lookupsRepository.GetLookupById(id);
        }
        #endregion
    }
}
