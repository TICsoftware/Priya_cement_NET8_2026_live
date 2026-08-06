using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Priya_Cement_MVC.Models;
using Priya_Cement_BusinessLogic.BAL;
using Priya_Cement_BusinessLogic.Entity;
using Priya_Cement_MVC;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;

namespace Priya_Cement_MVC.Controllers;

public class ProductController : Controller
{
    private readonly ILogger<ProductController> _logger;
    private readonly Product_BAL _bal;
    private readonly TechnicalSupport_BAL _TechnicalSupport_bal;

    public ProductController(ILogger<ProductController> logger, IConfiguration configuration)
    {
        _logger = logger;
        _bal = new Product_BAL(configuration);
        _TechnicalSupport_bal = new TechnicalSupport_BAL(configuration);

    }

    public IActionResult Index(string title)
    {
        try
        {
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


    public IActionResult Test(string title)
    {
        try
        {
            var data = _bal.GetProductInside_BAL("what-we-offer", 1, 1);

            // Service Type
            ViewBag.ServiceTypeList = new List<SelectListItem>
            {
                new SelectListItem { Value = "1", Text = "On-site testing services" },
                new SelectListItem { Value = "2", Text = "Technical support services" }
            };

            // State List
            ViewBag.StateList = new List<SelectListItem>
            {
                new SelectListItem { Value = "1", Text = "Andhra Pradesh" },
                new SelectListItem { Value = "2", Text = "Telangana" },
                new SelectListItem { Value = "3", Text = "Tamil Nadu" },
                new SelectListItem { Value = "4", Text = "Karnataka" },
                new SelectListItem { Value = "5", Text = "Odisha" },
                new SelectListItem { Value = "6", Text = "Maharashtra" }
            };

            // City List
            ViewBag.CityList = new List<SelectListItem>
            {
                new SelectListItem { Value = "1", Text = "Hyderabad, Rangareddy" },
                new SelectListItem { Value = "2", Text = "Vijayawada, Krishna" },
                new SelectListItem { Value = "3", Text = "Visakhapatnam" },
                new SelectListItem { Value = "4", Text = "Chennai" },
                new SelectListItem { Value = "5", Text = "Bengaluru, Urban" },
                new SelectListItem { Value = "6", Text = "Other" }
            };

            // Test Type List
            ViewBag.TestTypeList = new List<SelectListItem>
            {
                new SelectListItem { Value = "1", Text = "Cement tests" },
                new SelectListItem { Value = "2", Text = "Aggregate tests" },
                new SelectListItem { Value = "3", Text = "Concrete tests" },
                new SelectListItem { Value = "4", Text = "Water quality testing" },
                new SelectListItem { Value = "5", Text = "Other tests" }
            };


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


    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult SubmitTechnicalSupport(TechnicalSupportEnquiryModal model)
    {
        if (!ModelState.IsValid)
        {
            return Json(new
            {
                status = false,
                message = "Please correct the highlighted fields and try again."
            });
        }

        try
        {
            var entity = new TechnicalSupportEnquiry
            {
                ServiceTypeId = model.ServiceTypeId,
                Name = model.Name,
                Designation = model.Designation,
                CompanyName = model.CompanyName,
                PhoneNumber = model.PhoneNumber,
                EmailAddress = model.EmailAddress,
                StateId = Convert.ToInt32(model.StateId),
                CityId = Convert.ToInt32(model.CityId),
                TestTypeId = Convert.ToInt32(model.TestTypeId),
                IPAddress = GetClientIpAddress()
            };


            DataTable dt = _TechnicalSupport_bal.SubmitEnquiry_BAL(entity);

            string result = dt.Rows[0][0].ToString();

            switch (result.ToLower())
            {
                case "updated":
                    return Json(new
                    {
                        status = true,
                        message = "Thank you for your enquiry. Our technical team will contact you shortly."
                    });

                case "exceeds":
                    return Json(new
                    {
                        status = false,
                        message = "You have exceeded the maximum number of enquiries allowed today."
                    });

                default:
                    return Json(new
                    {
                        status = false,
                        message = result
                    });
            }
        }
        catch (Exception ex)
        {
            FileLogger.LogError("SubmitTechnicalSupport", ex);

            return Json(new
            {
                status = false,
                message = "Something went wrong while submitting your enquiry."
            });
        }
    }


    private string GetClientIpAddress()
    {
        var forwardedFor = Request.Headers["X-Forwarded-For"].FirstOrDefault();

        if (!string.IsNullOrWhiteSpace(forwardedFor))
            return forwardedFor.Split(',')[0].Trim();

        return HttpContext.Connection.RemoteIpAddress?.ToString() ?? string.Empty;
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
