using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;
using Transpo.AppServices.DTOs;

namespace Transpo.AppServices
{
    public class EmailService
    {
        public void SendEmail(EmailDto emailDto)
        {
            var message = new MailMessage(
                emailDto.FromEmail,
                emailDto.ToEmail,
                emailDto.Subject,
                emailDto.Body);
            message.IsBodyHtml = true;

            using (var client = new SmtpClient())
            {
                client.Send(message);
            }
        }
    }
}
