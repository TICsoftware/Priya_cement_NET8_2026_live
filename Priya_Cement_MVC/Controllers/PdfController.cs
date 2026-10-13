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
using PDFtoImage;
using SkiaSharp;



namespace Priya_Cement_MVC.Controllers;

public class PdfController : Controller
{
    private readonly IWebHostEnvironment _env;

    public PdfController(IWebHostEnvironment env)
    {
        _env = env;
    }

    public IActionResult Thumbnail(string url)
    {
        if (string.IsNullOrWhiteSpace(url))
            return NotFound();

        // /uploads/pdf/demo.pdf
        var relativePath = url.TrimStart('/');

        // wwwroot/uploads/pdf/demo.pdf
        var pdfPath = Path.Combine(
            _env.WebRootPath,
            relativePath.Replace('/', Path.DirectorySeparatorChar));

        if (!System.IO.File.Exists(pdfPath))
            return NotFound();

        using var stream = System.IO.File.OpenRead(pdfPath);

        // First page
        using var bitmap = Conversion.ToImage(stream, page: 0);

        using var image = SKImage.FromBitmap(bitmap);

        using var data = image.Encode(
            SKEncodedImageFormat.Jpeg,
            80);

        return File(data.ToArray(), "image/jpeg");
    }
}
