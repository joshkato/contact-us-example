using System;
using System.Net.Mail;
using ContactUsExample.Data;
using ContactUsExample.Models;
using Microsoft.Extensions.Logging;

namespace ContactUsExample.Services
{
    public interface IContactUsService
    {
        IServiceError SubmitMessageAsync(ContactUsMessage message);
    }

    public class ContactUsService : IContactUsService
    {
        private ILogger Log { get; }

        private IContactUsMessageRepository ContactUsMessages { get; }

        public ContactUsService(ILogger<ContactUsService> logger, IContactUsMessageRepository contactUsMessagesRepo)
        {
            Log = logger;
            ContactUsMessages = contactUsMessagesRepo;
        }

        public IServiceError SubmitMessageAsync(ContactUsMessage message)
        {
            Log.LogDebug("[ID: {messageId}] Validating incoming message...", message.Id);
            var validationError = ValidateMessage(message);
            if (validationError != null)
                return validationError;

            try
            {
                message.SubmittedAt = DateTime.Now;

                Log.LogDebug("[ID: {messageId}] Saving message...", message.Id);
                ContactUsMessages.SaveMessage(message);
                Log.LogDebug("[ID: {messageId}] Message saved.", message.Id);
            }
            catch (Exception ex)
            {
                Log.LogError(ex, "Failed to save 'Contact Us' message with ID {id}. " +
                                 "Refer to the exception for more information.",
                    message.Id);

                return new ServiceError("Failed to save message.", ex);
            }

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
