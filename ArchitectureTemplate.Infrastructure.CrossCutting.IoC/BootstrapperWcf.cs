using ArchitectureTemplate.Domain.Interfaces.Repositories;
using ArchitectureTemplate.Domain.Interfaces.Services;
using ArchitectureTemplate.Domain.Services;
using ArchitectureTemplate.Infrastructure.Data.Repositories;
using SimpleInjector;

namespace ArchitectureTemplate.Infrastructure.CrossCutting.IoC
{
    public static class BootstrapperWcf
    {
        public static void RegisterServices(Container container)
        {
            container.Register(typeof(IServiceBase<>), typeof(ServiceBase<>));
            container.Register(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));

            container.Register<IScreenService, ScreenService>(Lifestyle.Scoped);
            container.Register<IScreenRepository, ScreenRepository>(Lifestyle.Scoped);

            container.Register<IProfileService, ProfileService>(Lifestyle.Scoped);
            container.Register<IProfileRepository, ProfileRepository>(Lifestyle.Scoped);
        }


        public static void RegisterServicesWithoutVerify(Container container)
        {
            container.Register(typeof(IServiceBase<>), typeof(ServiceBase<>));
            container.Register(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));

            container.Register<IScreenService, ScreenService>();
            container.Register<IScreenRepository, ScreenRepository>();
        }
    }
}