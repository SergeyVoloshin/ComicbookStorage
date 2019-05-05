
namespace ComicbookStorage.Domain.Core.Entities
{
    using System;
    using Base;
    using Infrastructure.Cryptography;

    public class User : IAggregateRoot
    {
        public User(string email, string name, string password)
        {
            Email = email.Trim();
            Name = name.Trim();
            Salt = Guid.NewGuid().ToString();
            Password = GetSaltedPassword(Salt, password);
        }

        public int Id { get; private set; }

        public string Email { get; private set; }

        public string Name { get; private set; }

        public string Salt { get; private set; }

        public string Password { get; private set; }

        private string GetSaltedPassword(string salt, string password)
        {
            return HashAlgorithm.GetMd5(string.Concat(salt, HashAlgorithm.GetMd5(password)));
        }
    }
}
