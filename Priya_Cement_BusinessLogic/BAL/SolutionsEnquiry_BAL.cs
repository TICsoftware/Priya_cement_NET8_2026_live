using System;
using System.Data;
using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Configuration;
using Priya_Cement_BusinessLogic.Common;
using Priya_Cement_BusinessLogic.DAL;
using Priya_Cement_BusinessLogic.Entity;

namespace Priya_Cement_BusinessLogic.BAL
{
    public class SolutionsEnquiry_BAL : SolutionsEnquiry_DAL
    {
        private readonly IConfiguration _configuration;
        public SolutionsEnquiry_BAL(IConfiguration configuration) : base(configuration)
        {
            _configuration = configuration;
        }

        public DataTable SubmitEnquiry_BAL(SolutionsEnquiry model)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = AddSolutionsEnquiry_DAL(model);

                if (dt.Rows.Count > 0 && Convert.ToInt32(dt.Rows[0][0]) > 0)
                {
                    SendMail(MailSolutionsEnquiryContent(model), "Solutions Enquiry from " + model.FullName);
                }

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


        public string MailSolutionsEnquiryContent(SolutionsEnquiry obj)
        {
            string district = obj.District == "Other"
                ? obj.OtherDistrict
                : obj.District;

            string bodyhtmlcontent =
                "<h4>Dear Team,</h4>" +
                "<p>Please find below the Solution Centre Enquiry submitted through the website.</p>" +

                "<table border='1' cellpadding='6' cellspacing='0' " +
                "style='border-collapse:collapse;width:100%;'>" +
                "<tbody>" +

                "<tr>" +
                "<td><strong>Full Name</strong></td>" +
                "<td>" + obj.FullName + "</td>" +
                "</tr>" +

                "<tr>" +
                "<td><strong>Mobile Number</strong></td>" +
                "<td>" + obj.MobileNumber + "</td>" +
                "</tr>" +

                (!string.IsNullOrWhiteSpace(obj.WhatsappNumber)
                    ? "<tr><td><strong>WhatsApp Number</strong></td><td>" +
                      obj.WhatsappNumber + "</td></tr>"
                    : "") +

                "<tr>" +
                "<td><strong>Email Address</strong></td>" +
                "<td>" + obj.EmailId + "</td>" +
                "</tr>" +

                (!string.IsNullOrWhiteSpace(obj.Gender)
                    ? "<tr><td><strong>Gender</strong></td><td>" +
                      obj.Gender + "</td></tr>"
                    : "") +

                (!string.IsNullOrWhiteSpace(obj.AgeGroup)
                    ? "<tr><td><strong>Age Group</strong></td><td>" +
                      obj.AgeGroup + "</td></tr>"
                    : "") +

                "<tr>" +
                "<td><strong>District</strong></td>" +
                "<td>" + district + "</td>" +
                "</tr>" +

                "<tr>" +
                "<td><strong>Town / Village</strong></td>" +
                "<td>" + obj.TownVillage + "</td>" +
                "</tr>" +

                "<tr>" +
                "<td><strong>Current Occupation</strong></td>" +
                "<td>" + obj.CurrentOccupation + "</td>" +
                "</tr>" +

                (!string.IsNullOrWhiteSpace(obj.CurrentOccupationOthers)
                    ? "<tr><td><strong>Current Occupation - Other</strong></td><td>" +
                      obj.CurrentOccupationOthers + "</td></tr>"
                    : "") +

                "<tr>" +
                "<td><strong>Own Shop / Commercial Space</strong></td>" +
                "<td>" + obj.OwnShopCommercialSpace + "</td>" +
                "</tr>" +

                "<tr>" +
                "<td><strong>Previously Run Business</strong></td>" +
                "<td>" + obj.PreviouslyRunBusiness + "</td>" +
                "</tr>" +

                "<tr>" +
                "<td><strong>Space Available for Store Setup</strong></td>" +
                "<td>" + obj.SpaceForStoreSetup + "</td>" +
                "</tr>" +

                (!string.IsNullOrWhiteSpace(obj.StoreSizeSqft)
                    ? "<tr><td><strong>Store Size (Sq. Ft.)</strong></td><td>" +
                      obj.StoreSizeSqft + "</td></tr>"
                    : "") +

                (!string.IsNullOrWhiteSpace(obj.PreferredTimeForContact)
                    ? "<tr><td><strong>Preferred Time for Contact</strong></td><td>" +
                      obj.PreferredTimeForContact + "</td></tr>"
                    : "") +

                "</tbody>" +
                "</table>" +

                "<br/>" +
                "<p><strong>Regards,</strong><br/>" +
                "<strong>Priya Cement Website</strong></p>";

            return bodyhtmlcontent;
        }


        public void SendMail(string emailContent, string subject)
        {
            try
            {
                var settings = _configuration.GetSection("MailSetting");

                string host = settings["hostname"] ?? string.Empty;
                string username = settings["mailusername"] ?? string.Empty;
                string password = settings["mailpassword"] ?? string.Empty;
                string from = settings["From"] ?? string.Empty;
                string to = settings["SolutionCentre"] ?? string.Empty;
                string displayName = settings["DisplayName"] ?? string.Empty;

                int.TryParse(settings["Port"], out int port);

                if (string.IsNullOrWhiteSpace(host) ||
                    string.IsNullOrWhiteSpace(username) ||
                    string.IsNullOrWhiteSpace(password) ||
                    string.IsNullOrWhiteSpace(from) ||
                    string.IsNullOrWhiteSpace(to))
                {
                    throw new Exception("Mail settings are missing or invalid.");
                }

                using var message = new MailMessage();

                message.From = new MailAddress(from, displayName);

                // Multiple email addresses separated by comma
                foreach (var email in to.Split(',', StringSplitOptions.RemoveEmptyEntries))
                {
                    message.To.Add(email.Trim());
                }

                message.Subject = subject ?? string.Empty;
                message.Body = emailContent ?? string.Empty;
                message.IsBodyHtml = true;

                using var smtp = new SmtpClient(host)
                {
                    Port = port > 0 ? port : 587,
                    EnableSsl = true,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(username, password)
                };

                smtp.Send(message);
            }
            catch (Exception ex)
            {
                Priya_CementFileLogger.LogError("Solutions Enquiry :", ex);
                throw new Exception("Email sending failed: " + ex.Message, ex);
            }
        }




    }
}
