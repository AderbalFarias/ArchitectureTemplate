using ArchitectureTemplate.Domain.Interfaces.Repositories;
using ArchitectureTemplate.Domain.Interfaces.Services;
using ArchitectureTemplate.Domain.Services;
using ArchitectureTemplate.Infraestrutura.CrossCutting.Support.Extensions;
using ArchitectureTemplate.Infrastructure.Data.Repositories;
using SimpleInjector;

namespace ArchitectureTemplate.Infrastructure.CrossCutting.IoC
{
    public static class Bootstrapper
    {
        public static void RegisterServices(Container container)
        {
            container.Register(typeof(IServiceBase<>), typeof(ServiceBase<>));
            container.Register(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));

            container.Register<Pagination>(Lifestyle.Scoped);
            container.Register<Word>(Lifestyle.Scoped);

            container.Register<IEmailMailService, EmailMailService>(Lifestyle.Scoped);

            container.Register<IUserService, UserService>(Lifestyle.Scoped);
            container.Register<IUserRepository, UserRepository>(Lifestyle.Scoped);

            container.Register<IProfileService, ProfileService>(Lifestyle.Scoped);
            container.Register<IProfileRepository, ProfileRepository>(Lifestyle.Scoped);

            container.Register<IHierarchyService, HierarchyService>(Lifestyle.Scoped);
            container.Register<IHierarchyRepository, HierarchyRepository>(Lifestyle.Scoped);

            container.Register<IDictionaryAllService, DictionaryAllService>(Lifestyle.Scoped);
            container.Register<IDictionaryAllRepository, DictionaryAllRepository>(Lifestyle.Scoped);

            container.Register<ILogService, LogService>(Lifestyle.Scoped);
            container.Register<ILogRepository, LogRepository>(Lifestyle.Scoped);

            container.Register<IPermissionService, PermissionService>(Lifestyle.Scoped);
            container.Register<IPermissionRepository, PermissionRepository>(Lifestyle.Scoped);

            container.Register<IScreenService, ScreenService>(Lifestyle.Scoped);
            container.Register<IScreenRepository, ScreenRepository>(Lifestyle.Scoped);

            container.Register<IMenuService, MenuService>(Lifestyle.Scoped);
            container.Register<IMenuRepository, MenuRepository>(Lifestyle.Scoped);
        }
    }
}