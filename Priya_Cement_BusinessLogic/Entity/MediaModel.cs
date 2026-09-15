using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Priya_Cement_BusinessLogic.Entity
{
    public class MediaModel
    {
        public ContentViewModel? Content { get; set; }
        public List<ComponentGroup> Components { get; set; } = new();
        public List<ComponentGroup> Components2 { get; set; } = new();



        //NewsCoverage
        public List<ComponentModel> Print_List { get; set; } = new();
        public List<ComponentModel> Gallery_List { get; set; } = new();


         //Campaigns
        public List<ComponentModel> TVC_List { get; set; } = new();
        public List<ComponentModel> Corporate_Film_List { get; set; } = new();
       

        //Financial Information
        public List<ArticleModel> Section_List { get; set; } = new();
        public List<ArticleModel> SectionArticles_List { get; set; } = new();


       

        public int TotalCount { get; set; }

    }


}