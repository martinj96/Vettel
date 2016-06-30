using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transpo.AppServices.DTOs
{
    public class MessageDto
    {
        public int SenderUserId { get; set; }
        public int RecipientUserId { get; set; }
        public string Subject { get; set; }
        public string MessageBody { get; set; }
        public int? ParentMessageId { get; set; }
    }
}
