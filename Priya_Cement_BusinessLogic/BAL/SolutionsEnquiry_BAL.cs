using System;
using System.Data;
using Microsoft.Extensions.Configuration;
using Priya_Cement_BusinessLogic.DAL;
using Priya_Cement_BusinessLogic.Entity;

namespace Priya_Cement_BusinessLogic.BAL
{
    public class SolutionsEnquiry_BAL : SolutionsEnquiry_DAL
    {
        public SolutionsEnquiry_BAL(IConfiguration configuration) : base(configuration)
        {
        }

        public DataTable SubmitEnquiry_BAL(SolutionsEnquiry model)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = AddSolutionsEnquiry_DAL(model);
            }
            catch (Exception ex)
            {
                if (dt.Columns.Count == 0)
                {
                    dt.Columns.Add("status");
                    dt.Rows.Add(ex.Message);
                }
                else if (dt.Rows.Count == 0)
                {
                    dt.Rows.Add(ex.Message);
                }
                else
                {
                    dt.Rows[0][0] = ex.Message;
                }
            }

            return dt;
        }
    }
}
