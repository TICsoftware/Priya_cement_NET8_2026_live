using System.Data;
using Microsoft.Extensions.Configuration;

namespace Priya_Cement_BusinessLogic.DAL
{
    public class Careers_DAL : Page_Manage_DAL
    {
        public Careers_DAL(IConfiguration configuration) : base(configuration)
        {
        }

        public DataSet GetCareersPage_DAL(string pagename, int languageId, int geographyId)
        {
            return GetContentComponentData_DAL(pagename, languageId, geographyId);
        }
    }
}
