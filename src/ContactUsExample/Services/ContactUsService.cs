using System.Threading.Tasks;
using ContactUsExample.Models;
using Microsoft.Extensions.Logging;

namespace ContactUsExample.Services
{
    public interface IContactUsService
    {
        ValueTask<IServiceError> SubmitMessageAsync(ContactUsMessage message);
    }

    public class ContactUsService : IContactUsService
    {
        private ILogger Log { get; }

        public ContactUsService(ILogger<ContactUsService> logger)
        {
            Log = logger;
        }

        public async ValueTask<IServiceError> SubmitMessageAsync(ContactUsMessage message)
        {
            await Task.CompletedTask.ConfigureAwait(false);
            return null;
        }
    }
}
