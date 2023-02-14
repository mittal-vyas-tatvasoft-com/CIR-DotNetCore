using CIR.Common.Data;
using CIR.Common.Enums;
using CIR.Common.Helper;
using CIR.Core.Entities.GlobalConfiguration;
using CIR.Core.Interfaces.Common;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace CIR.Data.Data.Common
{
    public class CommonRepository : ICommonRepository
    {
        #region PROPERTIES

        private readonly CIRDbContext cirDbContext;

        #endregion

        #region CONSTRUCTORS

        public CommonRepository(CIRDbContext cirDbContext)
        {
            this.cirDbContext = cirDbContext ??
                throw new ArgumentNullException(nameof(cirDbContext));
        }

        #endregion

        #region METHODS

        /// <summary>
        /// This method returns the list of Countries
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> GetCountries()
        {
            try
            {
                List<CountryCode> countries = new List<CountryCode>();
                using (DbConnection dbConnection = new DbConnection())
                {
                    using (var connection = dbConnection.Connection)
                    {
                        countries = (await Task.FromResult(connection.Query<CountryCode>("spGetCountries", null, commandType: CommandType.StoredProcedure))).ToList();
                    }
                }
                if (countries.Count == 0)
                {
                    return new JsonResult(new CustomResponse<List<CountryCode>>() { StatusCode = (int)HttpStatusCodesAndMessages.HttpStatus.NotFound, Result = false, Message = string.Format(SystemMessages.msgDataNotExists, "countries") });
                }
                return new JsonResult(new CustomResponse<List<CountryCode>>() { StatusCode = (int)HttpStatusCodesAndMessages.HttpStatus.Success, Result = true, Message = HttpStatusCodesAndMessages.HttpStatus.Success.GetDescriptionAttribute(), Data = countries });
            }
            catch
            {
                return new JsonResult(new CustomResponse<Exception>() { StatusCode = (int)HttpStatusCodesAndMessages.HttpStatus.InternalServerError, Result = false, Message = SystemMessages.msgSomethingWentWrong });
            }
        }

        #endregion
    }
}
