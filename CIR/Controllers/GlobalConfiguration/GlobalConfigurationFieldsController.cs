using CIR.Core.Interfaces.GlobalConfiguration;
using Microsoft.AspNetCore.Mvc;

namespace CIR.Controllers.GlobalConfiguration
{
    [Route("api/[controller]")]
    [ApiController]
    public class GlobalConfigurationFieldsController : ControllerBase
    {
        #region PROPERTIES
        private readonly IGlobalConfigurationFieldsService globalConfigurationFieldsService;

        #endregion
        #region CONSTRUCTOR
        public GlobalConfigurationFieldsController(IGlobalConfigurationFieldsService iGlobalConfigurationFieldsServices)
        {
            globalConfigurationFieldsService = iGlobalConfigurationFieldsServices;
        }
        #endregion

        #region METHODS
        #endregion
    }
}
