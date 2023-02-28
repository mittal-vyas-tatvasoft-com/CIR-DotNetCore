using CIR.Core.Interfaces.GlobalConfiguration;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CIR.Controllers.GlobalConfiguration
{
    [Route("api/[controller]")]
    [ApiController]
    public class GlobalConfigurationStylesController : ControllerBase
    {
        #region PROPERTIES
        private readonly IGlobalConfigurationStylesService globalConfigurationStylesService;
        #endregion

        #region CONSTRUCTOR
        public GlobalConfigurationStylesController(IGlobalConfigurationStylesService _globalConfigurationStylesService)
        {
            globalConfigurationStylesService = _globalConfigurationStylesService;
        }
        #endregion

        #region METHODS
        #endregion
    }
}
