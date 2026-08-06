using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Priya_Cement_MVC.Models;
using Priya_Cement_BusinessLogic.BAL;
using Priya_Cement_BusinessLogic.Entity;
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
            FileLogger.LogError("/Product/Index :", ex);
            return View(new ProductModel());
        }
        finally
        {
            _bal.Dispose();
        }
    }

    public IActionResult Inside(string title)
    {
        try
        {
            var data = _bal.GetProductInside_BAL(title, 1, 1);
            return View(data);
        }
        catch (Exception ex)
        {
            FileLogger.LogError("/Product/Inside :", ex);
            return View(new ProductModel());
        }
        finally
        {
            _bal.Dispose();
        }
    }
    
    public IActionResult TechnicalServices(string title)
    {
        try
        {
            var pageName = string.IsNullOrWhiteSpace(title) ? "technical-services" : title;
            var data = _bal.GetTechnicalServices_BAL(pageName, 1, 1);
            return View(data);
        }
        catch (Exception ex)
        {
            FileLogger.LogError("/Product/TechnicalServices :", ex);
            return View(new ProductModel());
        }
        finally
        {
            _bal.Dispose();
        }
    }

    [HttpPost]
    public ActionResult SaveTechnicalSupport(TechnicalSupportEnquiry model)
    {
        if (!ModelState.IsValid)
        {
            return Json(new
            {
                Status = false,
                Message = "Please fill all required fields."
            });
        }

        // Save to database

        return Json(new
        {
            Status = true,
            Message = "Enquiry submitted successfully."
        });
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
