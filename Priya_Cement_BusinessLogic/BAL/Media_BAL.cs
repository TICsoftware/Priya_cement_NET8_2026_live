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
    public class Media_BAL : BasePageBAL
    {
        public Media_BAL(IConfiguration configuration) : base(configuration)
        {
        }

        public MediaModel GetNewsCoverage_BAL(string pagename, int languageId, int geographyId)
        {
            var model = new MediaModel();
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

                model.Print_List = MapComponents(groupedData, 1);
                model.Gallery_List = MapComponents(groupedData, 2);
            }

            return model;
        }

        public MediaModel GetCampaigns_BAL(string pagename, int languageId, int geographyId)
        {
            var model = new MediaModel();
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

                model.TVC_List = MapComponents(groupedData, 1);
                model.Corporate_Film_List = MapComponents(groupedData, 2);

            }

            return model;
        }






        public MediaModel GetPressReleases_BAL(string pagename, int languageId, int geographyId)
        {
            var model = new MediaModel();

            var ds = GetContentComponentData_DAL(pagename, languageId, geographyId);

            if (ds == null || ds.Tables.Count == 0)
            {
                return model;
            }

            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                model.Content = MapContent(ds.Tables[0].Rows[0]);
            }

            if (ds.Tables.Count > 2 && ds.Tables[2].Rows.Count > 0)
            {
                model.SectionArticles_List = Config_Application_Website.MapArticleList(ds.Tables[2]);
            }

            if (ds.Tables.Count > 3 && ds.Tables[3].Rows.Count > 0)
            {
                model.TotalCount = Convert.ToInt32(ds.Tables[3].Rows[0]["TotalCount"]);
            }

            return model;
        }

        public MediaModel GetPressReleases_page_wise_BAL(int cont_id, int page, int pageSize)
        {
            var model = new MediaModel();

            var ds = Get_PressRelease_page_wise_DAL(cont_id, page, pageSize);

            if (ds == null || ds.Tables.Count == 0)
            {
                return model;
            }


            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                model.SectionArticles_List = Config_Application_Website.MapArticleList(ds.Tables[0]);
            }

            if (ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0)
            {
                model.TotalCount = Convert.ToInt32(ds.Tables[1].Rows[0]["TotalCount"]);
            }

            return model;
        }




    }
}