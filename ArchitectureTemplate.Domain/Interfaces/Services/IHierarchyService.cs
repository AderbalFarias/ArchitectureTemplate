using ArchitectureTemplate.Domain.DataEntities;
using ArchitectureTemplate.Infraestrutura.CrossCutting.Support.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ArchitectureTemplate.Domain.Interfaces.Services
{
    public interface IHierarchyService : IServiceBase<Hierarchy>
    {
        IDictionary<long, string> GetDictionary();
        Task<IDictionary<long, string>> GetDictionaryAsync();
        IEnumerable<Hierarchy> Get(Pagination paginar);
        Task<IEnumerable<Hierarchy>> GetAsync(Pagination paginar);
        IEnumerable<Hierarchy> Get();
        Task<IEnumerable<Hierarchy>> GetAsync();
        Hierarchy Get(long id);
        Task<Hierarchy> GetAsync(long id);
        void Remove(long hierarchyId, long userId);
        void UpdateDetalhe(HierarchyDetail entity, long userId);
        Hierarchy GetHierarchyForUser(long userId);
        long? GetHierarchyIdForUser(long userId);
        IEnumerable<long> GetHierarchyIdsForUser(long? hirarquiaId = null, List<Hierarchy> hierarchyList = null);
        IEnumerable<long> GetAllHierarchyIds();
        IEnumerable<Hierarchy> GetHierarchyUp(long userId);
        IEnumerable<Hierarchy> GetHierarchyDown(long? hirarquiaId = null, List<Hierarchy> hierarchyList = null);
        IEnumerable<Hierarchy> GetHierarchyDown(string hierarchyType, List<Hierarchy> hierarchyList);
        IEnumerable<Hierarchy> GetHierarchyDown(long userId, int profileId, bool all = true);
        IDictionary<long, string> GetHierarchyDownDictionary(long userId, int profileId, bool all = true);
    }
}
