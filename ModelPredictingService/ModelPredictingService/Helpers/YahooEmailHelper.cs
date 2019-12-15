using ModelPredictingService.Models;
using System.Net;
using System.Net.Mail;

namespace ModelPredictingService.Helpers
{
    public class YahooEmailHelper : IEmailHelper
    {
        private const string Subject = "[GaitAuthenhication] Possible Intrusion Detected";
        private const string Message = "Please ignore this message!";
        private const string Email = ".";
        private const string Password = ".";

        public void SendEmail(string address)
        {
            var loginInfo = new NetworkCredential(Email, Password);
            var mailMessage = new MailMessage();
            var smtpClient = new SmtpClient("smtp.mail.yahoo.com", 465);

            mailMessage.From = new MailAddress(Email);
            mailMessage.To.Add(new MailAddress(address));
            mailMessage.Subject = Subject;
            mailMessage.Body = Message;
            mailMessage.IsBodyHtml = true;

            smtpClient.EnableSsl = true;
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = loginInfo;
            smtpClient.Send(mailMessage);
        }
    }
}