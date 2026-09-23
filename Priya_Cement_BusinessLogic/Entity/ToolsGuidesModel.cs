using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Priya_Cement_BusinessLogic.Entity
{
    public class ToolsGuidesModel
    {
        public ContentViewModel? Content { get; set; }
        public List<ComponentGroup> Components { get; set; } = new();
        public List<ComponentGroup> Components2 { get; set; } = new();


        //Tools and Guides
        public List<ComponentModel> Intro_Guides_List { get; set; } = new();
        public List<ComponentModel> Our_Guides_section_List { get; set; } = new();
        public List<ComponentModel> download_brochure_List { get; set; } = new();
        public List<ComponentModel> CTA_List { get; set; } = new();



    }


}