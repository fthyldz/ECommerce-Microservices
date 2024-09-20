using Polly;
using Polly.Retry;

namespace Notifications.OrderCreatedConsumer.Extensions;

public static class PollyExtensions
{
    public static void AddPollyResilience(this IServiceCollection services)
    {
        services.AddSingleton<AsyncPolicy>(CreateWaitAndRetryPolicy(new[]
        {
            TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(5), TimeSpan.FromSeconds(10), TimeSpan.FromSeconds(15)
        }));
    }        

    public static AsyncRetryPolicy CreateWaitAndRetryPolicy(IEnumerable<TimeSpan> sleepsBeetweenRetries)
    {
        return Policy
            .Handle<Exception>()
            .WaitAndRetryAsync(
                sleepDurations: sleepsBeetweenRetries,
                onRetry: (ex, span, retry, _) =>
                {
                    Console.WriteLine(ex);

                    var backgroundColor = Console.BackgroundColor;
                    var foregroundColor = Console.ForegroundColor;

                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.ForegroundColor = ConsoleColor.Black;

                    Console.Out.WriteLineAsync($" {DateTime.Now:HH:mm:ss} | Retry: {retry} | Awaited: {span.TotalSeconds} ");

                    Console.BackgroundColor = backgroundColor;
                    Console.ForegroundColor = foregroundColor;
                });
    }
}