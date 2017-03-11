using Reshelf.Extensions;

namespace Reshelf.Log4Net
{
    public static class Log4NetServiceConfigurationExtension
    {
        public static void UseLog4Net(this ServiceConfiguration config)
        {
            config.Use(new Log4NetLoggerFactory());
        }
    }
}
