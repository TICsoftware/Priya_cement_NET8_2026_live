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
    public class Product_BAL : BasePageBAL
    {
        public Product_BAL(IConfiguration configuration) : base(configuration)
        {
        }

        public ProductModel GetProduct_BAL(string pagename, int languageId, int geographyId)
        {
            var model = new ProductModel();
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

                model.Intro_PL_List = MapComponents(groupedData, 1);
                model.Technical_team_PL_List = MapComponents(groupedData, 2);
            }

            if (ds?.Tables.Count > 2 && ds.Tables[2].Rows.Count > 0)
            {
                model.Product_List = Config_Application_Website.MapArticleList(ds.Tables[2]); ;
            }

            return model;
        }










    }
}