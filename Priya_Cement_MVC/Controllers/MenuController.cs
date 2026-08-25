using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Priya_Cement_MVC.Models;
using Priya_Cement_BusinessLogic.BAL;
using Priya_Cement_BusinessLogic.Entity;
using Priya_Cement_MVC;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;

namespace Priya_Cement_MVC.Controllers;

public class MenuController : Controller
{
    private readonly ILogger<MenuController> _logger;
    private readonly Menu_BAL _bal;
    private readonly IConfiguration objconfig;

    public MenuController(ILogger<MenuController> logger, IConfiguration configuration)
    {
        _logger = logger;
        _bal = new Menu_BAL(configuration);
        objconfig = configuration;
    }



    public IActionResult GetFrontendHeaderMenus()
    {
        try
        {
            var headerMenus = _bal.GetFrontendHeaderMenus();
            return View(headerMenus);
        }
        catch (Exception ex)
        {
            FileLogger.LogError("/Header/GetFrontendHeaderMenus :", ex);

            return View(new List<MenuHeader>());
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
