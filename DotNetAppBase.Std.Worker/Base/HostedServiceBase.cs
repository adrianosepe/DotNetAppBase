using System;
using System.Threading;
using System.Threading.Tasks;
using DotNetAppBase.Std.Exceptions.Contract;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace DotNetAppBase.Std.Worker.Base
{
    /// <summary>
    ///     Base class for implementing a long running <see cref="IHostedService" />.
    /// </summary>
    public abstract class HostedServiceBase : IHostedService, IDisposable
    {
        private bool _initialized;
        private string _name;
        private ILogger _logger;

        private bool _enabled;
        private bool _disposed;

        private Task _executingTask;
        private CancellationTokenSource _stoppingCts;

        protected HostedServiceBase() { }

        public bool Enabled => _enabled;

        public bool IsInitialized => _initialized;

        protected ILogger Logger => _logger;

        public string Name => _name;

        internal void Initialize(string name, ILoggerFactory loggerFactory, IConfigurationSection settingSection)
        {
            if (_initialized)
            {
                XInitializeException.Reinitialize(this);
            }

            _initialized = true;

            _name = name;
            _logger = loggerFactory.CreateLogger($"#{_name}");

            if (settingSection != null)
            {
                InternalInitializeFromSettingsSection(settingSection);
            }
        }

        /// <summary>
        ///     Triggered when the application host is ready to start the service.
        /// </summary>
        /// <param name="cancellationToken">Indicates that the start process has been aborted.</param>
        public virtual Task StartAsync(CancellationToken cancellationToken)
        {
            if (_enabled)
            {
                return Task.CompletedTask;
            }

            _enabled = true;

            // Create linked token to allow cancelling executing task from provided token
            _stoppingCts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);

            _logger.LogInformation("Service is started.");

            // Store the task we're executing
            _executingTask = ExecuteAsync(_stoppingCts.Token);

            // If the task is completed then return it, this will bubble cancellation and failure to the caller
            if (_executingTask.IsCompleted)
            {
                return _executingTask;
            }

            // Otherwise it's running
            return Task.CompletedTask;
        }

        /// <summary>
        ///     Triggered when the application host is performing a graceful shutdown.
        /// </summary>
        /// <param name="cancellationToken">Indicates that the shutdown process should no longer be graceful.</param>
        public virtual async Task StopAsync(CancellationToken cancellationToken)
        {
            if (!_enabled)
            {
                return;
            }

            _enabled = false;

            _logger.LogInformation("Service is stoping.");

            // Stop called without start
            if (_executingTask == null)
            {
                return;
            }

            try
            {
                // Signal cancellation to the executing method
                _stoppingCts.Cancel();
            }
            finally
            {
                // Wait until the task completes or the stop token triggers
                await Task.WhenAny(
                    _executingTask.ContinueWith(InternalCleanup, cancellationToken), Task.Delay(Timeout.Infinite, cancellationToken));
            }

            _logger.LogInformation("Service is stoped.");
        }

        protected virtual Task InternalCleanup(Task obj) => Task.CompletedTask;

        /// <summary>
        ///     This method is called when the <see cref="IHostedService" /> starts. The implementation should return a task that
        ///     represents
        ///     the lifetime of the long running operation(s) being performed.
        /// </summary>
        /// <param name="cancellationToken">Triggered when <see cref="IHostedService.StopAsync(CancellationToken)" /> is called.</param>
        /// <returns>A <see cref="Task" /> that represents the long running operations.</returns>
        protected abstract Task ExecuteAsync(CancellationToken cancellationToken);

        protected virtual void InternalInitializeFromSettingsSection(IConfigurationSection settingSection) { }

        #region Dispose Pattern

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            _disposed = true;

            _stoppingCts?.Cancel();

            if (disposing)
            {
                GC.SuppressFinalize(this);
            }

            InternalDisposing(disposing);
        }

        protected virtual void InternalDisposing(in bool disposing) { }

        ~HostedServiceBase()
        {
            Dispose(false);
        }

        #endregion
    }
}