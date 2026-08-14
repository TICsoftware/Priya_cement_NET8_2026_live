using System;
using System.Collections.Generic;
using System.Data;
using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Configuration;
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

        public List<CommonDropdownModel> GetAreaOfInterestList()
        {
            return GetAreaOfInterestList_DAL();
        }

        public void SendMail(string emailContent, string subject)
        {
            try
            {
                var settings = _configuration.GetSection("MailSetting");

                string host = settings["hostname"];
                string username = settings["mailusername"];
                string password = settings["mailpassword"];
                int port = Convert.ToInt32(settings["Port"]);
                string displayName = settings["DisplayName"];

                using var message = new MailMessage();

                message.To.Add("receiver@example.com");

                //message.From = new MailAddress(username);
                message.From = new MailAddress(settings["From"], displayName);
                message.To.Add(new MailAddress(settings["To"], displayName));

                message.Subject = subject;
                message.Body = emailContent;
                message.IsBodyHtml = true;

                using var smtp = new SmtpClient(host)
                {
                    Port = port,
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

        public void SendMail1(string emailContent, string subject)
        {
            try
            {
                var settings = _configuration.GetSection("MailSetting");

                using var message = new MailMessage();

                string displayName = settings["DisplayName"];

                // Receiver
                message.To.Add(new MailAddress(settings["To"], displayName));

                // Sender
                message.From = new MailAddress(settings["From"], displayName);

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
                        settings["Password"]
                    )
                };

                smtp.Send(message);
            }
            catch (Exception ex)
            {
                // Log exception
                throw;
            }
        }

        // public void SendMail(string emailContent, string subject)
        // {
        //     try
        //     {
        //         var settings = _configuration.GetSection("EmailSettings");

        //         using var message = new MailMessage();
        //         string displayName = settings["DisplayName"];

        //         message.To.Add(new MailAddress(settings["To"]!, settings["DisplayName"]));
        //         message.From = new MailAddress(settings["From"]!, displayName);
        //         message.Subject = subject;
        //         message.Body = emailContent;
        //         message.IsBodyHtml = true;

        //         using var smtp = new SmtpClient(settings["Host"])
        //         {
        //             Port = Convert.ToInt32(settings["Port"]),
        //             EnableSsl = true,
        //             UseDefaultCredentials = false,
        //             Credentials = new NetworkCredential(
        //                 settings["Username"],
        //                 settings["Password"])
        //         };

        //         smtp.Send(message);
        //     }
        //     catch
        //     {
        //     }
        // }

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
                (!string.IsNullOrWhiteSpace(obj.Query)
                    ? "<tr><td><strong>Message</strong></td><td>" + obj.Query + "</td></tr>"
                    : "") +
                "</tbody></table>" +
                "<br/><p><strong>Regards,</strong><br/><strong>Priya Cement Website</strong></p>";
        }
    }
}
