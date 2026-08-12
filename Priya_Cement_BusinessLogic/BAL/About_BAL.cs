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
    public class About_BAL : BasePageBAL
    {
        public About_BAL(IConfiguration configuration) : base(configuration)
        {
        }

        public AboutModel GetAboutUs_BAL(string pagename, int languageId, int geographyId)
        {
            var model = new AboutModel();
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

                model.Four_Decades_Of_Building_List = MapComponents(groupedData, 1);
                model.Infographic_List = MapComponents(groupedData, 2);
                model.What_We_Stand_For_List = MapComponents(groupedData, 3);
                model.A_Word_From_Our_Leadership_List = MapComponents(groupedData, 4);
                model.Values_That_Define_How_We_Work_List = MapComponents(groupedData, 5);
                model.Built_On_Trust_Proven_At_Scale_List = MapComponents(groupedData, 6);
                model.Legacy_Built_One_Year_At_A_Time_List = MapComponents(groupedData, 7);
                model.Ready_To_Build_With_Priya_Cement_List  = MapComponents(groupedData, 8);
            }

            return model;
        }






    }
}