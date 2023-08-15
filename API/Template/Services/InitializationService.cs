namespace Template.Services
{
    public class InitializationService : IHostedService
    {
        readonly MainService _MainService;

        public InitializationService(MainService mainService)
        {
            _MainService = mainService;
        }

        public async Task StartAsync(CancellationToken ct)
        {
            await _MainService.StartAsync(ct);
        }

        public async Task StopAsync(CancellationToken ct)
        {
            await _MainService.StopAsync(ct);
        }
    }
}
