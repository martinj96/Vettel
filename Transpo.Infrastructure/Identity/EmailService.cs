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
    public class EmailService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            SmtpClient client = new SmtpClient();
            return client.SendMailAsync("info@kinisaj.mk",
                                        message.Destination,
                                        message.Subject,
                                        message.Body);
        }
    }
}
