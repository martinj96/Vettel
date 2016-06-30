using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transpo.AppServices.DTOs;
using Transpo.Infrastructure.Data.Entities;
using Transpo.Infrastructure.Data.Interfaces;

namespace Transpo.AppServices
{
    public class MessageService
    {
        private IMessageRepository _messageRepository;
        private IUserRepository _userRepository;

        public MessageService(IMessageRepository messageRepository, IUserRepository userRepository)
        {
            this._messageRepository = messageRepository;
            this._userRepository = userRepository;
        }

        public int GetUnreadMessagesCount(int recipientId)
        {
            return _messageRepository.GetUnreadMessagesCount(recipientId);
        }

        public void SendMessage(MessageDto msgDto)
        {
            Message msg = new Message();
            msg.MessageBody = msgDto.MessageBody;
            msg.SenderId = msgDto.SenderUserId;
            msg.Sender = _userRepository.GetById(msgDto.SenderUserId);
            msg.Subject = msgDto.Subject;
            msg.RecipientId = msgDto.RecipientUserId;
            msg.Recipient = _userRepository.GetById(msgDto.RecipientUserId);
            msg.IsRead = false;

            _messageRepository.Add(msg);
            _messageRepository.Save();
        }

        public IEnumerable<Message> GetMessagesBetweenUsers(int recipientUserId, int senderUserId, bool markAsRead = false)
        {
            var msgs = _messageRepository.GetMessagesBetweenUsers(recipientUserId, senderUserId);

            if (markAsRead)
            {
                _messageRepository.MarkMessagesAsRead(msgs);
            }

            return msgs;
        }

        public IEnumerable<Message> GetMessagesForUser(int recipientUserId)
        {
            var msgs = _messageRepository.GetMessagesForUser(recipientUserId);
            return msgs;
        }
    }
}
