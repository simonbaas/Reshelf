using Autofac;
using Reshelf.Extensions;

namespace Reshelf.Autofac
{
    public static class AutofacServiceConfiguratorExtension
    {
        public static void UseAutofac<T>(this ServiceConfigurator<T> serviceConfigurator, IContainer container)
            where T : class
        {
            serviceConfigurator.ConstructUsing(container.Resolve<T>);
        }
    }
}
