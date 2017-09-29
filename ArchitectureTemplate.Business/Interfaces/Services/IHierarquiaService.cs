using System.Collections.Generic;
using System.Threading.Tasks;
using ArchitectureTemplate.Business.DataEntities;
using ArchitectureTemplate.Infraestrutura.CrossCutting.Support.Extensions;

namespace ArchitectureTemplate.Business.Interfaces.Services
{
    public interface IHierarquiaService : IServiceBase<Hierarquia>
    {
        IDictionary<long, string> GetDictionary();
        Task<IDictionary<long, string>> GetDictionaryAsync();
        IEnumerable<Hierarquia> Get(Pagination paginar);
        Task<IEnumerable<Hierarquia>> GetAsync(Pagination paginar);
        IEnumerable<Hierarquia> Get();
        Task<IEnumerable<Hierarquia>> GetAsync();
        Hierarquia Get(long id);
        Task<Hierarquia> GetAsync(long id);
        void Remove(long hierarquiaId, long userId);
        void UpdateDetalhe(HierarquiaDetalhe entity, long userId);
        Hierarquia GetHierarquiaForUser(long userId);
        long? GetHierarquiaIdForUser(long userId);
        IEnumerable<long> GetHierarquiaIdsForUser(long? hirarquiaId = null, List<Hierarquia> hierarquiaList = null);
        IEnumerable<long> GetAllHierarquiaIds();
        IEnumerable<Hierarquia> GetHierarquiaUp(long userId);
        IEnumerable<Hierarquia> GetHierarquiaDown(long? hirarquiaId = null, List<Hierarquia> hierarquiaList = null);
        IEnumerable<Hierarquia> GetHierarquiaDown(string tipoHierarquia, List<Hierarquia> hierarquiaList);
        IEnumerable<Hierarquia> GetHierarquiaDown(long userId, int perfilId, bool all = true);
        IDictionary<long, string> GetHierarquiaDownDictionary(long userId, int perfilId, bool all = true);
    }
}
