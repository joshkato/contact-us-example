using System;
using ContactUsExample.Models;
using LiteDB;

namespace ContactUsExample.Data
{
    public interface IContactUsMessageRepository
    {
        Guid SaveMessage(ContactUsMessage message);
    }

    public class ContactUsMessagesRepository : IContactUsMessageRepository
    {
        private ILiteDatabase Db { get; }

        public ContactUsMessagesRepository(ILiteDatabase db)
        {
            Db = db;
        }

        public Guid SaveMessage(ContactUsMessage message)
        {
            var messages = Db.GetCollection<ContactUsMessage>();
            messages.Insert(message.Id, message);

            return message.Id;
        }
    }
}
