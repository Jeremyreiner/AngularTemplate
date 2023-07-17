using System.Timers;
using Template.Shared.Interfaces.IServices;

namespace Template.Services
{
    public class BackGroundService
    {
        private static System.Timers.Timer _DailyTimer;

        readonly IServiceProvider _ServiceProvider;

        private readonly ILogger<BackGroundService> _Logger;

        private CancellationToken _Ct;

        private bool _InvoiceEventsCompleted;

        public BackGroundService(ILogger<BackGroundService> logger, IServiceProvider serviceProvider)
        {
            _Logger = logger;
            _ServiceProvider = serviceProvider;
        }

        public void InitializeDailyTimer(int interval, CancellationToken ct)
        {
            _Ct = ct;

            _DailyTimer = new(interval);

            InvoiceEventHandler(true);

            _DailyTimer.Start();
        }

        public Task ShutDownAsync(CancellationToken ct)
        {
            _DailyTimer.Dispose();

            return Task.CompletedTask;
        }

        public int MinuteTimer() => 60000;

        private void InvoiceEventHandler(bool addEvent)
        {
            if (addEvent)
            {
                _DailyTimer.Elapsed += async (sender, e) => await InvoiceEventManagerAsync(sender, e);

                _InvoiceEventsCompleted = false;
            }
            else
            {
                _DailyTimer.Elapsed -= async (sender, e) => await InvoiceEventManagerAsync(sender, e);

                _InvoiceEventsCompleted = true;
            }
        }

        private async Task InvoiceEventManagerAsync(object sender, ElapsedEventArgs e)
        {
            try
            {
                using var scope = _ServiceProvider.CreateScope();

                var dalService = scope.ServiceProvider.GetRequiredService<IDalService>();

                await dalService.InvoiceTimeEventManagerAsync(_Ct);

                scope.Dispose();
            }
            catch (Exception ex)
            {
                _Logger.LogError(ex.ToString());
            }

            InvoiceEventHandler(false);

            TimerHandler(_Ct);
        }

        private void TimerHandler(CancellationToken ct)
        {
            if (!AllEventsCompleted()) return;

            _DailyTimer.Dispose();

            InitializeDailyTimer(MinuteTimer(), ct);
        }

        private bool AllEventsCompleted() => _InvoiceEventsCompleted;
    }
}
