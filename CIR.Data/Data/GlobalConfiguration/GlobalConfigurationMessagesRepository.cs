using CIR.Common.Data;
using CIR.Core.Interfaces.GlobalConfiguration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIR.Data.Data.GlobalConfiguration
{
    public class GlobalConfigurationMessagesRepository : IGlobalConfigurationMessagesRepository
    {
        #region PROPERTIES
        private readonly CIRDbContext cIRDbContext;
        #endregion

        #region CONSTRUCTOR
        public GlobalConfigurationMessagesRepository(CIRDbContext context)
        {
            cIRDbContext = context ??
               throw new ArgumentNullException(nameof(context));
        }

        #endregion

        #region METHODS
        #endregion
    }
}
