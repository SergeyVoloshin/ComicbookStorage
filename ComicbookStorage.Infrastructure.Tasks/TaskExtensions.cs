
namespace ComicbookStorage.Infrastructure.Tasks
{
    using System;
    using System.Threading.Tasks;

    public static class TaskExtensions
    {
        public static Task<TOutput> Transform<TInput, TOutput>(this Task<TInput> task, Func<TInput, TOutput> transformFunction)
        {
            var tcs = new TaskCompletionSource<TOutput>();
            task.ContinueWith(t =>
            {
                if (t.IsFaulted)
                {
                    tcs.TrySetException(t.Exception.InnerExceptions);
                }
                else if (t.IsCanceled)
                {
                    tcs.TrySetCanceled();
                }
                else
                {
                    tcs.TrySetResult(transformFunction(t.Result));
                }
            }, TaskContinuationOptions.ExecuteSynchronously);
            return tcs.Task;
        }
    }
}
