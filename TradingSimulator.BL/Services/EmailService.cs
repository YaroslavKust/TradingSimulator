using MailKit.Security;
using MimeKit;
using MailKit.Net.Smtp;

namespace TradingSimulator.BL.Services
{
    public class EmailService: IEmailService
    {
        public void SendNotification(string from, string to, string message)
        {
            from = "jermaine.wolf75@ethereal.email";
            to = "verbovikov80@gmail.com";
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(from));
            email.To.Add(MailboxAddress.Parse(to));
            email.Subject = "Trading Simulator Notification";
            email.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = $"<p>{message}</p>" };

            using var smtp = new SmtpClient();
            
            smtp.Connect("smtp.ethereal.email", 587, SecureSocketOptions.StartTls);
            smtp.Authenticate("jermaine.wolf75@ethereal.email", "VP5PU1m4YbzVCFRvn7");
            var response = smtp.Send(email);
            smtp.Disconnect(true);
        }

        public void SendConfirmation(string from, string to, string message, string linkToConfirm)
        {
            from = "jermaine.wolf75@ethereal.email";
            to = "gpsymswny345scex@ethereal.email";
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(from));
            email.To.Add(MailboxAddress.Parse(to));
            email.Subject = "Trading Simulator Notification";
            email.Body = new TextPart(MimeKit.Text.TextFormat.Html) 
            { 
                Text = $"<p>{message}</p><button><a href='{linkToConfirm}'>Confirm</a></button>" 
            };

            using var smtp = new SmtpClient();

            smtp.Connect("smtp.ethereal.email", 587, SecureSocketOptions.StartTls);
            smtp.Authenticate("jermaine.wolf75@ethereal.email", "VP5PU1m4YbzVCFRvn7");
            smtp.Send(email);
            smtp.Disconnect(true);
        }
    }
}
