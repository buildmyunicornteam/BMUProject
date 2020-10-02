using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Web;

namespace Model_Layer.Helper
{
    public class EmailSender
    {
        public string SendMail(string From, string To, string Subject, string Body)
        {
            try
            {

                SmtpClient SmtpServer = new SmtpClient();
                MailMessage mail = new MailMessage();
                //Add SMTP Credentials Details
                SmtpServer.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["SmtpServerUsername"].ToString(), ConfigurationManager.AppSettings["SmtpServerPassword"].ToString());

                // Add SMTP Port
                //  SmtpServer.Port = Convert.ToInt32(587);
                SmtpServer.Port = Convert.ToInt32(ConfigurationManager.AppSettings["SmtpServerPort"]);
                //Add SMTP Server Details
                SmtpServer.Host = ConfigurationManager.AppSettings["SmtpServerHost"].ToString();

                //Add Email IsSSl
                SmtpServer.EnableSsl = true;
                mail = new MailMessage();
                //Add From Email 
                mail.From = new MailAddress(From);

                //Add Recipent Email Address
                mail.To.Add(To);
                //Addd Email Siubject
                mail.Subject = Subject;
                //Add Email Body
                mail.Body = Body;
                //Enable HTML BODY
                mail.IsBodyHtml = true;
                //Certificate Expired Check Required
                bool certificatecheck = false;
                if (certificatecheck == true)
                {
                    ServicePointManager.ServerCertificateValidationCallback = (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) => true;
                }
                SmtpServer.Send(mail);
                return "OK";
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }
    }
}