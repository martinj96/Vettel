using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Transpo.Infrastructure.Data.Identity
{
    public class EmailService 
    {
        public Task SendAsync(MailMessage message)
        {
            SmtpClient client = new SmtpClient();
            return client.SendMailAsync(message);
        }
    }
}
