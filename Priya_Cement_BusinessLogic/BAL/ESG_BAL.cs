using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Priya_Cement_BusinessLogic.DAL;
using Priya_Cement_BusinessLogic.Entity;
using Priya_Cement_BusinessLogic;

namespace Priya_Cement_BusinessLogic.BAL
{
    public class ESG_BAL : BasePageBAL
    {
        public ESG_BAL(IConfiguration configuration) : base(configuration)
        {
        }

        public ESGModel GetSustainability_BAL(string pagename, int languageId, int geographyId)
        {
            var model = new ESGModel();
            var ds = GetContentComponentData_DAL(pagename, languageId, geographyId);

            // Content
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                model.Content = MapContent(ds.Tables[0].Rows[0]);
            }

            // Components
            if (ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0)
            {
                var groupedData = GetGroupedComponents(ds.Tables[1]);
                model.Components = groupedData;

                model.Strong_Cement_List = MapComponents(groupedData, 1);
                model.PC_Manufactures_Responsibly_List = MapComponents(groupedData, 2);
                model.Independently_Verified_List = MapComponents(groupedData, 3);
                model.Read_The_Numbers_List = MapComponents(groupedData, 4);
            }

            return model;
        }


        public ESGModel GetSafety_BAL(string pagename, int languageId, int geographyId)
        {
            var model = new ESGModel();
            var ds = GetContentComponentData_DAL(pagename, languageId, geographyId);

            // Content
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                model.Content = MapContent(ds.Tables[0].Rows[0]);
            }

            // Components
            if (ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0)
            {
                var groupedData = GetGroupedComponents(ds.Tables[1]);
                model.Components = groupedData;

                model.Intro_List = MapComponents(groupedData, 1);
                model.Our_safety_culture_in_practice_List = MapComponents(groupedData, 2);
                model.Recognising_the_people_who_make_us_safer_List = MapComponents(groupedData, 3);
                model.Safety_infrastructure_and_systems_List = MapComponents(groupedData, 4);
                model.Competency_is_the_foundation_of_safe_operations_List = MapComponents(groupedData, 5);
                model.Safety_governance_and_participation_List = MapComponents(groupedData, 6);
                model.Committee_activities_List = MapComponents(groupedData, 7);
                model.Technology_in_the_service_of_safety_List = MapComponents(groupedData, 8);
                model.Independently_verified_Externally_recognised_List = MapComponents(groupedData, 9);
                model.Protecting_health_left_side_intro_List = MapComponents(groupedData, 10);
                model.Protecting_health_right_side_point_List = MapComponents(groupedData, 11);
                model.CTA_List = MapComponents(groupedData, 12);
            }

            return model;
        }








    }
}