using ArchitectureTemplate.Business.Interfaces.Services;
using ArchitectureTemplate.Infrastructure.WCF.Contracts.Entities;
using ArchitectureTemplate.Infrastructure.WCF.Contracts.ServiceInterfaces;
using System.Collections.Generic;
using System.Linq;

namespace ArchitectureTemplate.Infrastructure.WCF.Services
{
    public class TelaManeger : ITelaServiceContract
    {
        private readonly ITelaService _telaService;

        public TelaManeger(ITelaService telaService)
        {
            _telaService = telaService;
        }

        public TelaContract GetById(int id)
        {
            var tela = _telaService.GetId(id);
            return new TelaContract
            {
                Id = tela.Id,
                Ativo = tela.Ativo,
                ControllerName = tela.ControllerName,
                Nome = tela.Nome
            };
        }

        public TelaContract GetByName(string name)
        {
            var tela = _telaService.Get(t => t.Nome == name);
            return new TelaContract
            {
                Id = tela.Id,
                Ativo = tela.Ativo,
                ControllerName = tela.ControllerName,
                Nome = tela.Nome
            };
        }

        public IEnumerable<TelaContract> GetTelas(string key)
        {
            var telaList = _telaService
                .GetList(t => t.Nome.Contains(key)
                    || t.ControllerName.Contains(key))
                .Select(tela => new TelaContract
                {
                    Id = tela.Id,
                    Ativo = tela.Ativo,
                    ControllerName = tela.ControllerName,
                    Nome = tela.Nome
                })
                .ToList();

            return telaList;
        }

        public IEnumerable<TelaContract> GetTelas(int idBegin, int idEnd)
        {
            var telaList = _telaService
                .GetList(t => t.Id >= idBegin && t.Id <= idEnd)
                .Select(tela => new TelaContract
                {
                    Id = tela.Id,
                    Ativo = tela.Ativo,
                    ControllerName = tela.ControllerName,
                    Nome = tela.Nome
                })
                .ToList();

            return telaList;
        }
    }
}
