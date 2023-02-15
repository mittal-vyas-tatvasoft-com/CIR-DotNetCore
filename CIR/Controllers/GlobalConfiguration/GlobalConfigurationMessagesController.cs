﻿using CIR.Core.Interfaces.GlobalConfiguration;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CIR.Controllers.GlobalConfiguration
{
    [Route("api/[controller]")]
    [ApiController]
    public class GlobalConfigurationMessagesController : ControllerBase
    {
        #region PROPERTIES
        private readonly IGlobalConfigurationMessagesService globalConfigurationMessagesService;

        #endregion
        #region CONSTRUCTOR
        public GlobalConfigurationMessagesController(IGlobalConfigurationMessagesService globalConfigurationMessagesServices)
        {
            this.globalConfigurationMessagesService = globalConfigurationMessagesServices;
        }
        #endregion

        #region METHODS
        #endregion
    }
}
