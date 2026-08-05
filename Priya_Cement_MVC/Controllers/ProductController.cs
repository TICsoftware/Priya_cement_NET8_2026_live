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
<<<<<<< HEAD
            //string pageName = HttpContext?.Request?.Path.Value?.Trim('/') ?? string.Empty;
=======
>>>>>>> 33b5aaf8c6d9076320abd1f08e7a6bfb4ad91929
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

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
