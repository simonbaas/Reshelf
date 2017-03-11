using System;
using Reshelf.Logging;

namespace Reshelf.Extensions
{
    public static class ServiceConfigurationExtensions
    {
        public static void Use(this ServiceConfiguration config, ILoggerFactory loggerFactory)
        {
            LogManager.LoggerFactory = loggerFactory ?? throw new ArgumentNullException(nameof(loggerFactory), "A valid LoggerFactory must be supplied");
        }

        public static void Service<T>(this ServiceConfiguration config, Action<ServiceConfigurator<T>> callback) where T : class
        {
            if (callback == null)
            {
                throw new ArgumentNullException(nameof(callback), "A valid configuration callback must be supplied");
            }

            var configurator = new ServiceConfigurator<T>();

            callback(configurator);

            config.ServiceConfigurator = configurator;
        }
    }
}
