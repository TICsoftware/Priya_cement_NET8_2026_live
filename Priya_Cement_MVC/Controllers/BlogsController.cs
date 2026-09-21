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

public class BlogsController : Controller
{
    private readonly ILogger<BlogsController> _logger;
    private readonly Blog_BAL _bal;

    public BlogsController(ILogger<BlogsController> logger, IConfiguration configuration)
    {
        _logger = logger;
        _bal = new Blog_BAL(configuration);
    }

   
    public IActionResult Index(string title)
    {
        try
        {
            var data = _bal.GetBlog_BAL(title, 1, 1);
            return View(data);
        }
        catch (Exception ex)
        {
            FileLogger.LogError("/Blogs :", ex);
            return View(new AboutModel());
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
            var data = _bal.GetBlogInside_BAL(title, 1, 1);
            return View(data);
        }
        catch (Exception ex)
        {
            FileLogger.LogError("/Blogs/Inside :", ex);
            return View(new ProductModel());
        }
        finally
        {
            _bal.Dispose();
        }
    }



    public ActionResult LoadMoreBlog(int cont_id, int pageSize, int pageNumber)
    {
        try
        {
            var model = _bal.GetBlogs_page_wise_BAL(cont_id, pageSize, pageNumber);
            return PartialView("_blogs_list", model.SectionArticles_List);
        }
        catch (Exception ex)
        {
            FileLogger.LogError("/Blogs LoadMoreBlog :", ex);
            return View(new AboutModel());
        }
        finally
        {
            _bal.Dispose();
        }
    }







}
