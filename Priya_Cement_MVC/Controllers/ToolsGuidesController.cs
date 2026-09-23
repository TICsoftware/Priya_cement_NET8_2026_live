using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Priya_Cement_MVC.Models;
using Priya_Cement_MVC.Classes;
using Core_project_BusinessLogic;
using Priya_Cement_BusinessLogic.BAL;
using Priya_Cement_BusinessLogic.Entity;
using System.Net;
using System.Net.Mail;
using Microsoft.AspNetCore.Mvc.Rendering;
using Priya_Cement_MVC;


namespace Priya_Cement_MVC.Controllers;

public class ToolsGuidesController : Controller
{
    private readonly ILogger<ToolsGuidesController> _logger;
    private readonly ToolsGuides_BAL _bal;

    public ToolsGuidesController(ILogger<ToolsGuidesController> logger, IConfiguration configuration)
    {
        _logger = logger;
        _bal = new ToolsGuides_BAL(configuration);
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Guides(string title)
    {
        try
        {
            var data = _bal.GetGuides_BAL(title, 1, 1);
            return View(data);
        }
        catch (Exception ex)
        {
            FileLogger.LogError("/Guides :", ex);
            return View(new ToolsGuidesModel());
        }
        finally
        {
            _bal.Dispose();
        }
    }


}
