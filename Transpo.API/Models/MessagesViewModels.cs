using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Transpo.Infrastructure.Data.Entities;

namespace Transpo.WebApp.Models
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

        public MessageViewModel()
        {

        }

        public MessageViewModel(Message msg)
        {
            SenderId = msg.SenderId;
            RecipientId = msg.RecipientId;
            Body = msg.MessageBody;
            Subject = msg.Subject;
            Recipient = new UserViewModel(msg.Recipient);
            Sender = new UserViewModel(msg.Sender);
            SendDate = msg.DateCreated;
        }

    }
}