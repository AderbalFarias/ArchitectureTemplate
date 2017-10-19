using ArchitectureTemplate.Infrastructure.CrossCutting.IoC;
using ArchitectureTemplate.Infrastructure.WCF.Contracts.ServiceInterfaces;
using ArchitectureTemplate.Infrastructure.WCF.Services;
using SimpleInjector;
using SimpleInjector.Integration.Wcf;
using System;
using System.ServiceModel;

namespace ArchitectureTemplate.Infrastructure.WCF.Host
{
    public class Program
    {
        static void Main(string[] args)
        {
            var container = InitializeContainer();

            //reserve url: netsh http add urlacl url = http://+:8086/ user="NAME-PC\User"
            ServiceHost serviceHost = new SimpleInjectorServiceHost(container, typeof(ScreenManager));
            serviceHost.Open();

            ServiceHost serviceHostProfile = new SimpleInjectorServiceHost(container, typeof(ProfileManager));
            serviceHostProfile.Open();

            Console.WriteLine("Services started. Press [Enter] to exit.");
            Console.ReadLine();

            serviceHost.Close();
            serviceHostProfile.Close();
        }

        private static Container InitializeContainer()
        {
            var container = new Container();

            container.Options.DefaultScopedLifestyle = new WcfOperationLifestyle(false);
            container.Register<IScreenServiceContract, ScreenManager>(Lifestyle.Scoped);
            container.Register<IProfileServiceContract, ProfileManager>(Lifestyle.Scoped);

            BootstrapperWcf.RegisterServices(container);
            container.Verify();

            return container;
        }
    }
}
