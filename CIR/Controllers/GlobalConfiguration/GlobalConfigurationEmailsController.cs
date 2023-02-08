using CIR.Core.Interfaces.GlobalConfiguration;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CIR.Controllers.GlobalConfiguration
{
    [Route("api/[controller]")]
    [ApiController]
    public class GlobalConfigurationEmailsController : ControllerBase
    {
        #region PROPERTIES
        private readonly IGlobalConfigurationEmailsService _globalConfigurationEmailsService;
        #endregion

        #region CONSTRUCTORS
        public GlobalConfigurationEmailsController(IGlobalConfigurationEmailsService globalConfigurationEmailsService)
        {
            _globalConfigurationEmailsService = globalConfigurationEmailsService;
        }
        #endregion

        #region METHODS
        #endregion
    }
}
