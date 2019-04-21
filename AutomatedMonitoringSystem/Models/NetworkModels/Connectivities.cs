using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Configuration;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;

namespace AutomatedMonitoringSystem.Models.NetworkModels
{
    public class Connectivities
    {
        static bool mailSent = false;
        static bool menableSsl;
        static string mHostID, mUserID, mUserPass, mFromAddress;
        static int mPortID;
        private static void ReadConfiguration()
        {
            Configuration config = WebConfigurationManager.OpenWebConfiguration(HttpContext.Current.Request.ApplicationPath);
            MailSettingsSectionGroup settings = (MailSettingsSectionGroup)config.GetSectionGroup("system.net/mailSettings");
            mHostID = settings.Smtp.Network.Host;
            mPortID = settings.Smtp.Network.Port;
            mFromAddress = settings.Smtp.From;
            mUserID = settings.Smtp.Network.UserName;
            mUserPass = settings.Smtp.Network.Password;
            menableSsl = settings.Smtp.Network.EnableSsl;

        }
        public bool SendEmail(string usermail, string username, string subject, string msg)
        {
            ReadConfiguration();
            MailMessage message = new MailMessage();
            message.From = new MailAddress(mFromAddress);
            message.To.Add(usermail.Trim());
            message.Subject = subject;
            message.Body = "Dear " + username + ",<br /><br />" + msg
                            +
                            "<br /><br />" +
                           "Regards," + Environment.NewLine +
                            "Company Name";
            message.IsBodyHtml = true;

            SmtpClient client = new SmtpClient();//smtp.gmail.com 587
            client.Host = mHostID;
            client.Port = mPortID;
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential(mUserID, mUserPass);
            client.EnableSsl = menableSsl;

            try
            {
                client.Send(message);
            }
            catch (Exception ex)
            {
                throw new Exception("Cannot send message: " + ex.Message);
            }

            return true;
        }
        public static void SendMail(string tomail, string body)
        {
            ReadConfiguration();
            MailMessage message = new MailMessage();
            message.From = new MailAddress(mFromAddress);
            message.To.Add(tomail.Trim());
            message.Subject = "Change Your Password.";//"Your user & access code";
            message.Body = body;//"Your user code : " + body + " and access code : " + body;

            SmtpClient client = new SmtpClient();//smtp.gmail.com 587
            client.Host = mHostID;
            client.Port = mPortID;
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential(mUserID, mUserPass);
            client.EnableSsl = menableSsl;
            try
            {
                client.Send(message);
            }
            catch (Exception ex)
            {
                throw new Exception("Cannot send message: " + ex.Message);
            }
        }

        public static void SendMail(string tomail)
        {
            ReadConfiguration();
            IPHostEntry ip = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress[] IPaddr = ip.AddressList;
            MailMessage message = new MailMessage();
            try
            {
                message.From = new MailAddress(mFromAddress);
                message.To.Add(tomail);
                message.Subject = "Internet Banking Access Blocked";//"Your user & access code";
                message.Body = " Dear Customer," + Environment.NewLine +
                                "Your access is blocked due to unauthorised attempt from host: " + IPaddr[1].ToString() + "." + Environment.NewLine +
                                "Regards, <br />" +
                                "Company Name";

                SmtpClient client = new SmtpClient();//smtp.gmail.com 587
                client.Host = mHostID;
                client.Port = mPortID;
                client.UseDefaultCredentials = false;
                client.Credentials = new System.Net.NetworkCredential(mUserID, mUserPass);
                client.EnableSsl = menableSsl;

                client.Send(message);
            }


            catch (Exception ex)
            {
                throw new Exception("Cannot send message: " + ex.Message);
            }
        }


        // ------------------- End --------------------- //
    }
}
