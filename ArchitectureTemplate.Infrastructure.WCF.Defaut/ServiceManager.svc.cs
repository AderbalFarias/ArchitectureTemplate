using ArchitectureTemplate.Domain.Interfaces.Services;
using ArchitectureTemplate.Infraestrutura.CrossCutting.Support.Extensions;
using ArchitectureTemplate.Infrastructure.CrossCutting.IoC;
using ArchitectureTemplate.Infrastructure.WCF.Default.Entities;
using SimpleInjector;
using SimpleInjector.Integration.Wcf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArchitectureTemplate.Infrastructure.WCF.Default
{
    public class ServiceManager : IServiceContract
    {
        private readonly IScreenService _screenService;
        private const string Log = "C:\\Logs\\ArchitectureTemplate\\Infrastructure\\WCF\\Default\\";

        public ServiceManager()
        {
            var container = InitializeContainer();
            _screenService = container.GetInstance<IScreenService>();
        }

        public ScreenContract GetById(int id)
        {
            try
            {
                var screen = _screenService.GetId(id);
                return screen.Cast<ScreenContract>();
            }
            catch (Exception e)
            {
                LogFile.Create(e, Log);
                throw new Exception("Erro on the method GetById " + e.Message);
            }
        }

        public async Task<ScreenContract> GetByIdAsync(int id)
        {
            try
            {
                var screen = await _screenService.GetIdAsync(id);
                return screen.Cast<ScreenContract>();
            }
            catch (Exception e)
            {
                LogFile.Create(e, Log);
                throw new Exception("Erro on the method GetByIdAsync " + e.Message);
            }
        }

        public ScreenContract GetByName(string name)
        {
            try
            {
                var screen = _screenService.Get(t => t.Nome == name);
                return screen.Cast<ScreenContract>();
            }
            catch (Exception e)
            {
                LogFile.Create(e, Log);
                throw new Exception("Erro on the method GetByName" + e.Message);
            }
        }

        public async Task<ScreenContract> GetByNameAsync(string name)
        {
            try
            {
                var screen = await _screenService.GetAsync(t => t.Nome == name);
                return screen?.Cast<ScreenContract>();
            }
            catch (Exception e)
            {
                LogFile.Create(e, Log);
                throw new Exception("Erro on the method GetByName" + e.Message);
            }
        }

        public IEnumerable<ScreenContract> GetScreens(string key)
        {
            try
            {
                var screenList = _screenService
                    .GetList(t => t.Nome.Contains(key)
                                  || t.ControllerName.Contains(key))
                    .ToList();

                return screenList.CastAll<ScreenContract>();
            }
            catch (Exception e)
            {
                LogFile.Create(e, Log);
                throw new Exception("Erro on the method GetScreens" + e.Message);
            }
        }

        public async Task<IEnumerable<ScreenContract>> GetScreensAsync(string key)
        {
            try
            {
                var screenList = await _screenService
                    .GetListAsync(t => t.Nome.Contains(key)
                                  || t.ControllerName.Contains(key));

                return screenList.CastAll<ScreenContract>();
            }
            catch (Exception e)
            {
                LogFile.Create(e, Log);
                throw new Exception("Erro on the method GetScreens" + e.Message);
            }
        }

        private static Container InitializeContainer()
        {
            var container = new Container();

            container.Options.DefaultScopedLifestyle = new WcfOperationLifestyle(false);
            //container.Register<IServiceContract, ServiceManager>(Lifestyle.Scoped);

            BootstrapperWcf.RegisterServicesWithoutVerify(container);
            //container.Verify();

            return container;
        }
    }
}
