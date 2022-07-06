
using Microsoft.Extensions.Configuration;
using Notifications.Models;
using Notifications.ViewModels;
using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Notifications
{
    public class EmailNotification : Notifications
    {
        //private readonly IEmailSender _emailSender;
        private readonly string _subject;
        private readonly IConfiguration _config;
        private readonly string _fromAddress;
        private readonly string _fromPass;
        protected MailMessage _mailMessage;
        private readonly EmailServerModel _smtp;

        public EmailNotification(IConfiguration config, EmailVM emailVM) : base(emailVM)
        {
            //_emailSender = emailSender;
            _subject = emailVM.Subject;
            _config = config;
            _fromAddress = _config.GetSection("EmailNotifications").GetSection("admin").GetSection("email").Value;
            _fromPass = _config.GetSection("EmailNotifications").GetSection("admin").GetSection("password").Value;
            _smtp = new EmailServerModel
            {
                SMTP = _config.GetSection("EmailNotifications").GetSection("admin").GetSection("server").Value,
                Port = Int32.Parse(_config.GetSection("EmailNotifications").GetSection("admin").GetSection("port").Value)
            };

        }

        public override void ConstructMessageToSend()
        {
            MailMessage mail = new MailMessage();

            mail.From = new MailAddress(_fromAddress);
            mail.To.Add(base.SendToAddress);
            mail.Subject = _subject;
            mail.Body = base.Message;
            mail.IsBodyHtml = true;

            _mailMessage = mail;
        }

        public async override Task SendNotification()
        {
            SmtpClient smtp = new SmtpClient(_smtp.SMTP, _smtp.Port);

            try
            {

                smtp.UseDefaultCredentials = false;
                smtp.Timeout = 20000;
                smtp.Credentials = new NetworkCredential(_fromAddress, _fromPass);
                smtp.EnableSsl = true;


                await smtp.SendMailAsync(_mailMessage);

            }
            catch (Exception e)
            {
                throw new Exception($"Email did not send: \\nError thrown: {e} \\n \\nCredential Provided: From Address: {_fromAddress}, To Address: {base.SendToAddress}, Pass: {_fromPass}, Subject {_subject}, Message: {base.Message}");
            }
        }
    }
}
