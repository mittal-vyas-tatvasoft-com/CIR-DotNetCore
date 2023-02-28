using CIR.Common.Data;
using CIR.Core.Interfaces.GlobalConfiguration;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIR.Data.Data.GlobalConfiguration
{
    public class GlobalConfigurationStylesRepository : ControllerBase, IGlobalConfigurationStylesRepository
    {
        #region PROPERTIES   
        private readonly CIRDbContext cIRDBContext;
        #endregion

        #region CONSTRUCTOR
        public GlobalConfigurationStylesRepository(CIRDbContext context)
        {
            cIRDBContext = context ??
               throw new ArgumentNullException(nameof(context));
        }
        #endregion

        #region METHODS
        #endregion
    }
}
