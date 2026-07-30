using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Reflection;
using Priya_Cement_MVC.Models.Manage_Model;
 using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.Extensions.DependencyInjection;
namespace Priya_Cement_MVC.Controllers
{

    public class DynamicFormController : Controller
    {
        [HttpGet]
        public IActionResult Create()
        {
            return View(new DynamicFormViewModel());
        }

        [HttpPost]
        public IActionResult Create(DynamicFormViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            TempData["msg"] = "Saved successfully!";
            return RedirectToAction("Create");
        }

        public static PropertyInfo[] GetDynamicFields()
        {
            return typeof(DynamicFormViewModel)
                .GetProperties()
                .Where(p => p.GetCustomAttribute<DynamicFieldAttribute>() != null)
                .ToArray();
        }
    }


}