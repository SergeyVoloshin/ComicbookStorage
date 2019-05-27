
namespace ComicbookStorage.Infrastructure.EmailSender
{
    public interface IEmailSenderSettings
    {
        string MailServer { get; set; }
        int MailPort { get; set; }
        string SenderName { get; set; }
        string Sender { get; set; }
        string Password { get; set; }
        int MaxEmailCount { get; set; }
        int ResendIntervalMinutes { get; set; }
    }

    public class EmailSenderSettings : IEmailSenderSettings
    {
        public string MailServer { get; set; }
        public int MailPort { get; set; }
        public string SenderName { get; set; }
        public string Sender { get; set; }
        public string Password { get; set; }
        public int MaxEmailCount { get; set; }
        public int ResendIntervalMinutes { get; set; }
    }
}
