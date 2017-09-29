using System.ServiceProcess;

namespace ArchitectureTemplate.Infrastructure.WindowService.Service
{
    static class Program
    {
        static void Main()
        {
            var servicesToRun = new ServiceBase[]
            {
                new StartService()
            };

            ServiceBase.Run(servicesToRun);
        }
    }
}
