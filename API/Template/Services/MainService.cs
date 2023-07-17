namespace Template.Services;

public class MainService
{
    private readonly BackGroundService _BgService;

    readonly ILogger<MainService> _Logger;

    public MainService(ILogger<MainService> logger, BackGroundService bgService)
    {
        _Logger = logger;
        _BgService = bgService;
    }

    public async Task StartAsync()
    {
        _Logger.LogInformation("Starting Server...");

        _BgService.InitializeDailyTimer(_BgService.MinuteTimer(), new CancellationToken());

        _Logger.LogInformation("Server Started.");
    }

    public async Task StopAsync()
    {
        _Logger.LogInformation("Server Shutting Down...");

        _Logger.LogInformation("Server Shut Down.");
    }
}