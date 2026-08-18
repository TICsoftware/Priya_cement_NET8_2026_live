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
    private readonly SolutionsEnquiry_BAL _SolutionsEnquiry_bal;
    private readonly IConfiguration objconfig;

    public ProductController(ILogger<ProductController> logger, IConfiguration configuration)
    {
        _logger = logger;
        _bal = new Product_BAL(configuration);
        _TechnicalSupport_bal = new TechnicalSupport_BAL(configuration);
        _SolutionsEnquiry_bal = new SolutionsEnquiry_BAL(configuration);
        objconfig = configuration;
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


    public IActionResult SolutionsCenter(string title)
    {
        try
        {
            var data = _bal.GetSolutionsCenter_BAL(title, 1, 1);
            ViewBag.captchapublickey = objconfig["CaptchaKeys:PublicKey"];
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
            ViewBag.captchapublickey = objconfig["CaptchaKeys:PublicKey"];
            BindTechnicalSupportDropdowns();
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
    [ValidateAntiForgeryToken]
    public IActionResult SubmitSolutionsEnquiry(SolutionCenterEnquiryModal model)
    {
        if (!ModelState.IsValid)
        {
            return Json(new
            {
                status = false,
                message = "Please correct the highlighted fields and try again.",
                errors = ModelState
                    .Where(x => x.Value != null && x.Value.Errors.Count > 0)
                    .ToDictionary(
                        x => x.Key,
                        x => x.Value!.Errors.Select(e => e.ErrorMessage).FirstOrDefault() ?? "This field is required.")
            });
        }

        try
        {
            var entity = new SolutionsEnquiry
            {
                FullName = model.FullName,
                MobileNumber = model.PhoneNumber,
                WhatsappNumber = model.WhatsAppNumber,
                EmailId = model.EmailAddress,
                Gender = model.Gender,
                AgeGroup = model.AgeGroup,
                District = model.District,
                TownVillage = model.TownVillage,
                CurrentOccupation = model.CurrentOccupation,
                CurrentOccupationOthers = string.Equals(model.CurrentOccupation, "Other", StringComparison.OrdinalIgnoreCase)
                    ? model.CurrentOccupationOthers
                    : null,
                OwnShopCommercialSpace = model.OwnShopOrCommercialSpace,
                PreviouslyRunBusiness = model.PreviouslyRunBusiness,
                SpaceForStoreSetup = model.HaveSpaceForStoreSetup,
                StoreSizeSqft = string.Equals(model.HaveSpaceForStoreSetup, "Yes", StringComparison.OrdinalIgnoreCase)
                    ? model.StoreSizeSqFt
                    : null,
                PreferredTimeForContact = model.PreferredTimeForContact,
                Consent = model.Consent,
                OtherDistrict = model.OtherDistrict,
            };

            DataTable dt = _SolutionsEnquiry_bal.SubmitEnquiry_BAL(entity);
            string result = dt.Rows.Count > 0 ? dt.Rows[0][0]?.ToString() ?? string.Empty : string.Empty;

            if (int.TryParse(result, out int newId) && newId > 0)
            {
                return Json(new
                {
                    status = true,
                    message = "Thank you for your enquiry. Our team will contact you shortly."
                });
            }

            return Json(new
            {
                status = false,
                message = string.IsNullOrWhiteSpace(result)
                    ? "Something went wrong while submitting your enquiry."
                    : result
            });
        }
        catch (Exception ex)
        {
            FileLogger.LogError("SubmitSolutionsEnquiry", ex);
            return Json(new
            {
                status = false,
                message = "Something went wrong while submitting your enquiry."
            });
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
                State = model.State,
                City = model.City,
                TestTypeId = Convert.ToInt32(model.TestTypeId),
                Consent = Convert.ToBoolean(model.Consent),
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


    private void BindTechnicalSupportDropdowns()
    {
        ViewBag.ServiceTypeList = new SelectList(_TechnicalSupport_bal.GetServiceTypeList(), "Id", "Name");

        ViewBag.StateList = new SelectList(_TechnicalSupport_bal.GetStateList(), "Id", "Name");

        ViewBag.CityList = new SelectList(_TechnicalSupport_bal.GetCityList(), "Id", "Name");

        ViewBag.TestTypeList = new SelectList(_TechnicalSupport_bal.GetTestTypeList(), "Id", "Name");
    }


    [HttpGet]
    public JsonResult GetCities(int stateId)
    {
        DataTable dt = _TechnicalSupport_bal.GetCityByState(stateId);

        var data = dt.AsEnumerable()
            .Select(x => new
            {
                CityId = Convert.ToInt32(x["CityId"]),
                CityName = x["CityName"].ToString()
            });

        return Json(data);
    }

    [HttpGet]
    public JsonResult GetTestTypes(int serviceTypeId)
    {
        DataTable dt = _TechnicalSupport_bal.GetTestTypeByService(serviceTypeId);

        var data = dt.AsEnumerable()
            .Select(x => new
            {
                TestTypeId = Convert.ToInt32(x["TestTypeId"]),
                TestTypeName = x["TestTypeName"].ToString()
            });

        return Json(data);
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
