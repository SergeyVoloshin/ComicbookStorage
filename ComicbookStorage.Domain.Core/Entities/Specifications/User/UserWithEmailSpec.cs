
namespace ComicbookStorage.Domain.Core.Entities.Specifications.User
{
    using System;
    using System.Linq.Expressions;
    using LinqSpecs;
    using Entities;

    public class UserWithEmailSpec : Specification<User>
    {
        public UserWithEmailSpec(string email)
        {
            Email = email;
        }

        public string Email { get; }

        public override Expression<Func<User, bool>> ToExpression()
        {
            return u => u.Email.Trim().ToLower() == Email.Trim().ToLower();
        }
    }
}
