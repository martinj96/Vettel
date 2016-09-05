using System;

namespace Transpo.Models
{
    public class MessagesViewModels
    {
    }

    public class MessageViewModel
    {
        public int RecipientId { get; set; }
        public int SenderId { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public UserViewModel Recipient { get; set; }
        public UserViewModel Sender { get; set; }
        public DateTime SendDate { get; set; }

    }
}