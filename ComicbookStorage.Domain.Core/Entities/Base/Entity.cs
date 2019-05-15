
namespace ComicbookStorage.Domain.Core.Entities.Base
{
    public abstract class Entity : IEntity
    {
        protected Entity()
        {
        }

        protected Entity(int id)
        {
            Id = id;
        }

        public int Id { get; private set; }
    }
}
