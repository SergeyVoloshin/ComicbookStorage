
namespace ComicbookStorage.Domain.Core.Entities
{
    using Attributes;
    using Base;
    using Infrastructure.Cryptography;
    using System;

    public class User : Entity, IAggregateRoot
    {
        private const int DefaultSaltLength = 32;
        private const int DefaultHashLength = 32;
        private const int DefaultIterationCount = 64000;
        private const int DefaultConfirmationCodeLength = 64;
        private const int DefaultRefreshTokenLength = 64;

        private const int MinPasswordLength = 8;
        private const int MaxPasswordLength = 15;

        public User(string email, string name, string password)
        {
            Email = PrepareEmail(email);
            Name = name;
            SetPassword(password);
            GenerateConfirmationCode();
        }

        private User()
        {
        }

        public string Email { get; private set; }

        public string Name { get; set; }

        public string Salt { get; private set; }

        [NotPubliclyAvailable]
        public int EncryptionIterationCount { get; private set; }

        [NotPubliclyAvailable]
        public string Password { get; private set; }

        public string ConfirmationCode { get; private set; }

        public bool IsEmailConfirmed { get; set; }

        public string RefreshToken { get; private set; }

        public DateTime? RefreshTokenExpirationTime { get; private set; }

        public string UserAgent { get; private set; }

        public void GenerateRefreshToken(string userAgent, double lifeTimeMinutes)
        {
            RefreshToken = PasswordEncryptionProvider.GenerateRandomBase64String(DefaultRefreshTokenLength);
            RefreshTokenExpirationTime = DateTime.Now.AddMinutes(lifeTimeMinutes);
            UserAgent = userAgent;
        }

        public string ResetPassword()
        {
            Random random = new Random();
            var password = PasswordEncryptionProvider.GenerateRandomAlphanumericString(random.Next(MinPasswordLength, MaxPasswordLength));
            RefreshToken = null;
            SetPassword(password);
            return password;
        }

        public bool VerifyRefreshToken(string userAgent, string token)
        {
            return IsEmailConfirmed && RefreshTokenExpirationTime >= DateTime.Now && userAgent == UserAgent && token == RefreshToken;
        }

        public bool VerifyPassword(string password)
        {
            return IsEmailConfirmed && PasswordEncryptionProvider.VerifyPassword(password, Password, Salt, EncryptionIterationCount);
        }

        public void SetEmail(string email)
        {
            Email = PrepareEmail(email);
            IsEmailConfirmed = false;
            RefreshToken = null;
            GenerateConfirmationCode();
        }

        public void SetPassword(string password)
        {
            EncryptionIterationCount = DefaultIterationCount;
            Salt = PasswordEncryptionProvider.GenerateRandomBase64String(DefaultSaltLength);
            Password = GetEncryptedPassword(password);
        }

        private string GetEncryptedPassword(string password)
        {
            return PasswordEncryptionProvider.CreateHash(password, Salt, EncryptionIterationCount, DefaultHashLength);
        }

        private void GenerateConfirmationCode()
        {
            ConfirmationCode = PasswordEncryptionProvider.GenerateConfirmationCode(Email, DefaultConfirmationCodeLength);
        }

        private string PrepareEmail(string email)
        {
            return email.Trim().ToLower();
        }
    }
}
