

namespace ComicbookStorage.Domain.Core.Entities
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using Attributes;
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

        public Email CreateEmail(string recipient, params IEntity[] entities)
        {
            return CreateEmail(recipient, null, entities);
        }

        public Email CreateEmail(string recipient, IReadOnlyDictionary<string, string> emailFields, params IEntity[] entities)
        {
            var subject = new StringBuilder(Subject);
            var body = new StringBuilder(Body);

            foreach (var entity in entities)
            {
                Type entityType = entity.GetType();
                var properties = entityType.GetProperties()
                    .Where(prop => prop.GetCustomAttribute<NotPubliclyAvailableAttribute>() == null);

                foreach (var property in properties)
                {
                    var key = $"{{{entityType.Name}.{property.Name}}}";
                    var value = (property.GetValue(entity) ?? string.Empty).ToString();
                    subject.Replace(key, value);
                    body.Replace(key, value);
                }
            }

            if (emailFields != null)
            {
                foreach (var (key, value) in emailFields)
                {
                    subject.Replace(key, value);
                    body.Replace(key, value);
                }
            }
            
            return new Email(recipient, subject.ToString(), body.ToString(), Id);
        }
    }
}
