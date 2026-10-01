using Microsoft.AspNetCore.Mvc;
using Priya_Cement_MVC.Models.Manage_Model;

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
            Model = new();
            return View(Model);
        }

        [HttpPost]
        public IActionResult Upload(UploadFiles Model, IFormCollection Form)
        {
            return View(Model);
        }

    }

}