using ArchitectureTemplate.Infrastructure.WindowService.Service;
using System.ServiceProcess;

namespace ArchitectureTemplate.Infrastructure.WindowService.HearSomething
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
