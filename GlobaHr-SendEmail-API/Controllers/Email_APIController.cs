using GlobaHr_SendEmail_API.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;

namespace GlobaHr_SendEmail_API.Controllers
{
    public class Email_APIController : ApiController
    {
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public class EmailController : ApiController
        {
            [HttpPost]
            public async Task<IHttpActionResult> RequestDemo(DemoRequest request)
            {
                CompanyDetails companyDetails = getCompanyDetails();

                string senderMail = ConfigurationManager.AppSettings["SenderMail"];
                string senderPasswd = ConfigurationManager.AppSettings["SenderPasswd"];

                string demoDetailreceiverMail = ConfigurationManager.AppSettings["demoDetailReceiverMail"];

                string smtpServer = "smtp.office365.com";
                int smtpPort = 587;
                string smtpUser = senderMail;
                string smtpPass = senderPasswd;

                // Email to YOU (notify)
                var notifyMsg = new MailMessage();
                notifyMsg.From = new MailAddress(smtpUser, "Demo Request Bot");
                notifyMsg.To.Add(demoDetailreceiverMail); // Your email
                notifyMsg.Subject = request.CompanyName + " - New Demo Request ";
                notifyMsg.Body = $"Name: {request.Name}" +
                    $"\nCompany Name: {request.CompanyName}" +
                    $"\nEmail: {request.Email}" +
                    $"\nContact: {request.ContactNumber}" +
                    $"\nNumber of Employees: {request.NumberOfEmployees}";

                // Auto-reply to USER
                var autoReply = new MailMessage();
                autoReply.From = new MailAddress(smtpUser, companyDetails.CompanyName);
                autoReply.To.Add(request.Email);
                autoReply.Subject = "Demo request  – " + companyDetails.CompanyName;
                //autoReply.Body = $"Dear {request.Name},\n\nThanks for requesting a demo. Our team will contact you soon.\n\nBest Regards,\nYour Company";
                autoReply.Body = $"Dear {request.Name},\n\nThank you for your interest in " + companyDetails.CompanyName + ". We’ve received your request for a demo " +
                    "and a member of our team will reach out to you shortly to coordinate a convenient time." +
                    "\n\nIf you have any additional questions or details to share, please feel free to reply to this email.\n\nWe look forward to speaking with you." +
                    "\n\nBest regards,\n" + companyDetails.CompanyName + "\n" + companyDetails.Contact + "\n" + companyDetails.Website + "";

                // autoReply.Bcc.Add(convertToCommaSeparated(companyDetails.BCCMailAddress));

                using (var smtp = new SmtpClient(smtpServer, smtpPort))
                {
                    smtp.Credentials = new NetworkCredential(smtpUser, smtpPass);
                    smtp.EnableSsl = true;

                    await smtp.SendMailAsync(notifyMsg);   // Send to YOU

                    await smtp.SendMailAsync(autoReply);   // Send to USER
                }

                return Ok();
            }

            private CompanyDetails getCompanyDetails()
            {
                return new CompanyDetails
                {
                    CompanyName = "GlobalHR Solutions",
                    Contact = "9404524861",
                    Website = "https://www.globalhr.solutions/",
                    BCCMailAddress = new List<string> { "ajinkya.shastri@globalcommit.com", "ak@globalcommit.com", "sd@globalcommit.com" }
                };
            }

            private string convertToCommaSeparated(List<string> mailAddresses)
            {
                string result = string.Join(",", mailAddresses);
                return result;
            }
        }
    }
}
