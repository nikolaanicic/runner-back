using MimeKit;

namespace Contracts.Dtos.Email
{
    public class Message
    {

        public MailboxAddress To { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }

        public Message(string to, string subject, string content)
        {
            this.To = new MailboxAddress("email", to);
            this.Subject = subject;
            this.Content = content;
        }
    }
}
