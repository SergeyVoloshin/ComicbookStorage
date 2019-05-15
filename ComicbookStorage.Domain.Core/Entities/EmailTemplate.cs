

namespace ComicbookStorage.Domain.Core.Entities
{
    using System.Collections.Generic;
    using System.Text;
    using Base;

    public class EmailTemplate : Entity
    {
        public EmailTemplate(EmailTemplateId id, string subject, string body) : this((int)id, subject, body)
        {
        }

        private EmailTemplate(int id, string subject, string body) : base(id)
        {
            Subject = subject;
            Body = body;
        }

        public string Subject { get; private set; }

        public string Body { get; private set; }

        public Email CreateEmail(string recipient, IReadOnlyDictionary<string, string> fields)
        {
            var subject = new StringBuilder(Subject);
            var body = new StringBuilder(Body);
            foreach (var (key, value) in fields)
            {
                subject.Replace(key, value);
                body.Replace(key, value);
            }
            return new Email(recipient, subject.ToString(), body.ToString());
        }
    }
}
