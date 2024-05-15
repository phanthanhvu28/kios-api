namespace VELA.WebCoreBase.Libraries.Extensions;

public static class TaskExtension
{
    public static void OnException(this Task task)
    {
        task.ContinueWith(_ =>
            {
                if (_.Exception?.InnerException is { } exception)
                {

                }
            },
            TaskContinuationOptions.OnlyOnFaulted);
    }
}