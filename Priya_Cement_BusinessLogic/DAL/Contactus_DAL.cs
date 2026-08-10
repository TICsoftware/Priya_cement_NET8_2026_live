using System.Data;
using Microsoft.Extensions.Configuration;

namespace Priya_Cement_BusinessLogic.DAL
{
    public class Contactus_DAL : Page_Manage_DAL
    {
        public Contactus_DAL(IConfiguration configuration) : base(configuration)
        {
        }

        public DataSet GetContactusPage_DAL(string pagename, int languageId, int geographyId)
        {
            return GetContentComponentData_DAL(pagename, languageId, geographyId);
        }
    }
}
