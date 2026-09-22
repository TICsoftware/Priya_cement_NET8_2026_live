using System.Security.Cryptography.X509Certificates;
using Core_project_BusinessLogic;
using Core_project_BusinessLogic.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Priya_Cement_MVC.Filters;

namespace Priya_Cement_MVC.Controllers.Manage
{
    [Authorize]
    [SessionAuthorize]
    public class TagMasterController : Controller
    {
        private readonly IConfiguration objconfig;
        public TagMasterController(IConfiguration configuration)
        {
            objconfig = configuration;
        }

        public IActionResult Index(List_Model_Tags Model, string Command)
        {
            using TagMaster_BAL _bal = new TagMaster_BAL(objconfig);
            List<Options_List> list_languages = [];
            List<TagMaster> objtags = new();
            Model.List_tags = [];
            Model.Languages = [];
            try
            {
                if (Command == null)
                {
                    Model.Status = 1;
                    Model.language_id = 1;
                    objtags = _bal.GetAllTags_BAL(Model.Status, Model.language_id, out list_languages);
                }
                else
                {
                    objtags = _bal.GetAllTags_BAL(Model.Status, Model.language_id, out list_languages);
                }
                if (list_languages != null && list_languages.Count > 0)
                {
                    foreach (var item in list_languages)
                    {
                        Model.Languages.Add(new SelectListItem { Text = item.title, Value = item.id.ToString() });
                    }
                }
                if (objtags != null && objtags.Count > 0)
                {
                    foreach (var item in objtags)
                    {
                        Model.List_tags.Add(new Model_Tag_Master
                        {
                            ID = item.ID,
                            Language_Master_ID = item.Language_Master_ID,
                            Tag_Name = item.Tag_name,
                            Language_Name = item.Languauge_Name,
                            Created_Date = item.Created_Date?.ToString("dd-MMM-yyyy"),
                            Updated_Date = item.Updated_Date?.ToString("dd-MMM-yyyy")
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                FileLogger.LogError("Post: ", ex);
                ModelState.AddModelError("", "Something went wrong. Please try again");
            }
            finally
            {
                _bal.Dispose();
            }
            return View(Model);
        }
    }
}