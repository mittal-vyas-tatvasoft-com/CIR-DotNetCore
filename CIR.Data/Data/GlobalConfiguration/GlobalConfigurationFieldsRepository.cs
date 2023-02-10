using CIR.Common.Data;
using CIR.Core.Interfaces.GlobalConfiguration;

namespace CIR.Data.Data.GlobalConfiguration
{
	public class GlobalConfigurationFieldsRepository : IGlobalConfigurationFieldsRepository
	{
		#region PROPERTIES
		private readonly CIRDbContext cIRDbContext;
		#endregion

		#region CONSTRUCTOR
		public GlobalConfigurationFieldsRepository(CIRDbContext context)
		{
			cIRDbContext = context ??
			   throw new ArgumentNullException(nameof(context));
		}

		#endregion

		#region METHODS
		#endregion
	}
}
