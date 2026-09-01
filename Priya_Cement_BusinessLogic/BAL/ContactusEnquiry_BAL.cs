using System;
using System.Collections.Generic;
using System.Data;
using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Configuration;
using Priya_Cement_BusinessLogic.Common;
using Priya_Cement_BusinessLogic.DAL;
using Priya_Cement_BusinessLogic.Entity;

namespace Priya_Cement_BusinessLogic.BAL
{
    public class ContactusEnquiry_BAL : ContactusEnquiry_DAL
    {
        private readonly IConfiguration _configuration;

        public ContactusEnquiry_BAL(IConfiguration configuration) : base(configuration)
        {
            _configuration = configuration;
        }

        public DataTable SubmitEnquiry_BAL(ContactUsEnquiry model)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = AddContactUsEnquiry_DAL(model);
                if (dt.Rows.Count > 0 && dt.Rows[0][0].ToString() == "updated")
                {
                    SendMail(MailEnquiryContent(model), "Contact enquiry from " + model.FullName);
                }
            }
            catch (Exception ex)
            {
                if (dt.Columns.Count == 0)
                {
                    dt.Columns.Add("Result");
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


        public List<CommonDropdownModel> GetStateList()
        {
            return GetStateList_DAL();
        }
        public List<CommonDropdownModel> GetCityList()
        {
            return GetCityList_DAL();
        }

        public List<CommonDropdownModel> GetContactCategoryList()
        {
            return GetContactCategoryList_DAL();
        }

        public List<CommonDropdownModel> GetAreaOfInterestList()
        {
            return GetAreaOfInterestList_DAL();
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
                string to = settings["ContactUs"] ?? string.Empty;
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
                Priya_CementFileLogger.LogError("/Contactus/Index :", ex);
                throw new Exception("Email sending failed: " + ex.Message, ex);
            }
        }

        public string MailEnquiryContent(ContactUsEnquiry obj)
        {
            return
                "<h4>Dear Team,</h4>" +
                "<p>Please find below the Contact Us enquiry submitted through the website.</p>" +
                "<table border='1' cellpadding='6' cellspacing='0' style='border-collapse:collapse;width:100%;'>" +
                "<tbody>" +
                "<tr><td><strong>Full Name</strong></td><td>" + obj.FullName + "</td></tr>" +
                (!string.IsNullOrWhiteSpace(obj.Designation)
                    ? "<tr><td><strong>Designation</strong></td><td>" + obj.Designation + "</td></tr>"
                    : "") +
                "<tr><td><strong>Company</strong></td><td>" + obj.Organisation + "</td></tr>" +
                "<tr><td><strong>Email</strong></td><td>" + obj.Email + "</td></tr>" +
                "<tr><td><strong>Phone</strong></td><td>" + obj.Phone + "</td></tr>" +
                "<tr><td><strong>City</strong></td><td>" + obj.City + "</td></tr>" +
                "<tr><td><strong>State</strong></td><td>" + obj.State + "</td></tr>" +
                "<tr><td><strong>Category</strong></td><td>" + obj.CategoryTitle + "</td></tr>" +
                (!string.IsNullOrWhiteSpace(obj.Query)
                    ? "<tr><td><strong>Message</strong></td><td>" + obj.Query + "</td></tr>"
                    : "") +
                "</tbody></table>" +
                "<br/><p><strong>Regards,</strong><br/><strong>Priya Cement Website</strong></p>";
        }
    }
}
