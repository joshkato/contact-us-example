using System.Diagnostics;

namespace ContactUsExample.Models
{
    [DebuggerDisplay("{" + nameof(Message) + ",nq}")]
    public class ContactUsMessage
    {
        public string FirstName { get; set; } = "";

        public string LastName { get; set; } = "";

        public string Email { get; set; } = "";

        public string Message { get; set; }
    }
}
