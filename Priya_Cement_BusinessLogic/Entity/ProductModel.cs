using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Priya_Cement_BusinessLogic.Entity
{
    public class ProductModel
    {
        public ContentViewModel? Content { get; set; }
        public List<ComponentGroup> Components { get; set; } = new();
        public List<ComponentGroup> Components2 { get; set; } = new();



        //About us
        public List<ComponentModel> Intro_PL_List { get; set; } = new();
        public List<ComponentModel> Technical_team_PL_List { get; set; } = new();
         public List<ArticleModel> Product_List { get; set; } = new();


        public int TotalCount { get; set; }

    }


}