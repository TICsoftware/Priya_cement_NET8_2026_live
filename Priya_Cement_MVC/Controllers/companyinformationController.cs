using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Priya_Cement_MVC.Models;

namespace Priya_Cement_MVC.Controllers;

public class CompanyinformationController : Controller
{
    private readonly ILogger<CompanyinformationController> _logger;

    public CompanyinformationController(ILogger<CompanyinformationController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }


}
