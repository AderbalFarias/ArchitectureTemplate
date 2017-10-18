using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ArchitectureTemplate.Domain.DataEntities;
using ArchitectureTemplate.Infraestrutura.CrossCutting.Support.Extensions;

namespace ArchitectureTemplate.Domain.Interfaces.Repositories
{
    public interface IHierarchyRepository : IRepositoryBase<Hierarchy>
    {
        IDictionary<long, string> GetDictionary();
        Task<IDictionary<long, string>> GetDictionaryAsync();
        IEnumerable<Hierarchy> Get(Pagination paginar);
        Task<IEnumerable<Hierarchy>> GetAsync(Pagination paginar);
        new IEnumerable<Hierarchy> GetList(Expression<Func<Hierarchy, bool>> predicate);
        new Task<IEnumerable<Hierarchy>> GetListAsync(Expression<Func<Hierarchy, bool>> predicate);
        Hierarchy Get(long id);
        Task<Hierarchy> GetAsync(long id);
        void Remove(long hierarchyId, long userId);
        void UpdateDetalhe(HierarchyDetail entity, long userId);
        Hierarchy GetHierarchyForUser(long userId);
        IEnumerable<Hierarchy> GetHierarchyDown(long? hirarquiaId = null, List<Hierarchy> hierarchyList = null);
        IEnumerable<Hierarchy> GetHierarchyDown(string hierarchyType, List<Hierarchy> hierarchyList, bool all = false);
    }
}
