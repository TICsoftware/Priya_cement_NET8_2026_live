using Microsoft.Extensions.Configuration;
using Priya_Cement_BusinessLogic.Entity;

namespace Priya_Cement_BusinessLogic.BAL
{
    public class Contactus_BAL : BasePageBAL
    {
        public Contactus_BAL(IConfiguration configuration) : base(configuration)
        {
        }

        /// <summary>
        /// Contact Us landing page.
        /// Sequences: 1 intro, 2 toll-free numbers, 3 enquiry CTA.
        /// </summary>
        public ContactusModel GetContactus_BAL(string pagename, int languageId, int geographyId)
        {
            var model = new ContactusModel();
            var ds = GetContentComponentData_DAL(pagename, languageId, geographyId);

            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                model.Content = MapContent(ds.Tables[0].Rows[0]);
            }

            if (ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0)
            {
                var groupedData = GetGroupedComponents(ds.Tables[1]);
                model.Components = groupedData;

                model.Intro_List = MapComponents(groupedData, 1);
                model.TollFree_List = MapComponents(groupedData, 2);
                model.Enquiry_CTA_List = MapComponents(groupedData, 3);
            }

            return model;
        }
    }
}
