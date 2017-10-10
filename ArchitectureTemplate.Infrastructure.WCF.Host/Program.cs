using ArchitectureTemplate.Infrastructure.WCF.Services;
using System;
using System.ServiceModel;

namespace ArchitectureTemplate.Infrastructure.WCF.Host
{
    class Program
    {


        static void Main(string[] args)
        {
            //Container container = new Container();
            //BootstrapperWcf.RegisterServices(container);

            ServiceHost hostGeoManager = new ServiceHost(typeof(TelaManager));
            hostGeoManager.Open();

            Console.WriteLine("Services started. Press [Enter] to exit.");
            Console.ReadLine();

            hostGeoManager.Close();
        }
    }
}
