using System;
using System.Diagnostics;

namespace ContactUsExample.Models
{
    [DebuggerDisplay("{" + nameof(Message) + ",nq}")]
    public class ContactUsMessage
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string FirstName { get; set; } = "";

        public string LastName { get; set; } = "";

        public string Email { get; set; } = "";

        public string Message { get; set; } = "";

        public DateTime SubmittedAt { get; set; }
    }
}
