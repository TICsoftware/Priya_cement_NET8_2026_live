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
                new SqlParameter("@Name",string.IsNullOrWhiteSpace(model.FullName) ? (object)DBNull.Value: model.FullName),
                new SqlParameter("@CompanyName", string.IsNullOrWhiteSpace(model.Organisation) ? (object)DBNull.Value : model.Organisation),
                new SqlParameter("@Designation", string.IsNullOrWhiteSpace(model.Designation) ? (object)DBNull.Value : model.Designation),
                new SqlParameter("@PhoneNumber", string.IsNullOrWhiteSpace(model.Phone) ? (object)DBNull.Value : model.Phone),
                new SqlParameter("@EmailAddress", string.IsNullOrWhiteSpace(model.Email) ? (object)DBNull.Value : model.Email),
                new SqlParameter("@StateId", model.StateId),
                new SqlParameter("@CityId", model.CityId),
                new SqlParameter("@Query", string.IsNullOrWhiteSpace(model.Query) ? (object)DBNull.Value : model.Query),
                new SqlParameter("@IPAddress", string.IsNullOrWhiteSpace(model.IPAddress) ? (object)DBNull.Value : model.IPAddress)
            };

            return GetDataSet("AddContactUs", sqlParams).Tables[0];
        }

        public List<CommonDropdownModel> GetCityList_DAL()
        {
            DataTable dt = GetDataSet("sp_GetCity").Tables[0];

            return dt.AsEnumerable()
                     .Select(x => new CommonDropdownModel
                     {
                         Id = Convert.ToInt32(x["Id"]),
                         Name = x["Name"]?.ToString()
                     }).ToList();
        }

        public List<CommonDropdownModel> GetStateList_DAL()
        {
            DataTable dt = GetDataSet("sp_GetState").Tables[0];

            return dt.AsEnumerable()
                     .Select(x => new CommonDropdownModel
                     {
                         Id = Convert.ToInt32(x["Id"]),
                         Name = x["Name"]?.ToString()
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
