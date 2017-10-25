using ArchitectureTemplate.Infrastructure.WCF.Client.ServiceReferenceDefault;
using ArchitectureTemplate.Infrastructure.WCF.Client.ServiceReferenceProfile;
using ArchitectureTemplate.Infrastructure.WCF.Client.ServiceReferenceScreens;
using ArchitectureTemplate.Infrastructure.WCF.Proxies;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using ProfileContract = ArchitectureTemplate.Infrastructure.WCF.Contracts.Entities.ProfileContract;
using ScreenContract = ArchitectureTemplate.Infrastructure.WCF.Contracts.Entities.ScreenContract;

namespace ArchitectureTemplate.Infrastructure.WCF.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            GetDefault();
            GetWithTcp();
            GetWithTcpProfile();
            GetWithHttp();
        }


        private static void GetDefault()
        {
            ServiceReferenceDefault.ServiceContractClient proxy = new ServiceContractClient();

            var data = proxy.GetByName("Usuario");

            proxy.Close();
        }


        private static void GetWithTcp()
        {
            //ScreenClient proxy = new ScreenClient("tcpEp");
            //var data = proxy.GetByName("Usuario");

            ScreenServiceContractClient proxy = new ScreenServiceContractClient();

            ScreenContract data = proxy.GetByName("Usuario");

            proxy.Close();
        }

        private static void GetWithTcpProfile()
        {
            ProfileServiceContractClient proxy = new ProfileServiceContractClient();

            ProfileContract data = proxy.GetByName("Administrator");

            proxy.Close();
        }

        private static void GetWithHttp()
        {
            EndpointAddress address = new EndpointAddress("net.tcp://localhost:8009/WcfService");
            Binding binding = new NetTcpBinding();

            ScreenClient proxy = new ScreenClient(binding, address);
            var data = proxy.GetScreens("T");
            var teste = data.ToList();

            proxy.Close();
        }
    }
}