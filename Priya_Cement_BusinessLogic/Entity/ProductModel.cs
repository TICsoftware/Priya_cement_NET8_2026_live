using System.Collections.Generic;

namespace Priya_Cement_BusinessLogic.Entity
{
    public class ProductModel
    {
        public ContentViewModel? Content { get; set; }
        public List<ComponentGroup> Components { get; set; } = new();
        public List<ComponentGroup> Components2 { get; set; } = new();

        // Product listing (shared sequences with About-style pages)
        public List<ComponentModel> Intro_PL_List { get; set; } = new();
        public List<ComponentModel> Technical_team_PL_List { get; set; } = new();
        public List<ArticleModel> Product_List { get; set; } = new();

        

        // Product inside
        public List<ComponentModel> Product_Float_List { get; set; } = new();
        public List<ComponentModel> Intro_BestFor_List { get; set; } = new();
        public List<ComponentModel> Why_Experts_List { get; set; } = new();
        public List<ComponentModel> Physical_Properties_List { get; set; } = new();
        public List<ComponentModel> Compressive_Strength_List { get; set; } = new();
        public List<ComponentModel> Chemical_Properties_List { get; set; } = new();
        public List<ComponentModel> Things_To_Know_List { get; set; } = new();
        public List<ComponentModel> Product_CTA_List { get; set; } = new();

        // Technical services
        public List<ComponentModel> Intro_TS_List { get; set; } = new();
        public List<ComponentModel> Onsite_Testing_List { get; set; } = new();
        public List<ComponentModel> Testing_Capabilities_List { get; set; } = new();
        public List<ComponentModel> Support_Intro_List { get; set; } = new();
        public List<ComponentModel> Support_Items_List { get; set; } = new();
        public List<ComponentModel> Enquiry_CTA_List { get; set; } = new();

        public int TotalCount { get; set; }
    }
}
