
namespace ComicbookStorage.Domain.Core.Entities
{
    using Base;

    public class Email : Entity
    {
        public Email(string recipient, string subject, string body)
        {
            Recipient = recipient;
            Subject = subject;
            Body = body;
        }

        public string Recipient { get; private set; }

        public string Subject { get; private set; }

        public string Body { get; private set; }
    }
}
