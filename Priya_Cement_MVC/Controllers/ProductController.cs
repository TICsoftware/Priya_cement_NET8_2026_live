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

public class ProductController : Controller
{
    private readonly ILogger<ProductController> _logger;
    private readonly Product_BAL _bal;

    public ProductController(ILogger<ProductController> logger, IConfiguration configuration)
    {
        _logger = logger;
        _bal = new Product_BAL(configuration);
    }



    public IActionResult Index(string title)
    {
        try
        {
            //string pageName = HttpContext?.Request?.Path.Value?.Trim('/') ?? string.Empty;
            var data = _bal.GetProduct_BAL(title, 1, 1);
            return View(data);
        }
        catch (Exception ex)
        {
            FileLogger.LogError("/About_Cop :", ex);
            return View(new AboutModel());
        }
        finally
        {
            _bal.Dispose();
        }
    }





}
