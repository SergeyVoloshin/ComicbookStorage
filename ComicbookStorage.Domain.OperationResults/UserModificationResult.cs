
namespace ComicbookStorage.Domain.OperationResults
{
    public enum UserModificationResult
    {
        NothingToUpdate = 0,

        SuccessConfirmationRequired,

        SuccessNoConfirmationRequired,

        DuplicateValues,

        IncorrectPassword,
    }
}
