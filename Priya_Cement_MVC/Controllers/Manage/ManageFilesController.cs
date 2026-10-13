using Core_project_BusinessLogic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Priya_Cement_MVC.Classes;

namespace Priya_Cement_MVC.Controllers.Manage
{
    public class ManageFilesController : Controller
    {
        private readonly ILogger<ManageController> _logger;
        private readonly IConfiguration objconfig;

        public ManageFilesController(ILogger<ManageController> logger, IConfiguration configuration)
        {
            _logger = logger;
            objconfig = configuration;
        }


        [HttpGet]
        public IActionResult Upload(UploadFiles Model)
        {
            Model = new()
            {
                Language_master_id = 1,
                Section_id = 0,
                Subsection_id = 0
            };
            try
            {
                pageload(Model, 1);
            }
            catch (Exception ex)
            {
                FileLogger.LogError("Get", ex);
            }
            return View(Model);
        }



        [HttpPost]
        public IActionResult Upload(UploadFiles Model, IFormCollection Form)
        {
            try
            {
                
            }
            catch (Exception ex)
            {
                FileLogger.LogError("Get", ex);
            }
            return View(Model);
        }

        private void pageload(UploadFiles Model, int status)
        {
            using ContentManager objBal = new(objconfig);
            try
            {
                CMS_pageload cmsbal = new();
                if (Model.Content_Type_ID == Core_project_BusinessLogic.Content_Types.Section)
                {
                    cmsbal = objBal.CMS_Pageload_Get_BAL(Model.Language_master_id, 0, status);
                }
                else
                {
                    cmsbal = objBal.CMS_Pageload_Get_BAL(Model.Language_master_id, 0, status);
                }

                Model.Languages = [];
                if (cmsbal.Languages != null && cmsbal.Languages.Count > 0)
                {
                    foreach (var item in cmsbal.Languages)
                    {
                        Model.Languages.Add(new SelectListItem { Text = item.title, Value = item.id.ToString() });
                    }
                }

                Model.Geographies = [];
                if (cmsbal.geographies != null && cmsbal.geographies.Count > 0)
                {
                    foreach (var item in cmsbal.geographies)
                    {
                        Model.Geographies.Add(new SelectListItem { Text = item.title, Value = item.id.ToString() });
                    }
                }
                Model.Templates = [];
                if (cmsbal.Templates != null && cmsbal.Templates.Count > 0)
                {
                    foreach (var item in cmsbal.Templates)
                    {
                        Model.Templates.Add(new SelectListItem { Text = item.title, Value = item.id.ToString() });
                    }

                }


                Model.Sections = [];
                if (cmsbal.Sections != null && cmsbal.Sections.Count > 0)
                {
                    foreach (var item in cmsbal.Sections)
                    {
                        Model.Sections.Add(new SelectListItem { Text = item.title, Value = item.id.ToString() });
                    }
                }
                Model.subSections = [];
                if (cmsbal.Subsections != null && cmsbal.Subsections.Count > 0)
                {
                    Model.subSections = Helper.BuildHierarchy(cmsbal.Subsections, Model.Section_id);
                }


                Model.Language_sections = [];
                if (cmsbal.Language_sections != null && cmsbal.Language_sections.Count > 0)
                {
                    foreach (var item in cmsbal.Language_sections)
                    {
                        Model.Language_sections.Add(new SelectListItem { Text = item.title, Value = item.id.ToString() });
                    }
                }

                Model.Articles = [];
                if (cmsbal.sect_Articles != null && cmsbal.sect_Articles.Count > 0)
                {
                    foreach (var item in cmsbal.sect_Articles)
                    {
                        Model.Articles.Add(new SelectListItem { Text = item.title, Value = item.id.ToString() });
                    }
                }

                Model.Language_subSections = [new SelectListItem { Value = "0", Text = "Select" }];
                if (Model.Language_master_id > 1 && Model.Language_section_id > 0)
                {
                    cmsbal.Language_subSections = new();
                    cmsbal.Language_subSections = objBal.Language_Subsections_Get_BAL(Model.Language_section_id, Model.Language_master_id);

                    if (cmsbal.Language_subSections != null && cmsbal.Language_subSections.Count > 0)
                    {
                        Model.Language_subSections = Helper.BuildHierarchy(cmsbal.Language_subSections, Model.Language_section_id);
                    }
                }

                Model.Mapping_sections = [];
                if (cmsbal.Mapping_Sections != null && cmsbal.Mapping_Sections.Count > 0)
                {
                    foreach (var item in cmsbal.Mapping_Sections)
                    {
                        Model.Mapping_sections.Add(new SelectListItem
                        {
                            Text = item.title,
                            Value = item.id.ToString(),
                            Selected = Model.SelectedMapping_sections != null && Model.SelectedMapping_sections.Contains(item.id ?? 0)
                        });
                    }
                }


                Model.Tagging_sections = [];
                if (cmsbal.Tagging_Sections != null && cmsbal.Tagging_Sections.Count > 0)
                {
                    foreach (var item in cmsbal.Tagging_Sections)
                    {
                        Model.Tagging_sections.Add(new SelectListItem
                        {
                            Text = item.title,
                            Value = item.id.ToString(),
                            Selected = Model.SelectedTagging_sections != null && Model.SelectedTagging_sections.Contains(item.id ?? 0)
                        });
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                objBal.Dispose();
            }

        }

    }

}