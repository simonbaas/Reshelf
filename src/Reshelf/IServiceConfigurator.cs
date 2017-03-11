using System;
using System.Threading.Tasks;

namespace Reshelf
{
    internal interface IServiceConfigurator : IDisposable
    {
        Task Start();
        Task Stop();
    }
}