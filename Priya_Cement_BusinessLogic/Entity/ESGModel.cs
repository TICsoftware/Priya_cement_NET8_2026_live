using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Priya_Cement_BusinessLogic.Entity
{
    public class ESGModel
    {
        public ContentViewModel? Content { get; set; }
        public List<ComponentGroup> Components { get; set; } = new();
        public List<ComponentGroup> Components2 { get; set; } = new();



        //Sustainability
        public List<ComponentModel> Strong_Cement_List { get; set; } = new();
        public List<ComponentModel> PC_Manufactures_Responsibly_List { get; set; } = new();
        public List<ComponentModel> Independently_Verified_List { get; set; } = new();
        public List<ComponentModel> Read_The_Numbers_List { get; set; } = new();


        //Safety
        public List<ComponentModel> Intro_List { get; set; } = new(); // 3
        public List<ComponentModel> Our_safety_culture_in_practice_List { get; set; } = new();
        public List<ComponentModel> Recognising_the_people_who_make_us_safer_List { get; set; } = new();
        public List<ComponentModel> Safety_infrastructure_and_systems_List { get; set; } = new();
        public List<ComponentModel> Competency_is_the_foundation_of_safe_operations_List { get; set; } = new();
        public List<ComponentModel> Safety_governance_and_participation_List { get; set; } = new();
        public List<ComponentModel> Committee_activities_List { get; set; } = new();
        public List<ComponentModel> Technology_in_the_service_of_safety_List { get; set; } = new();
        public List<ComponentModel> Independently_verified_Externally_recognised_List { get; set; } = new();
        public List<ComponentModel> Protecting_health_left_side_intro_List { get; set; } = new();
        public List<ComponentModel> Protecting_health_right_side_point_List { get; set; } = new();
        public List<ComponentModel> CTA_List { get; set; } = new();



        public int TotalCount { get; set; }

    }


}