﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transpo.Infrastructure.Data.Entities;
using Transpo.Infrastructure.Data.Interfaces;

namespace Transpo.Infrastructure.Data.Repositories
{
    public class MessageRepository : BaseRepository<Message>, IMessageRepository
    {
        public MessageRepository(TranspoDbContext context)
            : base(context)
        {

        }

        public IEnumerable<Message> GetMessagesBetweenUsers(int recipientId, int senderId)
        {
            using (TranspoDbContext ctx = new TranspoDbContext(DAUtilities.ConnectionString))
            {
                var msgs = (from m in _context.Messages
                            where (m.Active == true &&
                            ( (m.RecipientId == recipientId && m.SenderId == senderId)
                            || (m.RecipientId == senderId && m.SenderId == recipientId)) )
                            select m).OrderBy(m => m.DateCreated);
                return msgs.ToList();
            }
        }

        public IEnumerable<Message> GetMessagesForUser(int recipientId)
        {
            using (TranspoDbContext ctx = new TranspoDbContext(DAUtilities.ConnectionString))
            {
                var msgs = (from m in ctx.Messages
                             where (m.RecipientId == recipientId || m.SenderId == recipientId)
                             let otherId = (m.RecipientId == recipientId) ? m.SenderId : m.RecipientId
                             group m by otherId into g
                             select g.OrderByDescending(m => m.DateCreated).FirstOrDefault());

                return msgs.ToList();
            }
        }

        public int GetUnreadMessagesCount(int recipientId)
        {
            using (TranspoDbContext ctx = new TranspoDbContext(DAUtilities.ConnectionString))
            {
                var count = (from m in ctx.Messages
                             where (m.RecipientId == recipientId && m.IsRead == false && m.Active == true)
                             select m).Count();
                return count;
            }
        }

        public void MarkMessagesAsRead(IEnumerable<Message> msgs)
        {
            foreach (var msg in msgs)
            {
                if (msg.IsRead == false)
                {
                    _context.Entry(msg).State = System.Data.Entity.EntityState.Modified;
                    _context.Entry(msg).Entity.IsRead = true;
                }
            }
            _context.SaveChanges();
        }
    }
}