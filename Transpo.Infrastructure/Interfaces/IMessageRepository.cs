using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transpo.Infrastructure.Data.Entities;

namespace Transpo.Infrastructure.Data.Interfaces
{
    public interface IMessageRepository : IBaseRepository<Message>
    {
        IEnumerable<Message> GetMessagesBetweenUsers(int recipientId, int senderId);

        IEnumerable<Message> GetMessagesForUser(int recipientId);
    }
}
