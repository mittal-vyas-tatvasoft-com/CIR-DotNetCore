using CIR.Common.Data;
using CIR.Core.Interfaces.GlobalConfiguration;

namespace CIR.Data.Data.GlobalConfiguration
{
	public class GlobalConfigurationFieldsRepository : IGlobalConfigurationFieldsRepository
	{
		#region PROPERTIES
		private readonly CIRDbContext _cIRDbContext;
		#endregion

		#region CONSTRUCTOR
		public GlobalConfigurationFieldsRepository(CIRDbContext cIRDbContext)
		{
			_cIRDbContext = cIRDbContext;
		}
		#endregion

		#region METHODS
		#endregion
	}
}
