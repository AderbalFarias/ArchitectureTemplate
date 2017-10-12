using System.ComponentModel;

namespace ArchitectureTemplate.Infrastructure.WindowService.HearSomething
{
    [RunInstaller(true)]
    public partial class ProjectInstaller : System.Configuration.Install.Installer
    {
        public ProjectInstaller()
        {
            InitializeComponent();
        }
    }
}
