using System;
using System.Diagnostics;

namespace ContactUsExample.Models
{
    public interface IServiceError
    {
        string Message { get; }

        Exception Exception { get; }

        public void Deconstruct(out string message, out Exception exception) =>
            (message, exception) = (Message, Exception);
    }

    [DebuggerDisplay("{" + nameof(Message) + ",nq}")]
    public class ServiceError : IServiceError
    {
        public string Message { get; set; }

        public Exception Exception { get; set; }

        public ServiceError()
        {
        }

        public ServiceError(string message)
        {
            Message = message;
        }

        public ServiceError(string message, Exception exception)
        {
            Message = message;
            Exception = exception;
        }
    }
}
