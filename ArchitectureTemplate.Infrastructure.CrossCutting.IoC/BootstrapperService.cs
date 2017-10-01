using ArchitectureTemplate.Business.Interfaces.Repositories;
using ArchitectureTemplate.Business.Interfaces.Services;
using ArchitectureTemplate.Business.Services;
using ArchitectureTemplate.Infrastructure.Data.Repositories;
using SimpleInjector;

namespace ArchitectureTemplate.Infrastructure.CrossCutting.IoC
{
    public static class BootstrapperService
    {
        public static void RegisterServices(Container container)
        {
            container.Register(typeof(IServiceBase<>), typeof(ServiceBase<>));
            container.Register(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));

            container.Register<IEmailMailService, EmailMailService>();

            container.Register<IUsuarioService, UsuarioService>();
            container.Register<IUsuarioRepository, UsuarioRepository>();
        }
    }
}