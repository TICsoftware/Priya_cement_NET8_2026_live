using System.Globalization;
using System.Net;
using System.Security.Authentication;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Extensions.FileProviders;
using Priya_Cement_MVC.Policy;
using Priya_Cement_MVC.Routes;

var builder = WebApplication.CreateBuilder(args);

var enableTls12 = builder.Configuration.GetValue<string>("EnableTls12");

// 👉 Optional (legacy support - only if really needed)
if (enableTls12 == "1")
{
    ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls12;
}

// 👉 Recommended modern approach (HttpClient)
builder.Services.AddHttpClient("MyClient")
    .ConfigurePrimaryHttpMessageHandler(() =>
        new HttpClientHandler
        {
            SslProtocols = SslProtocols.Tls12
        });

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();

// Add services to the container.
//builder.Services.AddControllersWithViews();
//.AddRazorRuntimeCompilation();
if (builder.Environment.IsDevelopment())
{
    builder.Services.AddControllersWithViews()
    .AddRazorRuntimeCompilation();//uncomment while live
}
else
{
    builder.Services.AddControllersWithViews()
      .AddRazorRuntimeCompilation();
}


builder.Services.AddAuthentication("MyCookieAuth")
    .AddCookie("MyCookieAuth", options =>
    {
        options.LoginPath = "/Manage/Login";
        options.AccessDeniedPath = "/Manage/AccessDenied";
        options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
    });

builder.Services.AddAuthorization();

//uncomment while live start
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(20); // session timeout
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

 builder.Services.AddAntiforgery(options =>
{
    options.Cookie.Name = "SecureToken";
    options.Cookie.HttpOnly = true;
   options.Cookie.SecurePolicy =
        CookieSecurePolicy.Always;  
});
//uncomment while live end

builder.Services.AddHttpClient();


// Kestrel limit
builder.WebHost.ConfigureKestrel(options =>
{
    options.Limits.MaxRequestBodySize = 1073741824;
    options.AddServerHeader = false;
});

// Multipart/form-data limit (file uploads)
builder.Services.Configure<Microsoft.AspNetCore.Http.Features.FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = 1073741824; // 1 GB
});

// builder.Services.Configure<Microsoft.AspNetCore.Http.Features.FormOptions>(options =>
// {
//     options.MultipartBodyLengthLimit = 52428800; // 50 MB
// });


builder.Services.AddScoped<PdfThumbnailService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});

//uncomment while live
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(app.Environment.ContentRootPath, "Assets")),
    RequestPath = "/Assets"
});

// Handles 404, 403, etc. when no endpoint produces the response
app.UseStatusCodePagesWithReExecute("/Error");

var redirects = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
{
    // Home page
    { "^index\\.html$", "/" },
    { "^index_new\\.html$", "/" },
    { "^404_page\\.html$", "/" },
    { "^google30c494c5d635adbc\\.html$", "/" },

    // Common pages
    { "^disclaimer\\.html$", "/disclaimer" },
    { "^privacy-policy\\.html$", "/privacy-policy" },
    { "^sitemap\\.html$", "/sitemap" },
    { "^terms-of-use\\.html$", "/terms-of-use" },

    // Technical Services
    { "^technical-services\\.html$", "/solutions/technical-services" },

    // Contact
    { "^dealership_enquiry$", "/solutions/enquiry" },
    { "^contact-us/contact\\.html$", "/contact-us" },
     { "^Solutions/Enquiry$", "/solutions/enquiry" },

    // About Us
    { "^about-us/overview\\.html$", "/about-us" },

    // Products / Solutions
    { "^products/processes\\.html$", "/about-us/manufacturing-and-processing" },
    { "^products/processes1\\.html$", "/about-us/manufacturing-and-processing" },
    { "^products/processes2\\.html$", "/about-us/manufacturing-and-processing" },
    { "^products/processes3\\.html$", "/about-us/manufacturing-and-processing" },
    { "^products/processes4\\.html$", "/about-us/manufacturing-and-processing" },
    { "^products/processes5\\.html$", "/about-us/manufacturing-and-processing" },
    { "^products/processes6\\.html$", "/about-us/manufacturing-and-processing" },
    { "^products/processes7\\.html$", "/about-us/manufacturing-and-processing" },
    { "^products/processes8\\.html$", "/about-us/manufacturing-and-processing" },

    { "^/about-us/financial-Information/annual-reports$", "/about-us/financial-information" },
    { "^/about-us/financial-Information/annual-return-in-form-mgt-7$", "/about-us/financial-information" },

    { "^products/products-opc-and-ppc\\.html$", "/solutions" },
    { "^products/products-opc-and-ppc\\.html#OPC$", "/solutions/maxload-opc-53-grade" },
    { "^products/products-opc-and-ppc\\.html#PPC1$", "/solutions/portland-pozzolana-cement" },

    { "^products/products-maxload-bulk-cement\\.html$", "/solutions/maxload-bulk-cement" },
    { "^/solutions/ppc$", "/solutions/portland-pozzolana-cement" },

    // Social Responsibility
   { "^social-responsibility/overview\\.html$", "/esg/csr" },
    { "^social-responsibility/join-us\\.html$", "/esg/csr" },
    { "^social-responsibility/privacy-policy\\.html$", "/esg/csr" },
    { "^social-responsibility/404_page\\.html$", "/esg/csr" },
    { "^social-responsibility/rcl-csr-policy\\.pdf$", "/esg/csr" },

    { "^/esg$", "/esg/sustainability" },
    { "^/esg/sustainability/safety$", "/esg/safety" },
    { "^/esg/sustainability/csr$", "/esg/csr" },


    { "^/pdf/rcl-mgt-7-2021\\.pdf$", "/uploads/pdf/rcl-mgt-7-2021.pdf" },
    { "^/pdf/rcl-mgt-7-2022\\.pdf$", "/uploads/pdf/rcl-mgt-7-2022.pdf" },
    { "^/pdf/rain-cements-annual-reports-2021\\.pdf$", "/uploads/pdf/rain-cements-annual-reports-2021.pdf" },
    { "^/pdf/rain-cements-annual-reports-2022\\.pdf$", "/uploads/pdf/rain-cements-annual-reports-2022.pdf" },
    { "^/pdf/rcl-csr-committee-composition-and-csr-projects-2024\\.pdf$", "/uploads/pdf/rcl-csr-committee-composition-and-csr-projects-2024.pdf" },
    { "^/pdf/rcl-csr-committee-composition-and-csr-projects-2023\\.pdf$", "/uploads/pdf/rcl-csr-committee-composition-and-csr-projects-2023.pdf" },
    { "^/pdf/rcl-csr-policy\\.pdf$", "/uploads/pdf/rcl-csr-policy.pdf" },

    { "^/pdf/rcl-unit-ii\\s+ml-1-details-committed-csr-expenditure\\.pdf$", "/esg/csr" },
    { "^/pdf/rcl-unit-ii-ml-2-details-committed-sdu-frame-work\\.pdf$", "/esg/csr" },
    { "^/pdf/rcl-unit-ii\\s+ml-1-details-committed-sdu-frame-work\\.pdf$", "/esg/csr" },
    { "^/pdf/V_1_IA_TG_IND1_467947_2024_57732343\\.pdf$", "/esg/sustainability" },
    { "^/pdf/rain-supplier-code-conduct-polish\\.pdf$", "/error/index" },

    // Media
     { "^media/media\\.html$", "/media/press-releases" },
    { "^media/gallery\\.html$", "/media/archive" },
    { "^media/CorporateFilm\\.html$", "/media/campaigns-and-tvcs" },
    { "^media/Print\\.html$", "/media/press-releases" },
    { "^media/?$", "/media/press-releases" },

    // Careers
    { "^careers/join-us\\.html$", "/careers" },
    { "^careers/current_opening\\.html$", "/careers" },
    { "^careers/current-opening\\.html$", "/careers" },
    { "^careers/index\\.html$", "/careers" },
    { "^/about-cop/careers$", "/careers" },

{ "^/tools-and-guides$", "/tools-and-guides/guides" },

    // Financial Information
    { "^financial-information/financial-information\\.html$", "/about-us/financial-information" },

    // Regulatory Environment Reports
    { "^regulatory-environment-reports/index\\.html$", "/esg/sustainability" },

    { "^investor-relations$", "/about-us/financial-information" },

    { "^/contactus$", "/contact-us" },
    { "^/Contact-Us$", "/contact-us" },

    { "^home$", "/" },

    // Any other old .html URL → Home page
    { "^.*\\.html$", "/" }
};

