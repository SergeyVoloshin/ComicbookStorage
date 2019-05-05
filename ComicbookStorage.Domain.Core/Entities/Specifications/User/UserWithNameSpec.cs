
namespace ComicbookStorage.Domain.Core.Entities.Specifications.User
{
    using System;
    using System.Linq.Expressions;
    using LinqSpecs;
    using Entities;

    public class UserWithNameSpec : Specification<User>
    {
        public UserWithNameSpec(string name)
        {
            Name = name;
        }

        public string Name { get; }

        public override Expression<Func<User, bool>> ToExpression()
        {
            return u => u.Name.Trim().ToLower() == Name.Trim().ToLower();
        }
    }
}
