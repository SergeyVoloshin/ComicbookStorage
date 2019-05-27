
namespace ComicbookStorage.Domain.Core.Entities
{
    using System;
    using Base;

    public class Email : Entity, IAggregateRoot
    {
        public Email(string recipient, string subject, string body, int? emailTemplateId = null) 
            : this(recipient, subject, body, DateTime.Now, emailTemplateId)
        {
            Status = EmailStatus.Created;
        }

        private Email(string recipient, string subject, string body, DateTime creationTime, int? emailTemplateId)
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

        public DateTime? LastSendingAttemptTime { get; private set; }

        public string LastSendingError { get; private set; }

        public int? EmailTemplateId { get; private set; }

        public EmailStatus Status { get; private set; }

        public void SetSendingStatus()
        {
            Status = EmailStatus.Sending;
            LastSendingAttemptTime = DateTime.Now;
        }

        public void SetErrorStatus(string error)
        {
            Status = EmailStatus.Error;
            LastSendingError = error;
        }

        public void SetSentStatus()
        {
            Status = EmailStatus.Sent;
        }
    }
}
