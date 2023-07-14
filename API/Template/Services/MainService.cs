namespace Template.Services;

public class MainService
{
    readonly IServiceProvider _ServiceProvider;

    private readonly BackGroundService _BgService;

    readonly ILogger<MainService> _Logger;

    public MainService(IServiceProvider serviceProvider, ILogger<MainService> logger, BackGroundService bgService)
    {
        //Used to create scope for dal service 
        _ServiceProvider = serviceProvider;

        _Logger = logger;
        _BgService = bgService;
    }

    public async Task StartAsync()
    {
        _Logger.LogInformation("Starting Server...");

        _BgService.InitializeDailyTimer(60000, new CancellationToken());

        _Logger.LogInformation("Server Started.");
    }

    public async Task StopAsync()
    {
        _Logger.LogInformation("Server Shutting Down...");

        _Logger.LogInformation("Server Shut Down.");
    }
}