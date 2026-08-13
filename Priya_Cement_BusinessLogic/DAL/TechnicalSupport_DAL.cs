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
                new SqlParameter("@IPAddress", string.IsNullOrWhiteSpace(model.IPAddress) ? (object)DBNull.Value : model.IPAddress),
                new SqlParameter("@Consent", (object?)model.Consent ?? DBNull.Value)
            };

            return GetDataSet("AddTechnicalSupportEnquiry", sqlParams).Tables[0];
        }


        public List<CommonDropdownModel> GetServiceTypeList_DAL()
        {
            return GetDropdownList("sp_GetServiceType");
        }

        public List<CommonDropdownModel> GetStateList_DAL()
        {
            return GetDropdownList("sp_GetState");
        }

        public List<CommonDropdownModel> GetCityList_DAL()
        {
            return GetDropdownList("sp_GetCity");
        }

        public List<CommonDropdownModel> GetTestTypeList_DAL()
        {
            return GetDropdownList("sp_GetTestType");
        }


        public DataTable GetCityByState_DAL(int stateId)
        {
            SqlParameter[] param =
            {
                new SqlParameter("@StateId", stateId)
            };

            return GetDataSet("sp_GetCityByState", param).Tables[0];
        }

        public DataTable GetTestTypeByService_DAL(int serviceTypeId)
        {
            SqlParameter[] param =
            {
                new SqlParameter("@ServiceRequestId", serviceTypeId)
            };

            return GetDataSet("sp_GetTestTypeByService", param).Tables[0];
        }


        private List<CommonDropdownModel> GetDropdownList(string procedureName)
        {
            DataTable dt = GetDataSet(procedureName).Tables[0];

            return dt.AsEnumerable()
                     .Select(x => new CommonDropdownModel
                     {
                         Id = Convert.ToInt32(x["Id"]),
                         Name = x["Name"].ToString()
                     }).ToList();
        }


    }
}