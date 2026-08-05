using System.Data;
using Microsoft.Extensions.Configuration;
using Priya_Cement_BusinessLogic.Entity;

namespace Priya_Cement_BusinessLogic.BAL
{
    public class Product_BAL : BasePageBAL
    {
        public Product_BAL(IConfiguration configuration) : base(configuration)
        {
        }

        /// <summary>
        /// Product listing page (same component sequence pattern as About Us).
        /// </summary>
        public ProductModel GetProduct_BAL(string pagename, int languageId, int geographyId)
        {
            var model = new ProductModel();
            var ds = GetContentComponentData_DAL(pagename, languageId, geographyId);

            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                model.Content = MapContent(ds.Tables[0].Rows[0]);
            }

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

        /// <summary>
        /// Product inside / detail page.
        /// Sequences: 1 float+downloads, 2 intro+best for, 3 why experts,
        /// 4 physical, 5 compressive, 6 chemical, 7 things to know, 8 CTA.
        /// </summary>
        public ProductModel GetProductInside_BAL(string pagename, int languageId, int geographyId)
        {
            var model = new ProductModel();
            var ds = GetContentComponentData_DAL(pagename, languageId, geographyId);

            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                model.Content = MapContent(ds.Tables[0].Rows[0]);
            }

            if (ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0)
            {
                var groupedData = GetGroupedComponents(ds.Tables[1]);
                model.Components = groupedData;

                model.Product_Float_List = MapComponents(groupedData, 1);
                model.Intro_BestFor_List = MapComponents(groupedData, 2);
                model.Why_Experts_List = MapComponents(groupedData, 3);
                model.Physical_Properties_List = MapComponents(groupedData, 4);
                model.Compressive_Strength_List = MapComponents(groupedData, 5);
                model.Chemical_Properties_List = MapComponents(groupedData, 6);
                model.Things_To_Know_List = MapComponents(groupedData, 7);
                model.Product_CTA_List = MapComponents(groupedData, 8);
            }

            return model;
        }
    }
}
