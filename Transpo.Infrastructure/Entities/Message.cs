using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transpo.Infrastructure.Data.Entities
{
    public class Message : BaseEntity
    {
        public string Subject { get; set; }
        public string MessageBody { get; set; }
        public bool IsRead { get; set; }
        public int SenderId { get; set; }
        public int RecipientId { get; set; }
        public virtual User Sender { get; set; }
        public virtual User Recipient { get; set; }
    }
}
