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

public class ESGController : Controller
{
    private readonly ILogger<ESGController> _logger;
    private readonly ESG_BAL _bal;

    public ESGController(ILogger<ESGController> logger, IConfiguration configuration)
    {
        _logger = logger;
        _bal = new ESG_BAL(configuration);
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Sustainability(string title)
    {
        try
        {
            var data = _bal.GetSustainability_BAL(title, 1, 1);
            return View(data);
        }
        catch (Exception ex)
        {
            FileLogger.LogError("/Sustainability :", ex);
            return View(new AboutModel());
        }
        finally
        {
            _bal.Dispose();
        }
    }








}
