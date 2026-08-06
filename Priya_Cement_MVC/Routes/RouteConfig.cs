using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Priya_Cement_MVC.Routes
{
    public static class RouteConfig
    {
        public static void RegisterRoutes(this WebApplication app)
        {
            // ✅ Custom routes first



            app.MapControllerRoute(
                  name: "ourproducts",
                  pattern: "what-we-offer",
                  defaults: new { controller = "Product", action = "Index", title = "what-we-offer" }
              );





            app.MapControllerRoute(
                          name: "products-inside",
                          pattern: "what-we-offer/{title?}",
                          defaults: new { controller = "Product", action = "Inside" }
                      );

            app.MapControllerRoute(
               name: "Test",
               pattern: "test",
               defaults: new { controller = "Product", action = "Test" }
           );

            app.MapControllerRoute(
              name: "SubmitTechnicalSupport",
              pattern: "Product/SubmitTechnicalSupport",
              defaults: new { controller = "Product", action = "SubmitTechnicalSupport" }
          );



            app.MapControllerRoute(
                name: "LoadMoreSearch",
                pattern: "Search/LoadMoreSearch",
                defaults: new { controller = "Search", action = "LoadMoreSearch" }
            );

            app.MapControllerRoute(
                name: "search",
                pattern: "search/{id?}",
                defaults: new { controller = "Search", action = "Index" }
            );


            app.MapControllerRoute(
                name: "contactus",
                pattern: "contactus",
                defaults: new { controller = "Contactus", action = "Index", title = "contact" }
            );

            app.MapControllerRoute(
                name: "legal-disclaimer",
                pattern: "legal-disclaimer",
                defaults: new { controller = "pagearticle", action = "article", id = "legal-disclaimer" }
            );

            app.MapControllerRoute(
                name: "privacy-policy",
                pattern: "privacy-policy",
                defaults: new { controller = "pagearticle", action = "article", id = "privacy-policy" }
            );

            app.MapControllerRoute(
             name: "terms-of-use",
             pattern: "terms-and-conditions",
             defaults: new { controller = "pagearticle", action = "article", id = "terms-and-conditions" }
         );
            app.MapControllerRoute(
              name: "sitemap",
              pattern: "sitemap",
              defaults: new { controller = "pagearticle", action = "article", id = "sitemap" }
          );


            app.MapControllerRoute(
                name: "Error",
                pattern: "Error",
                defaults: new { controller = "pagearticle", action = "Error" }
            );

            // ✅ Area / Admin route (before default)
            app.MapControllerRoute(
                name: "manage",
                pattern: "Manage/{action=Login}/{id?}",
                defaults: new { controller = "Manage" }
            );

            // ✅ Default route LAST
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}"
            );
        }
    }
}