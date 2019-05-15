﻿
namespace ComicbookStorage.Domain.Core.Entities
{
    using Base;
    using Infrastructure.Cryptography;

    public class User : Entity, IAggregateRoot
    {
        private const int DefaultSaltLength = 32;
        private const int DefaultHashLength = 32;
        private const int DefaultIterationCount = 64000;
        private const int DefaultConfirmationCodeLength = 64;

        public User(string email, string name, string password)
        {
            Email = email.Trim();
            Name = name.Trim();
            Salt = PasswordEncryptionProvider.GenerateSalt(DefaultSaltLength);
            EncryptionIterationCount = DefaultIterationCount;
            Password = PasswordEncryptionProvider.CreateHash(password, Salt, EncryptionIterationCount, DefaultHashLength);
            ConfirmationCode = PasswordEncryptionProvider.GenerateConfirmationCode(DefaultConfirmationCodeLength);
        }

        public string Email { get; private set; }

        public string Name { get; private set; }

        public string Salt { get; private set; }

        public int EncryptionIterationCount { get; private set; }

        public string Password { get; private set; }

        public string ConfirmationCode { get; private set; }
    }
}
