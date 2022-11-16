using Commisions.Business;
using Commisions.Business.Contract;
using Commisions.Business.Misc;
using Commisions.Business.Processors;
using Microsoft.Extensions.DependencyInjection;

namespace Commisions
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = new ServiceCollection()
                .AddSingleton<IProcessor, UsersDataProcessor>()
                .AddSingleton<IProcessor, TransferDataProcessor>()
                .AddSingleton<IXmlHelper, UsersXmlHelper>()
                .AddSingleton<IXmlHelper, TransfersXmlHelper>()
                .AddSingleton<IManagment, Managment>()
                .BuildServiceProvider();

            var manage = serviceProvider.GetService<IManagment>();

            manage.Run();
        }
    }
}
