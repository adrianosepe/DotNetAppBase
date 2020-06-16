using System;
using System.Threading;
using System.Threading.Tasks;
using Cronos;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace DotNetAppBase.Std.Worker.Crons
{
    public abstract class CronHostedService : IHostedService, IDisposable
    {
        private readonly string _name;
        private readonly string _cronExpression;
        private readonly CronExpression _expression;
        private readonly TimeZoneInfo _timeZoneInfo;
        private readonly ILogger _logger;

        private System.Timers.Timer _timer;

        protected CronHostedService(string name, string cronExpression, TimeZoneInfo timeZoneInfo, ILoggerFactory loggerFactory)
        {
            _name = name;
            _cronExpression = cronExpression;

            _expression = CronExpression.Parse(cronExpression);
            _timeZoneInfo = timeZoneInfo;

            _logger = loggerFactory.CreateLogger($"{GetType().Name}#{_name}");
        }

        public string Name => _name;

        protected ILogger Logger => _logger;

        public virtual async Task StartAsync(CancellationToken cancellationToken)
        {
            Logger.LogInformation($"Service starting (Cron: {_cronExpression})");

            await ScheduleJob(cancellationToken);
        }

        protected virtual async Task ScheduleJob(CancellationToken cancellationToken)
        {
            var next = _expression.GetNextOccurrence(DateTimeOffset.Now, _timeZoneInfo);
            if (next.HasValue)
            {
                var delay = next.Value - DateTimeOffset.Now;

                _timer = new System.Timers.Timer(delay.TotalMilliseconds);
                _timer.Elapsed += async (sender, args) =>
                {
                    _timer.Dispose();
                    _timer = null;

                    while (!cancellationToken.IsCancellationRequested)
                    {
                        var result = await DoWork(cancellationToken);
                        if (result.Success || result.RetryTimeSpan == null)
                        {
                            break;
                        }
                    }

                    if (!cancellationToken.IsCancellationRequested)
                    {
                        await ScheduleJob(cancellationToken);
                    }
                };

                _timer.Start();

                Logger.LogWarning($"Next occurrence at {next.Value:dd/MM/yyyy hh:mm:ss}");
            }

            await Task.CompletedTask;
        }

        public abstract Task<Result> DoWork(CancellationToken cancellationToken);

        public virtual async Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Stop();

            await Task.CompletedTask;
        }

        public virtual void Dispose() => _timer?.Dispose();

        public class Result
        {
            public bool Success { get; set; }

            public TimeSpan? RetryTimeSpan { get; set; }
        }
    }
}