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

public class MediaController : Controller
{
    private readonly ILogger<MediaController> _logger;
    private readonly Media_BAL _bal;

    public MediaController(ILogger<MediaController> logger, IConfiguration configuration)
    {
        _logger = logger;
        _bal = new Media_BAL(configuration);
    }

    public IActionResult Index()
    {
        return View();
    }


    public IActionResult PressReleases(string title)
    {
        try
        {
            var data = _bal.GetPressReleases_BAL(title, 1, 1);
            return View(data);
        }
        catch (Exception ex)
        {
            FileLogger.LogError("/PressReleases :", ex);
            return View(new AboutModel());
        }
        finally
        {
            _bal.Dispose();
        }
    }


    public IActionResult NewsCoverage(string title)
    {
        try
        {
            var data = _bal.GetNewsCoverage_BAL(title, 1, 1);
            return View(data);
        }
        catch (Exception ex)
        {
            FileLogger.LogError("/NewsCoverage :", ex);
            return View(new AboutModel());
        }
        finally
        {
            _bal.Dispose();
        }
    }

    public IActionResult Campaigns(string title)
    {
        try
        {
            var data = _bal.GetCampaigns_BAL(title, 1, 1);
            return View(data);
        }
        catch (Exception ex)
        {
            FileLogger.LogError("/Campaigns :", ex);
            return View(new AboutModel());
        }
        finally
        {
            _bal.Dispose();
        }
    }





    public ActionResult LoadMorePressReleases(int cont_id, int pageSize, int pageNumber)
    {
        try
        {
            var model = _bal.GetPressReleases_page_wise_BAL(cont_id, pageSize, pageNumber);
            return PartialView("_press_release_list", model.SectionArticles_List);
        }
        catch (Exception ex)
        {
            FileLogger.LogError("/LoadMorePressReleases :", ex);
            return View(new AboutModel());
        }
        finally
        {
            _bal.Dispose();
        }
    }







}