var options = new RewriteOptions();

foreach (var rule in redirects)
{
    options.AddRedirect(rule.Key, rule.Value, StatusCodes.Status301MovedPermanently);
}

// ✅ IMPORTANT: Must be before UseRouting()
app.UseRewriter(options);
var culture = new CultureInfo("en-US");
app.UseRequestLocalization(new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture(culture),
    SupportedCultures = new[] { culture },
    SupportedUICultures = new[] { culture }
});
app.Use(async (context, next) =>
{
    context.Response.Headers["Vary"] = "Accept-Encoding";
    context.Response.Headers["Access-Control-Allow-Headers"] = "Origin, Content-Type, Accept";
    context.Response.Headers["X-XSS-Protection"] = "1; mode=block";
    context.Response.Headers["X-Frame-Options"] = "SAMEORIGIN";
    context.Response.Headers["X-Content-Type-Options"] = "nosniff";
    context.Response.Headers["Referrer-Policy"] = "strict-origin-when-cross-origin";
    context.Response.Headers["X-Permitted-Cross-Domain-Policies"] = "none";
    //uncommen for live after ssl
    context.Response.Headers["Strict-Transport-Security"] = "max-age=31536000; includeSubDomains";
    context.Response.Headers["Permissions-Policy"] = "accelerometer=(), camera=(), geolocation=(), gyroscope=(), magnetometer=(), microphone=(self), payment=(), fullscreen=(self)";
    context.Response.Headers["Cache-Control"] = "no-cache, no-store, must-revalidate";
    context.Response.Headers["Pragma"] = "no-cache";
    context.Response.Headers["Expires"] = "0";

        // Content Security Policy
// Content Security Policy - Allow resources from all domains
context.Response.Headers["Content-Security-Policy"] =
    "default-src * 'self' data: blob: 'unsafe-inline' 'unsafe-eval'; " +
    "script-src * 'self' data: blob: 'unsafe-inline' 'unsafe-eval'; " +
    "style-src * 'self' data: blob: 'unsafe-inline'; " +
    "img-src * 'self' data: blob:; " +
    "font-src * 'self' data:; " +
    "connect-src * 'self'; " +
    "frame-src * 'self' data: blob:; " +
    "media-src * 'self' data: blob:; " +
    "object-src * 'self' data: blob:;";

    // Remove unwanted headers
    context.Response.Headers.Remove("Server");
    context.Response.Headers.Remove("X-Powered-By");
    context.Response.Headers.Remove("X-AspNetMvc-Version");

    await next();
});

//app.UseCspPolicy();

app.UseRouting();

app.UseSession();

app.UseAuthentication();

app.UseAuthorization(); 

app.RegisterRoutes(); 

app.Run();
