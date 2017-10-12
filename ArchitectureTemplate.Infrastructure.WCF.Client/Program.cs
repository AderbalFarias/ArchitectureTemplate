using ArchitectureTemplate.Infrastructure.WCF.Proxies;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace ArchitectureTemplate.Infrastructure.WCF.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            GetWithTcp();
        }

        private static void GetWithTcp()
        {
            TelaClient proxy = new TelaClient("tcpEP");
            var data = proxy.GetByName("Usuario");

            proxy.Close();
        }

        private static void GetWithHttp()
        {
            EndpointAddress address = new EndpointAddress("net.tcp://localhost:8009/TelaService");
            Binding binding = new NetTcpBinding();

            TelaClient proxy = new TelaClient(binding, address);
            var data = proxy.GetTelas("T");
            var teste = data.ToList();

            proxy.Close();
        }

    }
}