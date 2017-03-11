using System;
using System.Runtime.Loader;
using System.Threading;
using Reshelf.Logging;

namespace Reshelf
{
    internal class ServiceController : IDisposable
    {
        private readonly ManualResetEvent _manualResetEvent = new ManualResetEvent(false);
        private readonly ILogger _logger = LogManager.GetLogger<ServiceController>();
        private readonly IServiceConfigurator _configurator;
        private bool _serviceInstanceDisposed;

        public ServiceController(IServiceConfigurator configurator)
        {
            _configurator = configurator;

            Console.CancelKeyPress += ConsoleOnCancelKeyPress;
            AssemblyLoadContext.Default.Unloading += DefaultOnUnloading;
        }

        public void Start()
        {
            try
            {
                _logger.Debug("Service starting");
                _configurator.Start();
                _logger.Info("Service started");
                _manualResetEvent.WaitOne();
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("An unhandled exception occurred while starting service {0}", ex);
                _logger.Error("An unhandled exception occurred while starting service", ex);
            }
        }

        private void ConsoleOnCancelKeyPress(object sender, ConsoleCancelEventArgs consoleCancelEventArgs)
        {
            _logger.Debug("SIGINT received");

            Shutdown();
        }

        private void DefaultOnUnloading(AssemblyLoadContext assemblyLoadContext)
        {
            _logger.Debug("SIGTERM received");

            Shutdown();
        }

        private void Shutdown()
        {
            try
            {
                _logger.Debug("Service stopping");
                _configurator?.Stop().Wait();
                _logger.Info("Service stopped");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("An unhandled exception occurred while stopping service {0}", ex);
                _logger.Error("An unhandled exception occurred while stopping service", ex);
            }
            finally
            {
                DisposeService();
            }

            _manualResetEvent.Set();
        }

        private void DisposeService()
        {
            if (_serviceInstanceDisposed) return;

            _configurator?.Dispose();
            _serviceInstanceDisposed = true;
            _logger.Debug("Service disposed");
        }

        private bool _disposed;
        public void Dispose()
        {
            if (_disposed) return;

            Console.CancelKeyPress -= ConsoleOnCancelKeyPress;
            AssemblyLoadContext.Default.Unloading -= DefaultOnUnloading;
            _disposed = true;
        }
    }
}