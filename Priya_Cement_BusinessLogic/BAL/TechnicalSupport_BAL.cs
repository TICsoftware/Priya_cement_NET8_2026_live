using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Priya_Cement_BusinessLogic.DAL;
using Priya_Cement_BusinessLogic.Entity;

namespace Priya_Cement_BusinessLogic.BAL
{
    public class TechnicalSupport_BAL : TechnicalSupport_DAL
    {
        private readonly IConfiguration _configuration;
        public TechnicalSupport_BAL(IConfiguration configuration) : base(configuration)
        {
            _configuration = configuration;
        }



        public DataTable SubmitEnquiry_BAL(TechnicalSupportEnquiry model)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = AddTechnicalSupportEnquiry_DAL(model);
                if (dt.Rows[0][0].ToString() == "updated")
                {
                    SendMail(MailEnquiryContent(model), "Service request from " + model.Name);
                }
            }
            catch (Exception ex)
            {
                dt.Rows[0][0] = ex.Message.ToString();
            }
            finally
            {

            }

            return dt;
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
                //string to = settings["To"] ?? string.Empty;
                string to = settings["TechnicalServices"] ?? string.Empty;
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
                throw new Exception("Email sending failed: " + ex.Message, ex);
            }
        }


        public string MailEnquiryContent(TechnicalSupportEnquiry obj)
        {
            string bodyhtmlcontent =
                "<h4>Dear Team,</h4>" +
                "<p>Please find below the Technical Support Enquiry submitted through the website.</p>" +

                "<table border='1' cellpadding='6' cellspacing='0' style='border-collapse:collapse;width:100%;'>" +
                "<tbody>" +

                "<tr>" +
                "<td><strong>Service Type</strong></td>" +
                "<td>" + obj.ServiceTypeId + "</td>" +
                "</tr>" +

                "<tr>" +
                "<td><strong>Name</strong></td>" +
                "<td>" + obj.Name + "</td>" +
                "</tr>" +

                (!string.IsNullOrWhiteSpace(obj.Designation)
                    ? "<tr><td><strong>Designation</strong></td><td>" + obj.Designation + "</td></tr>"
                    : "") +

                "<tr>" +
                "<td><strong>Company Name</strong></td>" +
                "<td>" + obj.CompanyName + "</td>" +
                "</tr>" +

                "<tr>" +
                "<td><strong>Email Address</strong></td>" +
                "<td>" + obj.EmailAddress + "</td>" +
                "</tr>" +

                "<tr>" +
                "<td><strong>Phone Number</strong></td>" +
                "<td>" + obj.PhoneNumber + "</td>" +
                "</tr>" +

                "<tr>" +
                "<td><strong>State</strong></td>" +
                "<td>" + obj.State + "</td>" +
                "</tr>" +

                "<tr>" +
                "<td><strong>City</strong></td>" +
                "<td>" + obj.City + "</td>" +
                "</tr>" +

                "<tr>" +
                "<td><strong>Type of Tests Required</strong></td>" +
                "<td>" + obj.TestTypeId + "</td>" +
                "</tr>" +

                "</tbody>" +
                "</table>" +

                "<br/>" +
                "<p><strong>Regards,</strong><br/>" +
                "<strong>Priya Cement Website</strong></p>";

            return bodyhtmlcontent;
        }


        public List<CommonDropdownModel> GetServiceTypeList()
        {
            return GetServiceTypeList_DAL();
        }

        public List<CommonDropdownModel> GetStateList()
        {
            return GetStateList_DAL();
        }

        public List<CommonDropdownModel> GetCityList()
        {
            return GetCityList_DAL();
        }

        public List<CommonDropdownModel> GetTestTypeList()
        {
            return GetTestTypeList_DAL();
        }

        public DataTable GetCityByState(int stateId)
        {
            return GetCityByState_DAL(stateId);
        }

        public DataTable GetTestTypeByService(int serviceTypeId)
        {
            return GetTestTypeByService_DAL(serviceTypeId);
        }


    }
}