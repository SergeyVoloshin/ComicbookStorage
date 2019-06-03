
namespace ComicbookStorage.Domain.Core.Entities.Specifications.User
{
    using System;
    using System.Linq.Expressions;
    using LinqSpecs;
    using Entities;

    public class UserWithConfirmationCode : Specification<User>
    {
        public UserWithConfirmationCode(string confirmationCode)
        {
            ConfirmationCode = confirmationCode;
        }

        public string ConfirmationCode { get; }

        public override Expression<Func<User, bool>> ToExpression()
        {
            return u => u.ConfirmationCode == ConfirmationCode;
        }
    }
}
