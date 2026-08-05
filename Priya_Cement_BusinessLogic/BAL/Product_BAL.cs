<<<<<<< HEAD
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Priya_Cement_BusinessLogic.DAL;
using Priya_Cement_BusinessLogic.Entity;
using Priya_Cement_BusinessLogic;
=======
using System.Data;
using Microsoft.Extensions.Configuration;
using Priya_Cement_BusinessLogic.Entity;
>>>>>>> 33b5aaf8c6d9076320abd1f08e7a6bfb4ad91929

namespace Priya_Cement_BusinessLogic.BAL
{
    public class Product_BAL : BasePageBAL
    {
        public Product_BAL(IConfiguration configuration) : base(configuration)
        {
        }

<<<<<<< HEAD
=======
        /// <summary>
        /// Product listing page (same component sequence pattern as About Us).
        /// </summary>
>>>>>>> 33b5aaf8c6d9076320abd1f08e7a6bfb4ad91929
        public ProductModel GetProduct_BAL(string pagename, int languageId, int geographyId)
        {
            var model = new ProductModel();
            var ds = GetContentComponentData_DAL(pagename, languageId, geographyId);

<<<<<<< HEAD
            // Content
=======
>>>>>>> 33b5aaf8c6d9076320abd1f08e7a6bfb4ad91929
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                model.Content = MapContent(ds.Tables[0].Rows[0]);
            }

<<<<<<< HEAD
            // Components
=======
>>>>>>> 33b5aaf8c6d9076320abd1f08e7a6bfb4ad91929
            if (ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0)
            {
                var groupedData = GetGroupedComponents(ds.Tables[1]);
                model.Components = groupedData;

<<<<<<< HEAD
                model.Intro_PL_List = MapComponents(groupedData, 1);
                model.Technical_team_PL_List = MapComponents(groupedData, 2);
            }

            if (ds?.Tables.Count > 2 && ds.Tables[2].Rows.Count > 0)
            {
                model.Product_List = Config_Application_Website.MapArticleList(ds.Tables[2]); ;
=======
                model.Component_With_Cards_List = MapComponents(groupedData, 1);
                model.infographics_List = MapComponents(groupedData, 2);
                model.Access_to_Safe_List = MapComponents(groupedData, 3);
                model.Who_Are_We_List = MapComponents(groupedData, 4);
                model.Our_Footprint_List = MapComponents(groupedData, 5);
                model.Singular_Spirit_Aboutus_List = MapComponents(groupedData, 6);
>>>>>>> 33b5aaf8c6d9076320abd1f08e7a6bfb4ad91929
            }

            return model;
        }

<<<<<<< HEAD









    }
}
=======
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
>>>>>>> 33b5aaf8c6d9076320abd1f08e7a6bfb4ad91929
