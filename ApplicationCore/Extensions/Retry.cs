using OneOf;
using VELA.WebCoreBase.Libraries.Exceptions;

namespace ApplicationCore.Extensions;

public static class Retry
{
    public static async Task<OneOf<T, CommonExceptionBase>> DoAsync<T>(
        Func<Task<T>> func,
        int maxRetryAttempts = 3,
        int delayMilliseconds = 300)
    {
        int retryCount = 0;
        while (retryCount < maxRetryAttempts)
        {
            try
            {
                return await func();
            }
            catch (Exception ex)
            {
                retryCount++;
                if (retryCount >= maxRetryAttempts)
                {
                    return new UnhandledException(ex.Message, ex);
                }

                await Task.Delay(TimeSpan.FromMilliseconds(delayMilliseconds + new Random().Next(100 * retryCount)));
            }
        }

        return new UnhandledException("Retry failed for an unknown reason");
    }
}