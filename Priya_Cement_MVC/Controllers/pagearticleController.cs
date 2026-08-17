using Microsoft.AspNetCore.Mvc;
using Priya_Cement_BusinessLogic.BAL;
using Priya_Cement_BusinessLogic.Entity;
namespace Priya_Cement_MVC.Controllers
{
    public class pagearticleController : Controller
    {
        private readonly IConfiguration objconfig;
        public pagearticleController(IConfiguration configuration)
        {
            // HttpContext.Session.SetString("userid", "1");
            objconfig = configuration;
        }

        public IActionResult Index()
        {
            try
            {
                Page_Manage_BAL _bal = new Page_Manage_BAL(objconfig);
                var data = _bal.GetPageData_BAL("contact", 1, 1);
                return View(data);
            }
            catch (Exception ex)
            {
                FileLogger.LogError("/pagearticle/Index :", ex);
                return View(new PageViewModel());
            }
            finally
            {
                // _bal.Dispose();
            }
        }
        public IActionResult article(string id)
        {
            try
            {
                Page_Manage_BAL _bal = new Page_Manage_BAL(objconfig);
                var data = _bal.GetPageData_BAL(id, 1, 1);
                return View(data);
            }
            catch (Exception ex)
            {
                FileLogger.LogError("/pagearticle/article :" + id, ex);
                return View(new PageViewModel());
            }
            finally
            {
                // _bal.Dispose();
            }
        }

        public IActionResult Error()
        {
            Response.StatusCode = 404;
            return View();
        }

    }
}