using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ArchitectureTemplate.Domain.DataEntities;
using ArchitectureTemplate.Infraestrutura.CrossCutting.Support.Extensions;

namespace ArchitectureTemplate.Domain.Interfaces.Repositories
{
    public interface IHierarquiaRepository : IRepositoryBase<Hierarquia>
    {
        IDictionary<long, string> GetDictionary();
        Task<IDictionary<long, string>> GetDictionaryAsync();
        IEnumerable<Hierarquia> Get(Pagination paginar);
        Task<IEnumerable<Hierarquia>> GetAsync(Pagination paginar);
        new IEnumerable<Hierarquia> GetList(Expression<Func<Hierarquia, bool>> predicate);
        new Task<IEnumerable<Hierarquia>> GetListAsync(Expression<Func<Hierarquia, bool>> predicate);
        Hierarquia Get(long id);
        Task<Hierarquia> GetAsync(long id);
        void Remove(long hierarquiaId, long userId);
        void UpdateDetalhe(HierarquiaDetalhe entity, long userId);
        Hierarquia GetHierarquiaForUser(long userId);
        IEnumerable<Hierarquia> GetHierarquiaDown(long? hirarquiaId = null, List<Hierarquia> hierarquiaList = null);
        IEnumerable<Hierarquia> GetHierarquiaDown(string tipoHierarquia, List<Hierarquia> hierarquiaList, bool all = false);
    }
}
