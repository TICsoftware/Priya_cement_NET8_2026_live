using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Priya_Cement_BusinessLogic.Entity;

namespace Priya_Cement_BusinessLogic.DAL
{
    public class Product_DAL : Page_Manage_DAL
    {
        public Product_DAL(IConfiguration configuration) : base(configuration)
        {
        }

        public DataSet GetProductPage_DAL(string pagename, int languageId, int geographyId)
        {
            return GetContentComponentData_DAL(pagename, languageId, geographyId);
        }

       

    }
}
