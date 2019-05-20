
namespace ComicbookStorage.Domain.Core.Entities.Base
{
    using Attributes;

    public abstract class Entity : IEntity
    {
        protected Entity()
        {
        }

        protected Entity(int id)
        {
            Id = id;
        }

        [NotPubliclyAvailable]
        public int Id { get; private set; }
    }
}
