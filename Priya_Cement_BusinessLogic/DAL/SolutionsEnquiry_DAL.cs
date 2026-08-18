using System;
using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Priya_Cement_BusinessLogic.Entity;

namespace Priya_Cement_BusinessLogic.DAL
{
    public class SolutionsEnquiry_DAL : DBHelper
    {
        public SolutionsEnquiry_DAL(IConfiguration configuration) : base(configuration)
        {
        }

        public DataTable AddSolutionsEnquiry_DAL(SolutionsEnquiry model)
        {
            SqlParameter[] sqlParams =
            {
                new SqlParameter("@full_name", model.FullName ?? (object)DBNull.Value),
                new SqlParameter("@mobile_number", model.MobileNumber ?? (object)DBNull.Value),
                new SqlParameter("@whatsapp_number", string.IsNullOrWhiteSpace(model.WhatsappNumber) ? (object)DBNull.Value : model.WhatsappNumber),
                new SqlParameter("@email_id", string.IsNullOrWhiteSpace(model.EmailId) ? (object)DBNull.Value : model.EmailId),
                new SqlParameter("@gender", string.IsNullOrWhiteSpace(model.Gender) ? (object)DBNull.Value : model.Gender),
                new SqlParameter("@age_group", string.IsNullOrWhiteSpace(model.AgeGroup) ? (object)DBNull.Value : model.AgeGroup),
                new SqlParameter("@district", string.IsNullOrWhiteSpace(model.District) ? (object)DBNull.Value : model.District),
                new SqlParameter("@town_village", string.IsNullOrWhiteSpace(model.TownVillage) ? (object)DBNull.Value : model.TownVillage),
                new SqlParameter("@current_occupation", string.IsNullOrWhiteSpace(model.CurrentOccupation) ? (object)DBNull.Value : model.CurrentOccupation),
                new SqlParameter("@current_occupation_others", string.IsNullOrWhiteSpace(model.CurrentOccupationOthers) ? (object)DBNull.Value : model.CurrentOccupationOthers),
                new SqlParameter("@own_shop_commercial_space", string.IsNullOrWhiteSpace(model.OwnShopCommercialSpace) ? (object)DBNull.Value : model.OwnShopCommercialSpace),
                new SqlParameter("@previously_run_business", string.IsNullOrWhiteSpace(model.PreviouslyRunBusiness) ? (object)DBNull.Value : model.PreviouslyRunBusiness),
                new SqlParameter("@space_for_store_setup", string.IsNullOrWhiteSpace(model.SpaceForStoreSetup) ? (object)DBNull.Value : model.SpaceForStoreSetup),
                new SqlParameter("@store_size_sqft", string.IsNullOrWhiteSpace(model.StoreSizeSqft) ? (object)DBNull.Value : model.StoreSizeSqft),
                new SqlParameter("@preferred_time_for_contact", string.IsNullOrWhiteSpace(model.PreferredTimeForContact) ? (object)DBNull.Value : model.PreferredTimeForContact),
                new SqlParameter("@Consent", (object?)model.Consent ?? DBNull.Value),
                new SqlParameter("@OtherDistrict", model.OtherDistrict ?? (object)DBNull.Value),
            };

            return GetDataSet("Solutions_Enquiry_Submission_Insert", sqlParams).Tables[0];
        }
    }
}
