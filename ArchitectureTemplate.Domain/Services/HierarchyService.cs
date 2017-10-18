using ArchitectureTemplate.Domain.DataEntities;
using ArchitectureTemplate.Domain.Interfaces.Repositories;
using ArchitectureTemplate.Domain.Interfaces.Services;
using ArchitectureTemplate.Infraestrutura.CrossCutting.Support.Extensions;
using ArchitectureTemplate.Infraestrutura.CrossCutting.Support.Resources;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArchitectureTemplate.Domain.Services
{
    public class HierarchyService : ServiceBase<Hierarchy>, IHierarchyService
    {
        #region Fields

        private readonly IHierarchyRepository _hierarchyRepository;

        #endregion

        #region Constructors

        public HierarchyService(IHierarchyRepository hierarchyRepository)
            : base(hierarchyRepository)
        {
            _hierarchyRepository = hierarchyRepository;
        }

        #endregion

        #region Methods

        public IDictionary<long, string> GetDictionary()
        {
            return _hierarchyRepository.GetDictionary();
        }

        public async Task<IDictionary<long, string>> GetDictionaryAsync()
        {
            return await _hierarchyRepository.GetDictionaryAsync();

        }

        public IEnumerable<Hierarchy> Get(Pagination paginar)
        {
            return _hierarchyRepository.Get(paginar);
        }

        public async Task<IEnumerable<Hierarchy>> GetAsync(Pagination paginar)
        {
            return await _hierarchyRepository.GetAsync(paginar);
        }

        public IEnumerable<Hierarchy> Get()
        {
            return _hierarchyRepository.GetList(w => w.Ativo);
        }

        public async Task<IEnumerable<Hierarchy>> GetAsync()
        {
            return await _hierarchyRepository.GetListAsync(w => w.Ativo);
        }

        public Hierarchy Get(long id)
        {
            var entity = _hierarchyRepository.Get(id);

            if (entity.HierarchyDetalhe != null)
                entity.HierarchyDetalhe.PessoaFisica = !(entity
                    .HierarchyDetalhe.CpfCnpj.ToString().Length > 11);

            return entity;
        }

        public async Task<Hierarchy> GetAsync(long id)
        {
            var entity = await _hierarchyRepository.GetAsync(id);

            if (entity.HierarchyDetalhe != null)
                entity.HierarchyDetalhe.PessoaFisica = !(entity
                    .HierarchyDetalhe.CpfCnpj.ToString().Length > 11);

            return entity;
        }

        public void Remove(long hierarchyId, long userId)
        {
            _hierarchyRepository.Remove(hierarchyId, userId);
        }

        public void UpdateDetalhe(HierarchyDetail entity, long userId)
        {
            _hierarchyRepository.UpdateDetalhe(entity, userId);
        }

        public Hierarchy GetHierarchyForUser(long userId)
        {
            return _hierarchyRepository.GetHierarchyForUser(userId);
        }

        public long? GetHierarchyIdForUser(long userId)
        {
            return _hierarchyRepository.GetHierarchyForUser(userId)?.Id;
        }

        public IEnumerable<long> GetHierarchyIdsForUser(long? hirarquiaId = null, List<Hierarchy> hierarchyList = null)
        {
            return _hierarchyRepository
                .GetHierarchyDown(hirarquiaId, hierarchyList)?.Select(s => s.Id);
        }

        public IEnumerable<long> GetAllHierarchyIds()
        {
            return _hierarchyRepository
                .GetAll()?.Select(s => s.Id);
        }

        public IEnumerable<Hierarchy> GetHierarchyUp(long userId)
        {
            yield return _hierarchyRepository.GetHierarchyForUser(userId);
        }

        public IEnumerable<Hierarchy> GetHierarchyDown(long? hirarquiaId = null, List<Hierarchy> hierarchyList = null)
        {
            return _hierarchyRepository.GetHierarchyDown(hirarquiaId, hierarchyList);
        }

        public IEnumerable<Hierarchy> GetHierarchyDown(string hierarchyType, List<Hierarchy> hierarchyList)
        {
            return _hierarchyRepository.GetHierarchyDown(hierarchyType, hierarchyList);
        }

        public IEnumerable<Hierarchy> GetHierarchyDown(long userId, int profileId, bool all = true)
        {
            if (profileId == ProfileResource.Administrator)
            {
                var hList = _hierarchyRepository
                    .GetList(w => w.HierarchyPaiId == null)
                    .ToList();

                return _hierarchyRepository
                    .GetHierarchyDown(HierarchyTypeResource.Unidade, hList, all);
            }

            var hierarchyUser = _hierarchyRepository.GetHierarchyForUser(userId);

            if (hierarchyUser != null)
            {
                var nlist = new List<Hierarchy> { hierarchyUser };
                return !hierarchyUser.HierarchyType.Descricao.Equals(HierarchyTypeResource.Unidade)
                    ? _hierarchyRepository.GetHierarchyDown(HierarchyTypeResource.Unidade, nlist, true)
                    : nlist;
            }

            return new List<Hierarchy>();
        }

        public IDictionary<long, string> GetHierarchyDownDictionary(long userId, int profileId, bool all = true)
        {
            return GetHierarchyDown(userId, profileId, all)
                .ToDictionary(k => k.Id, v => v.Nome);
        }

        #endregion
    }
}