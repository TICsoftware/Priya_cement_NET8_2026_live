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


        






    }
}