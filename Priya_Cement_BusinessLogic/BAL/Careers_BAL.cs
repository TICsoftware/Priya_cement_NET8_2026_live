using System.Data;
using Microsoft.Extensions.Configuration;
using Priya_Cement_BusinessLogic.Entity;

namespace Priya_Cement_BusinessLogic.BAL
{
    public class Careers_BAL : BasePageBAL
    {
        public Careers_BAL(IConfiguration configuration) : base(configuration)
        {
        }

        /// <summary>
        /// Careers landing page.
        /// Sequences: 1 intro, 2 life inside, 3 workplace culture, 4 testimonials, 5 CTA.
        /// </summary>
        public CareersModel GetCareers_BAL(string pagename, int languageId, int geographyId)
        {
            var model = new CareersModel();
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
                model.LifeInside_List = MapComponents(groupedData, 2);
                model.WorkplaceCulture_List = MapComponents(groupedData, 3);
                model.Testimonials_List = MapComponents(groupedData, 4);
                model.Careers_CTA_List = MapComponents(groupedData, 5);
            }

            return model;
        }
    }
}
