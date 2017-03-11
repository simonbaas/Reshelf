using System;
using System.Threading.Tasks;

namespace Reshelf
{
    public class ServiceConfigurator<T> : IServiceConfigurator where T : class
    {
        internal Func<T> ConstructFunc { get; set; }
        internal Func<T, Task> Start { get; set; }
        internal Func<T, Task> Stop { get; set; }

        private T _instance;
        private bool _isStarted;

        async Task IServiceConfigurator.Start()
        {
            if (ConstructFunc == null) throw new InvalidOperationException("Service cannot be constructed!");
            if (Start == null) throw new InvalidOperationException("Service cannot be started!");

            if (_isStarted || _instance != null) return;

            _instance = ConstructFunc();

            _isStarted = true;

            await Start(_instance);
        }

        async Task IServiceConfigurator.Stop()
        {
            if (Stop == null) return;

            if (!_isStarted || _instance == null) return;

            await Stop(_instance);

            _isStarted = false;
        }

        private bool _disposed;
        void IDisposable.Dispose()
        {
            if (_disposed) return;

            (_instance as IDisposable)?.Dispose();
            _instance = null;
            _disposed = true;
        }
    }
}