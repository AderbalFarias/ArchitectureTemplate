using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArchitectureTemplate.Business.DataEntities;
using ArchitectureTemplate.Business.Interfaces.Repositories;
using ArchitectureTemplate.Business.Interfaces.Services;
using ArchitectureTemplate.Infraestrutura.CrossCutting.Support.Extensions;

namespace ArchitectureTemplate.Business.Services
{
    public class TelaService : ServiceBase<Tela>, ITelaService
    {
        #region Fields

        private readonly ITelaRepository _telaRepository;

        #endregion
        
        #region Constructors

        public TelaService(ITelaRepository telaRepository)
            : base(telaRepository)
        {
            _telaRepository = telaRepository;
        }

        #endregion

        #region Methods
       
        public void Synchronize(IList<Tela> entityList, long userId)
        {
            IList<Tela> telas = _telaRepository
                .GetAllWithDapper()
                .ToList();
            
            var telasNaoExistentes = entityList
                .Where(w => !telas.Select(s => s.ControllerName).Contains(w.ControllerName))
                .ToList();

            var telasDelete = telas
                .Where(w => !entityList.Select(s => s.ControllerName).Contains(w.ControllerName))
                .ToList();

            var telasExistentes = telas
                .Join(entityList, t => t.ControllerName, e => e.ControllerName, (t, e) => new {t, e})
                .Select(s => new Tela
                {
                    Id = s.t.Id,
                    Nome = s.t.Nome,
                    ControllerName = s.e.ControllerName,
                    DataCadastro = s.t.DataCadastro,
                    Ativo = s.t.Ativo,
                    Create = s.e.Create,
                    Read = s.e.Read,
                    Update = s.e.Update,
                    Delete = s.e.Delete
                })
                .ToList();

            _telaRepository.AddOrUpdate(telasExistentes, userId);

            if (telasNaoExistentes.Count > 0)
            {
                _telaRepository.AddOrUpdate(telasNaoExistentes, userId);
            }

            foreach (var tela in telasDelete)
            {
                _telaRepository.Remove(tela.Id, userId);
            }
        }

        public IEnumerable<Tela> Get(Pagination pagination)
        {
            return _telaRepository.Get(pagination);
        }

        public async Task<IEnumerable<Tela>> GetAsync(Pagination pagination)
        {
            return await _telaRepository.GetAsync(pagination);
        }

        public void Update(Tela entity, long userId)
        {
            var tela = _telaRepository.GetId(entity.Id);
            tela.Nome = entity.Nome;
            tela.Ativo = entity.Ativo;

            _telaRepository.Update(tela, userId, true);
        }

        #endregion
    }
}