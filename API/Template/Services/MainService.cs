namespace Template.Services;

public class MainService
{
    readonly ILogger<MainService> _Logger;

    public MainService(ILogger<MainService> logger)
    {
        _Logger = logger;
    }

    public async Task StartAsync(CancellationToken ct)
    {
        _Logger.LogInformation("Starting Server...");

        _Logger.LogInformation("Server Started.");
    }

    public async Task StopAsync(CancellationToken ct)
    {
        _Logger.LogInformation("Server Shutting Down...");
        
        _Logger.LogInformation("Server Shut Down.");
    }
}