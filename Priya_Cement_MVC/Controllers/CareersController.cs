using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Priya_Cement_MVC.Models;
using Priya_Cement_BusinessLogic.BAL;
using Priya_Cement_BusinessLogic.Entity;
using Priya_Cement_MVC;

namespace Priya_Cement_MVC.Controllers;

public class CareersController : Controller
{
    private readonly ILogger<CareersController> _logger;
    private readonly Careers_BAL _bal;

    public CareersController(ILogger<CareersController> logger, IConfiguration configuration)
    {
        _logger = logger;
        _bal = new Careers_BAL(configuration);
    }

    public IActionResult Index(string title)
    {
        try
        {
            var pageName = string.IsNullOrWhiteSpace(title) ? "careers" : title;
            var data = _bal.GetCareers_BAL(pageName, 1, 1);
            return View(data);
        }
        catch (Exception ex)
        {
            FileLogger.LogError("/Careers/Index :", ex);
            return View(new CareersModel());
        }
        finally
        {
            _bal.Dispose();
        }
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
