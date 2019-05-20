
namespace ComicbookStorage.Domain.Core.Entities
{
    using System;
    using Base;

    public class Email : Entity
    {
        public Email(string recipient, string subject, string body) 
            : this(recipient, subject, body, DateTime.Now)
        {
        }

        private Email(string recipient, string subject, string body, DateTime creationTime)
        {
            Recipient = recipient;
            Subject = subject;
            Body = body;
            CreationTime = creationTime;
        }

        public string Recipient { get; private set; }

        public string Subject { get; private set; }

        public string Body { get; private set; }

        public DateTime CreationTime { get; private set; }
    }
}
