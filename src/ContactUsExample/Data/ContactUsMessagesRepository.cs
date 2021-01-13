using System;
using System.Threading.Tasks;
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
            return Guid.Empty;
        }
    }
}
