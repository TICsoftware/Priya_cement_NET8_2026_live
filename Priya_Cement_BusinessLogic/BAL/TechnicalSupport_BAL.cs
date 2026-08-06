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
                var settings = _configuration.GetSection("EmailSettings");

                using var message = new MailMessage();

                string displayName = settings["DisplayName"];

                message.To.Add(new MailAddress(settings["To"]!, settings["DisplayName"]));



                message.From = new MailAddress(settings["From"]!, displayName);
                message.Subject = subject;
                message.Body = emailContent;
                message.IsBodyHtml = true;

                using var smtp = new SmtpClient(settings["Host"])
                {
                    Port = Convert.ToInt32(settings["Port"]),
                    EnableSsl = true,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(
                        settings["Username"],
                        settings["Password"])
                };

                smtp.Send(message);
            }
            catch (Exception ex)
            {
                //FileLogger.LogError("EmailService/SendMail", ex);
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
                "<td>" + obj.StateId + "</td>" +
                "</tr>" +

                "<tr>" +
                "<td><strong>Site Location</strong></td>" +
                "<td>" + obj.CityId + "</td>" +
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