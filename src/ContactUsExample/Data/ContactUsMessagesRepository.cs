using System;
using System.Collections.Generic;
using ContactUsExample.Models;
using LiteDB;

namespace ContactUsExample.Data
{
    public interface IContactUsMessageRepository
    {
        List<ContactUsMessage> GetTopMessages(int count);

        Guid SaveMessage(ContactUsMessage message);
    }

    public class ContactUsMessagesRepository : IContactUsMessageRepository
    {
        private ILiteDatabase Db { get; }

        public ContactUsMessagesRepository(ILiteDatabase db)
        {
            Db = db;
        }

        public List<ContactUsMessage> GetTopMessages(int count)
        {
            var messages = Db.GetCollection<ContactUsMessage>();
            return messages.Query().OrderByDescending(m => m.SubmittedAt)
                .Limit(count)
                .ToList();
        }

        public Guid SaveMessage(ContactUsMessage message)
        {
            var messages = Db.GetCollection<ContactUsMessage>();
            messages.Insert(message.Id, message);

            return message.Id;
        }
    }
}
