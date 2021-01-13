using System.Net.Mail;
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
            Log.LogDebug("Validating incoming message...");
            var validationError = ValidateMessage(message);
            if (validationError != null)
                return validationError;

            Log.LogDebug("Saving message...");
            // TODO: Implement actually saving the message.

            await Task.CompletedTask.ConfigureAwait(false);
            return null;
        }

        private IServiceError ValidateMessage(ContactUsMessage message)
        {
            if (string.IsNullOrWhiteSpace(message.FirstName))
                return new ServiceError($"Required field \"{nameof(ContactUsMessage.FirstName)}\" has not been populated.");

            if (string.IsNullOrWhiteSpace(message.LastName))
                return new ServiceError($"Required field \"{nameof(ContactUsMessage.LastName)}\" has not been populated.");

            if (string.IsNullOrWhiteSpace(message.Message))
                return new ServiceError($"Required field \"{nameof(ContactUsMessage.Message)}\" has not been populated.");

            if (string.IsNullOrWhiteSpace(message.Email))
                return new ServiceError($"Required field \"{nameof(ContactUsMessage.Email)}\" has not been populated.");

            if (!MailAddress.TryCreate(message.Email, out _))
                return new ServiceError($"Provided email address {message.Email} is not a valid email address.");

            return null;
        }
    }
}
