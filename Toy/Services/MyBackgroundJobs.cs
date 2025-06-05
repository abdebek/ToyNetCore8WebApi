public class MyBackgroundJobs
{
    private readonly ILogger<MyBackgroundJobs> _logger;

    // Example of injecting a dependency if needed
    public MyBackgroundJobs(ILogger<MyBackgroundJobs> logger)
    {
        _logger = logger;
    }

    public void PrintHiJob()
    {
        _logger.LogInformation("Hi from a RecurringJob inside MyBackgroundJobs class!");
    }

    // Async job method
    public async Task AnotherJobMethod(string message)
    {
        _logger.LogInformation($"Starting AnotherJobMethod with message: {message}. Will delay for 10 seconds.");
        await Task.Delay(TimeSpan.FromSeconds(10)); // Simulating async work
        _logger.LogInformation($"Another job executed with message: {message}, after 10 secs delay.");
    }

    // Another example of an async job that returns a value (though Hangfire doesn't directly use the return value of a top-level job)
    public async Task<string> GetDataAsync(int id)
    {
        _logger.LogInformation($"GetDataAsync called with id: {id}. Simulating data fetch...");
        await Task.Delay(TimeSpan.FromSeconds(2)); // Simulate I/O bound operation
        var result = $"Data for ID {id}";
        _logger.LogInformation($"GetDataAsync completed for id: {id}. Result: {result}");
        return result;
    }
}