using System.Data;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Priya_Cement_BusinessLogic.BAL;
using Priya_Cement_BusinessLogic.Entity;
using Priya_Cement_MVC.Models;
using Priya_Cement_MVC;

namespace Priya_Cement_MVC.Controllers;

public class ContactusController : Controller
{
    private readonly ILogger<ContactusController> _logger;
    private readonly Contactus_BAL _bal;
    private readonly ContactusEnquiry_BAL _enquiryBal;

    public ContactusController(ILogger<ContactusController> logger, IConfiguration configuration)
    {
        _logger = logger;
        _bal = new Contactus_BAL(configuration);
        _enquiryBal = new ContactusEnquiry_BAL(configuration);
    }

    public IActionResult Index(string title)
    {
        try
        {
            var pageName = string.IsNullOrWhiteSpace(title) ? "contact" : title;
            var data = _bal.GetContactus_BAL(pageName, 1, 1);
            BindEnquiryDropdowns();
            return View("~/Views/Contact-us/Index.cshtml", data);
        }
        catch (Exception ex)
        {
            FileLogger.LogError("/Contactus/Index :", ex);
            BindEnquiryDropdowns();
            return View("~/Views/Contact-us/Index.cshtml", new ContactusModel());
        }
        finally
        {
            _bal.Dispose();
            _enquiryBal.Dispose();
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult SubmitEnquiry(ContactUsEnquiryModel model)
    {
        if (!ModelState.IsValid)
        {
            _enquiryBal.Dispose();
            return Json(new
            {
                status = false,
                message = "Please correct the highlighted fields and try again."
            });
        }

        try
        {
            var entity = new ContactUsEnquiry
            {
                FullName = model.FullName,
                Designation = model.Designation,
                Organisation = model.Organisation,
                Email = model.Email,
                Phone = model.Phone,
                CityId = model.CityId,
                InterestId = model.InterestId,
                Query = model.Message,
                Consent = model.Consent,
                IPAddress = GetClientIpAddress()
            };

            DataTable dt = _enquiryBal.SubmitEnquiry_BAL(entity);
            string result = dt.Rows.Count > 0 ? dt.Rows[0][0]?.ToString() ?? string.Empty : string.Empty;

            switch (result.ToLowerInvariant())
            {
                case "updated":
                    return Json(new
                    {
                        status = true,
                        message = "Thank you for your enquiry. Our team will contact you shortly."
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
                        message = string.IsNullOrWhiteSpace(result)
                            ? "Something went wrong while submitting your enquiry."
                            : result
                    });
            }
        }
        catch (Exception ex)
        {
            FileLogger.LogError("SubmitEnquiry", ex);
            return Json(new
            {
                status = false,
                message = "Something went wrong while submitting your enquiry."
            });
        }
        finally
        {
            _enquiryBal.Dispose();
        }
    }

    private void BindEnquiryDropdowns()
    {
        ViewBag.CityList = new SelectList(_enquiryBal.GetCityList(), "Id", "Name");
        ViewBag.InterestList = new SelectList(_enquiryBal.GetAreaOfInterestList(), "Id", "Name");
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
