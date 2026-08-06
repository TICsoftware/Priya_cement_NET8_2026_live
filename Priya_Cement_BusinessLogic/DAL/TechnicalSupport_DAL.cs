using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Priya_Cement_BusinessLogic.Entity;

namespace Priya_Cement_BusinessLogic.DAL
{
    public class TechnicalSupport_DAL : DBHelper
    {
        public TechnicalSupport_DAL(IConfiguration configuration) : base(configuration)
        {
        }

        public DataTable AddTechnicalSupportEnquiry_DAL(TechnicalSupportEnquiry model)
        {
            SqlParameter[] sqlParams =
            {
                new SqlParameter("@ServiceTypeId", model.ServiceTypeId),
                new SqlParameter("@Name", model.Name),
                new SqlParameter("@Designation", string.IsNullOrWhiteSpace(model.Designation) ? (object)DBNull.Value : model.Designation),
                new SqlParameter("@CompanyName", model.CompanyName),
                new SqlParameter("@PhoneNumber", model.PhoneNumber),
                new SqlParameter("@EmailAddress", model.EmailAddress),
                new SqlParameter("@StateId", model.StateId),
                new SqlParameter("@CityId", model.CityId),
                new SqlParameter("@TestTypeId", model.TestTypeId == 0 ? (object)DBNull.Value : model.TestTypeId),
                new SqlParameter("@IPAddress", string.IsNullOrWhiteSpace(model.IPAddress) ? (object)DBNull.Value : model.IPAddress)
            };

            return GetDataSet("AddTechnicalSupportEnquiry", sqlParams).Tables[0];
        }
    }
}