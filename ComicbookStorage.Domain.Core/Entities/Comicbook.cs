
namespace ComicbookStorage.Domain.Core.Entities
{
    using Base;

    public class Comicbook : IAggregateRoot
    {
        public Comicbook(string name)
        {
            Name = name;
        }

        public int Id { get; private set; }

        public string Name { get; private set; }
    }
}
