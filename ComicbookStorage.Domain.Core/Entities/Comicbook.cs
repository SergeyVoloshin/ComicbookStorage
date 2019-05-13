
namespace ComicbookStorage.Domain.Core.Entities
{
    using Base;

    public class Comicbook : Entity, IAggregateRoot
    {
        public Comicbook(string name, string description, string coverExtension)
        {
            Name = name;
            Description = description;
            CoverExtension = coverExtension;
        }

        public string Name { get; private set; }

        public string Description { get; private set; }

        public string CoverExtension { get; private set; }

        public string SeoUrl { get; private set; }
    }
}
