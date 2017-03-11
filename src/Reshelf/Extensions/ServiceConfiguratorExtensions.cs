using System;
using System.Threading.Tasks;

namespace Reshelf.Extensions
{
    public static class ServiceConfiguratorExtensions
    {
        public static void ConstructUsing<T>(this ServiceConfigurator<T> serviceConfigurator, Func<T> func) where T : class
        {
            serviceConfigurator.ConstructFunc = func ?? throw new ArgumentNullException(nameof(func), "A valid construct function must be supplied");
        }

        public static void OnStart<T>(this ServiceConfigurator<T> serviceConfigurator, Func<T, Task> start) where T : class
        {
            serviceConfigurator.Start = start ?? throw new ArgumentNullException(nameof(start), "A valid start function must be supplied");
        }

        public static void OnStop<T>(this ServiceConfigurator<T> serviceConfigurator, Func<T, Task> stop) where T : class
        {
            serviceConfigurator.Stop = stop ?? throw new ArgumentNullException(nameof(stop), "A valid stop function must be supplied");
        }
    }
}
