using ArchitectureTemplate.Business.Interfaces.Services;
using ArchitectureTemplate.Infraestrutura.CrossCutting.Support.Extensions;
using ArchitectureTemplate.Infrastructure.WCF.Contracts.Entities;
using ArchitectureTemplate.Infrastructure.WCF.Contracts.ServiceInterfaces;
using System.Collections.Generic;
using System.Linq;

namespace ArchitectureTemplate.Infrastructure.WCF.Services
{
    public class TelaManager : ITelaServiceContract
    {
        private readonly ITelaService _telaService;

        public TelaManager()
        {

        }

        public TelaManager(ITelaService telaService)
        {
            _telaService = telaService;
        }

        public TelaContract GetById(int id)
        {
            var tela = _telaService.GetId(id);
            return tela.Cast<TelaContract>();
        }

        public TelaContract GetByName(string name)
        {
            var tela = _telaService.Get(t => t.Nome == name);
            return tela.Cast<TelaContract>();
        }

        public IEnumerable<TelaContract> GetTelas(string key)
        {
            var telaList = _telaService
                .GetList(t => t.Nome.Contains(key)
                    || t.ControllerName.Contains(key))
                .ToList();

            return telaList.CastAll<TelaContract>();
        }

        public IEnumerable<TelaContract> GetTelas(int idBegin, int idEnd)
        {
            var telaList = _telaService
                .GetList(t => t.Id >= idBegin && t.Id <= idEnd)
                .ToList();

            return telaList.Cast<TelaContract>();
        }
    }
}
