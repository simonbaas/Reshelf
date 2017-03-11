using System;
using Reshelf.Logging;

namespace Reshelf
{
    public class ServiceFactory
    {
        private static readonly ILogger Logger = LogManager.GetLogger<ServiceFactory>();

        private ServiceFactory() { }

        public static void Start(Action<ServiceConfiguration> configure)
        {
            try
            {
                var configuration = new ServiceConfiguration();

                configure(configuration);

                using (var controller = new ServiceController(configuration.ServiceConfigurator))
                {
                    controller.Start();
                }
            }
            catch (Exception ex)
            {
                Logger.Error("An unexpected error occurred", ex);
            }
        }
    }
}
