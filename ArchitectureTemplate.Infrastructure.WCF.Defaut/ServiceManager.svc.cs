using ArchitectureTemplate.Domain.Interfaces.Services;
using ArchitectureTemplate.Infraestrutura.CrossCutting.Support.Extensions;
using ArchitectureTemplate.Infrastructure.WCF.Defaut.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ArchitectureTemplate.Infrastructure.WCF.Defaut
{
    public class ServiceManager : IServiceContract
    {
        private readonly IScreenService _screenService;
        private const string Log = "C:\\Logs\\ArchitectureTemplate\\Infrastructure\\WCF\\Services\\";

        public ServiceManager(IScreenService screenService)
        {
            _screenService = screenService;
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

        public IEnumerable<ScreenContract> GetScreens(int idBegin, int idEnd)
        {
            try
            {
                var screenList = _screenService
                    .GetList(t => t.Id >= idBegin && t.Id <= idEnd)
                    .ToList();

                return screenList.Cast<ScreenContract>();
            }
            catch (Exception e)
            {
                LogFile.Create(e, Log);
                throw new Exception("Erro on the method GetScreens" + e.Message);
            }
        }
    }
}
