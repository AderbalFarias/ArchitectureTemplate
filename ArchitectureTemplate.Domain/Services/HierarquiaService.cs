using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArchitectureTemplate.Domain.DataEntities;
using ArchitectureTemplate.Domain.Interfaces.Repositories;
using ArchitectureTemplate.Domain.Interfaces.Services;
using ArchitectureTemplate.Infraestrutura.CrossCutting.Support.Extensions;
using ArchitectureTemplate.Infraestrutura.CrossCutting.Support.Resources;

namespace ArchitectureTemplate.Domain.Services
{
    public class HierarquiaService : ServiceBase<Hierarquia>, IHierarquiaService
    {
        #region Fields

        private readonly IHierarquiaRepository _hierarquiaRepository;

        #endregion

        #region Constructors

        public HierarquiaService(IHierarquiaRepository hierarquiaRepository)
            : base(hierarquiaRepository)
        {
            _hierarquiaRepository = hierarquiaRepository;
        }

        #endregion

        #region Methods

        public IDictionary<long, string> GetDictionary()
        {
            return _hierarquiaRepository.GetDictionary();
        }

        public async Task<IDictionary<long, string>> GetDictionaryAsync()
        {
            return await _hierarquiaRepository.GetDictionaryAsync();

        }

        public IEnumerable<Hierarquia> Get(Pagination paginar)
        {
            return _hierarquiaRepository.Get(paginar);
        }

        public async Task<IEnumerable<Hierarquia>> GetAsync(Pagination paginar)
        {
            return await _hierarquiaRepository.GetAsync(paginar);
        }

        public IEnumerable<Hierarquia> Get()
        {
            return _hierarquiaRepository.GetList(w => w.Ativo);
        }

        public async Task<IEnumerable<Hierarquia>> GetAsync()
        {
            return await _hierarquiaRepository.GetListAsync(w => w.Ativo);
        }

        public Hierarquia Get(long id)
        {
            var entity = _hierarquiaRepository.Get(id);

            if (entity.HierarquiaDetalhe != null)
                entity.HierarquiaDetalhe.PessoaFisica = !(entity
                    .HierarquiaDetalhe.CpfCnpj.ToString().Length > 11);

            return entity;
        }

        public async Task<Hierarquia> GetAsync(long id)
        {
            var entity = await _hierarquiaRepository.GetAsync(id);

            if (entity.HierarquiaDetalhe != null)
                entity.HierarquiaDetalhe.PessoaFisica = !(entity
                    .HierarquiaDetalhe.CpfCnpj.ToString().Length > 11);

            return entity;
        }

        public void Remove(long hierarquiaId, long userId)
        {
            _hierarquiaRepository.Remove(hierarquiaId, userId);
        }

        public void UpdateDetalhe(HierarquiaDetalhe entity, long userId)
        {
            _hierarquiaRepository.UpdateDetalhe(entity, userId);
        }

        public Hierarquia GetHierarquiaForUser(long userId)
        {
            return _hierarquiaRepository.GetHierarquiaForUser(userId);
        }

        public long? GetHierarquiaIdForUser(long userId)
        {
            return _hierarquiaRepository.GetHierarquiaForUser(userId)?.Id;
        }

        public IEnumerable<long> GetHierarquiaIdsForUser(long? hirarquiaId = null, List<Hierarquia> hierarquiaList = null)
        {
            return _hierarquiaRepository
                .GetHierarquiaDown(hirarquiaId, hierarquiaList)?.Select(s => s.Id);
        }

        public IEnumerable<long> GetAllHierarquiaIds()
        {
            return _hierarquiaRepository
                .GetAll()?.Select(s => s.Id);
        }

        public IEnumerable<Hierarquia> GetHierarquiaUp(long userId)
        {
            yield return _hierarquiaRepository.GetHierarquiaForUser(userId);
        }

        public IEnumerable<Hierarquia> GetHierarquiaDown(long? hirarquiaId = null, List<Hierarquia> hierarquiaList = null)
        {
            return _hierarquiaRepository.GetHierarquiaDown(hirarquiaId, hierarquiaList);
        }

        public IEnumerable<Hierarquia> GetHierarquiaDown(string tipoHierarquia, List<Hierarquia> hierarquiaList)
        {
            return _hierarquiaRepository.GetHierarquiaDown(tipoHierarquia, hierarquiaList);
        }

        public IEnumerable<Hierarquia> GetHierarquiaDown(long userId, int ProfileId, bool all = true)
        {
            if (ProfileId == ProfileResource.Administrator)
            {
                var hList = _hierarquiaRepository
                    .GetList(w => w.HierarquiaPaiId == null)
                    .ToList();

                return _hierarquiaRepository
                    .GetHierarquiaDown(TipoHierarquiaResource.Unidade, hList, all);
            }

            var hierarquiaUsuario = _hierarquiaRepository.GetHierarquiaForUser(userId);

            if (hierarquiaUsuario != null)
            {
                var nlist = new List<Hierarquia> { hierarquiaUsuario };
                return !hierarquiaUsuario.TipoHierarquia.Descricao.Equals(TipoHierarquiaResource.Unidade)
                    ? _hierarquiaRepository.GetHierarquiaDown(TipoHierarquiaResource.Unidade, nlist, true)
                    : nlist;
            }

            return new List<Hierarquia>();
        }

        public IDictionary<long, string> GetHierarquiaDownDictionary(long userId, int ProfileId, bool all = true)
        {
            return GetHierarquiaDown(userId, ProfileId, all)
                .ToDictionary(k => k.Id, v => v.Nome);
        }

        #endregion
    }
}