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

            container.Register<IUsuarioService, UsuarioService>(Lifestyle.Scoped);
            container.Register<IUsuarioRepository, UsuarioRepository>(Lifestyle.Scoped);

            container.Register<IPerfilService, PerfilService>(Lifestyle.Scoped);
            container.Register<IPerfilRepository, PerfilRepository>(Lifestyle.Scoped);

            container.Register<IHierarquiaService, HierarquiaService>(Lifestyle.Scoped);
            container.Register<IHierarquiaRepository, HierarquiaRepository>(Lifestyle.Scoped);

            container.Register<IDictionaryAllService, DictionaryAllService>(Lifestyle.Scoped);
            container.Register<IDictionaryAllRepository, DictionaryAllRepository>(Lifestyle.Scoped);

            container.Register<ILogService, LogService>(Lifestyle.Scoped);
            container.Register<ILogRepository, LogRepository>(Lifestyle.Scoped);

            container.Register<IPermissaoService, PermissaoService>(Lifestyle.Scoped);
            container.Register<IPermissaoRepository, PermissaoRepository>(Lifestyle.Scoped);

            container.Register<ITelaService, TelaService>(Lifestyle.Scoped);
            container.Register<ITelaRepository, TelaRepository>(Lifestyle.Scoped);

            container.Register<IMenuService, MenuService>(Lifestyle.Scoped);
            container.Register<IMenuRepository, MenuRepository>(Lifestyle.Scoped);
        }
    }
}