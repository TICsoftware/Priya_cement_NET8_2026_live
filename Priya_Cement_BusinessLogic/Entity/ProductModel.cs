using System.Collections.Generic;

namespace Priya_Cement_BusinessLogic.Entity
{
    public class ProductModel
    {
        public ContentViewModel? Content { get; set; }
        public List<ComponentGroup> Components { get; set; } = new();
        public List<ComponentGroup> Components2 { get; set; } = new();

        // Product listing (shared sequences with About-style pages)
        public List<ComponentModel> Component_With_Cards_List { get; set; } = new();
        public List<ComponentModel> infographics_List { get; set; } = new();
        public List<ComponentModel> Access_to_Safe_List { get; set; } = new();
        public List<ComponentModel> Who_Are_We_List { get; set; } = new();
        public List<ComponentModel> Our_Footprint_List { get; set; } = new();
        public List<ComponentModel> Singular_Spirit_Aboutus_List { get; set; } = new();

        // Product inside
        public List<ComponentModel> Product_Float_List { get; set; } = new();
        public List<ComponentModel> Intro_BestFor_List { get; set; } = new();
        public List<ComponentModel> Why_Experts_List { get; set; } = new();
        public List<ComponentModel> Physical_Properties_List { get; set; } = new();
        public List<ComponentModel> Compressive_Strength_List { get; set; } = new();
        public List<ComponentModel> Chemical_Properties_List { get; set; } = new();
        public List<ComponentModel> Things_To_Know_List { get; set; } = new();
        public List<ComponentModel> Product_CTA_List { get; set; } = new();

        public int TotalCount { get; set; }
    }
}
