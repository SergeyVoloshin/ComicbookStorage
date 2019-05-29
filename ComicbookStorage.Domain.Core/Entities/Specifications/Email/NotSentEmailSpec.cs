

namespace ComicbookStorage.Domain.Core.Entities.Specifications.Email
{
    using System;
    using System.Linq.Expressions;
    using LinqSpecs;
    using Entities;

    public class NotSentEmailSpec : Specification<Email>
    {
        private readonly int errorResendIntervalMinutes;

        public NotSentEmailSpec(int errorResendIntervalMinutes)
        {
            this.errorResendIntervalMinutes = errorResendIntervalMinutes;
        }

        public override Expression<Func<Email, bool>> ToExpression()
        {
            return e => e.Status == EmailStatus.Created ||
                        e.Status == EmailStatus.Sending && (DateTime.Now - e.LastSendingAttemptTime.Value).TotalMinutes > errorResendIntervalMinutes;
        }
    }
}
