

namespace ComicbookStorage.Domain.Core.Entities
{
    using Base;

    public class EmailTemplate : Entity
    {
        public string Subject { get; private set; }

        public string Body { get; private set; }
    }
}
