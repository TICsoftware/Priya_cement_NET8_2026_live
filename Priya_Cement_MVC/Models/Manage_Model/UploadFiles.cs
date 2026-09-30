using Microsoft.AspNetCore.Mvc.Rendering;

namespace Priya_Cement_MVC.Models.Manage_Model
{
    public class UploadFiles
    {
        public int Language_id { get; set; }
        public List<SelectListItem>? Languages { get; set; }
        public int Template_id { get; set; }
        public List<SelectListItem>? Templates { get; set; }
        public int Geography_ID { get; set; }
        public List<SelectListItem>? Geographies { get; set; }
        public int Section_id { get; set; }
        public List<SelectListItem>? Language_sections { get; set; }
        public int Language_Section_id { get; set; }
        public List<SelectListItem>? Language_subSections { get; set; }
        public List<SelectedFiles>? FilesList { get; set; }
    }

    public class SelectedFiles
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public DateTime? Filedate { get; set; }
        public string? Filepagename { get; set; }
        public int Sequence { get; set; }

    }
}