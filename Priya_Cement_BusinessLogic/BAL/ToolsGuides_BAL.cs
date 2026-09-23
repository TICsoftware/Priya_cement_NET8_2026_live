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
    public class ToolsGuides_BAL : BasePageBAL
    {
        public ToolsGuides_BAL(IConfiguration configuration) : base(configuration)
        {
        }

        public ToolsGuidesModel GetGuides_BAL(string pagename, int languageId, int geographyId)
        {
            var model = new ToolsGuidesModel();
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

                model.Intro_Guides_List = MapComponents(groupedData, 1);
                model.Our_Guides_section_List = MapComponents(groupedData, 2);
                model.download_brochure_List = MapComponents(groupedData, 3);
                model.CTA_List = MapComponents(groupedData, 4);
               
            }

            return model;
        }



    }
}