using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Priya_Cement_BusinessLogic.Entity;

namespace Priya_Cement_BusinessLogic.DAL
{
    public class ContactusEnquiry_DAL : DBHelper
    {
        public ContactusEnquiry_DAL(IConfiguration configuration) : base(configuration)
        {
        }

        public DataTable AddContactUsEnquiry_DAL(ContactUsEnquiry model)
        {
            SqlParameter[] sqlParams =
            {
                new SqlParameter("@FullName", model.FullName ?? (object)DBNull.Value),
                new SqlParameter("@Designation", string.IsNullOrWhiteSpace(model.Designation) ? (object)DBNull.Value : model.Designation),
                new SqlParameter("@Organisation", model.Organisation ?? (object)DBNull.Value),
                new SqlParameter("@Email", model.Email ?? (object)DBNull.Value),
                new SqlParameter("@Phone", model.Phone ?? (object)DBNull.Value),
                new SqlParameter("@CityId", model.CityId),
                new SqlParameter("@InterestId", model.InterestId),
                new SqlParameter("@Query", string.IsNullOrWhiteSpace(model.Query) ? (object)DBNull.Value : model.Query),
                new SqlParameter("@Consent", model.Consent),
                new SqlParameter("@IPAddress", string.IsNullOrWhiteSpace(model.IPAddress) ? (object)DBNull.Value : model.IPAddress)
            };

            return GetDataSet("sp_AddContactUsEnquiry", sqlParams).Tables[0];
        }

        public List<CommonDropdownModel> GetCityList_DAL()
        {
            DataTable dt = GetDataSet("GetCityMaster").Tables[0];

            return dt.AsEnumerable()
                     .Select(x => new CommonDropdownModel
                     {
                         Id = Convert.ToInt32(x["CityId"]),
                         Name = x["CityName"]?.ToString()
                     }).ToList();
        }

        public List<CommonDropdownModel> GetAreaOfInterestList_DAL()
        {
            DataTable dt = GetDataSet("GetAreaOfInterestMaster").Tables[0];

            return dt.AsEnumerable()
                     .Select(x => new CommonDropdownModel
                     {
                         Id = Convert.ToInt32(x["InterestId"]),
                         Name = x["InterestName"]?.ToString()
                     }).ToList();
        }
    }
}
