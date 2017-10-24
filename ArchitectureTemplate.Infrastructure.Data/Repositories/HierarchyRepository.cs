using ArchitectureTemplate.Domain.DataEntities;
using ArchitectureTemplate.Domain.Interfaces.Repositories;
using ArchitectureTemplate.Infraestrutura.CrossCutting.Support.Extensions;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ArchitectureTemplate.Infrastructure.Data.Repositories
{
    public class HierarchyRepository : RepositoryBase<Hierarchy>, IHierarchyRepository
    {
        #region Fields

        //private readonly ILogRepository _logRepository = new LogRepository();

        #endregion

        #region Methods

        /// <summary>
        /// Busca hierarchy
        /// </summary>
        /// <returns>Dicionário contendo código e descrição de todas as hierarchy</returns>
        public IDictionary<long, string> GetDictionary()
        {
            return _context.Hierarchy
                .Include(i => i.HierarchyType)
                .Where(w => w.Ativo)
                .OrderBy(o => o.Nome)
                .ToDictionary(k => k.Id, v => $"{v.Nome} ({v.HierarchyType.Descricao})");
        }

        /// <summary>
        /// Busca hierarchy de forma assíncrona
        /// </summary>
        /// <returns>Dicionário contendo código e descrição de todas as hierarchy</returns>
        public async Task<IDictionary<long, string>> GetDictionaryAsync()
        {
            return await _context.Hierarchy
                .Include(i => i.HierarchyType)
                .Where(w => w.Ativo)
                .OrderBy(o => o.Nome)
                .ToDictionaryAsync(k => k.Id, v => $"{v.Nome} ({v.HierarchyType.Descricao})");
        }

        /// <summary>
        /// Buscar hierarchy
        /// </summary>
        /// <param name="paginar">propriedades para paginação</param>
        /// <returns>Lista de hierarchy limitados por paginação</returns>
        public IEnumerable<Hierarchy> Get(Pagination paginar)
        {
            return _context.Hierarchy
                .Include(i => i.HierarchyType)
                .Include(i => i.HierarchyDetalhe)
                .Include(i => i.HierarchyUp)
                .Include(i => i.HierarchyDown)
                .OrderBy(o => o.Nome)
                .Skip(paginar.SkipPagina(paginar))
                .Take(paginar.QtdeItensPagina)
                .ToList();
        }

        /// <summary>
        /// Busca hierarchy assíncrona
        /// </summary>
        /// <param name="paginar">propriedades para paginação</param>
        /// <returns>Lista de hierarchy limitados por paginação</returns>
        public async Task<IEnumerable<Hierarchy>> GetAsync(Pagination paginar)
        {
            return await _context.Hierarchy
                .Include(i => i.HierarchyType)
                .Include(i => i.HierarchyDetalhe)
                .Include(i => i.HierarchyUp)
                .Include(i => i.HierarchyDown)
                .OrderBy(o => o.Nome)
                .Skip(paginar.SkipPagina(paginar))
                .Take(paginar.QtdeItensPagina)
                .ToListAsync();
        }

        public new IEnumerable<Hierarchy> GetList(Expression<Func<Hierarchy, bool>> predicate)
        {
            return _context.Hierarchy
                .Include(i => i.HierarchyType)
                .Include(i => i.HierarchyDetalhe)
                .Include(i => i.HierarchyUp)
                .Include(i => i.HierarchyDown)
                .Where(predicate)
                .OrderBy(o => o.Nome)
                .ToList();
        }

        public new async Task<IEnumerable<Hierarchy>> GetListAsync(Expression<Func<Hierarchy, bool>> predicate)
        {
            return await _context.Hierarchy
                .Include(i => i.HierarchyType)
                .Include(i => i.HierarchyDetalhe)
                .Include(i => i.HierarchyUp)
                .Include(i => i.HierarchyDown)
                .Where(predicate)
                .OrderBy(o => o.Nome)
                .ToListAsync();
        }

        public Hierarchy Get(long id)
        {
            return _context.Hierarchy
                .Include(i => i.HierarchyType)
                .Include(i => i.HierarchyDetalhe)
                .Include(i => i.HierarchyUp)
                .Include(i => i.HierarchyDown)
                .First(f => f.Id == id);
        }

        public async Task<Hierarchy> GetAsync(long id)
        {
            return await _context.Hierarchy
                .Include(i => i.HierarchyType)
                .Include(i => i.HierarchyDetalhe)
                .Include(i => i.HierarchyUp)
                .Include(i => i.HierarchyDown)
                .FirstAsync(f => f.Id == id);
        }

        public void Remove(long hierarchyId, long userId)
        {
            var hierarchy = _context.Hierarchy.Find(hierarchyId);
            var detail = _context.HierarchyDetalhe.Find(hierarchyId);

            _context.HierarchyDetalhe.Remove(detail);
            _context.Hierarchy.Remove(hierarchy);
            _context.SaveChanges();

            //_logRepository.Add(new Log().GeneratedsForEntity(userId, hierarchy, LogTypeResource.Delete));
            //_logRepository.Add(new Log().GeneratedForEntity(userId, detail, LogTypeResource.Delete));
        }

        public void UpdateDetalhe(HierarchyDetail entity, long userId)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();

            //_logRepository.Add(new Log().GeneratedForEntity(userId, entity, LogTypeResource.Update));
        }

        public Hierarchy GetHierarchyForUser(long userId)
        {
            return _context.User
                .Include(i => i.Hierarchy)
                .Include(i => i.Hierarchy.HierarchyDown)
                .Include(i => i.Hierarchy.HierarchyUp)
                .Include(i => i.Hierarchy.HierarchyType)
                .First(f => f.Id == userId)?.Hierarchy;
        }

        public IEnumerable<Hierarchy> GetHierarchyDown(long? hirarquiaId = null, List<Hierarchy> hierarchyList = null)
        {
            hierarchyList = hierarchyList ?? new List<Hierarchy>();

            var idsHierarchy = hierarchyList
                .Select(s => s.Id)
                .ToList();

            var hieraquia = _context.Hierarchy
                .Include(i => i.HierarchyDown);

            var entity = hirarquiaId != null
                ? hieraquia.Where(w => w.Id == hirarquiaId).ToList()
                : hieraquia.Where(w => idsHierarchy.Contains(w.HierarchyPaiId ?? 0)
                    && !idsHierarchy.Contains(w.Id)).ToList();

            hierarchyList.AddRange(entity);

            if (entity.SelectMany(sm => sm.HierarchyDown).Any())
            {
                GetHierarchyDown(hierarchyList: hierarchyList);
            }

            return hierarchyList.OrderBy(o => o.Nome);
        }

        public IEnumerable<Hierarchy> GetHierarchyDown(string hierarchyType, List<Hierarchy> hierarchyList, bool all = false)
        {
            if (hierarchyList.Any(a => a.HierarchyType.Descricao.Contains(hierarchyType)))
            {
                if (all)
                    return hierarchyList
                    .OrderBy(o => o.Nome)
                    .ToList();

                hierarchyList = hierarchyList
                    .Where(w => w.HierarchyType.Descricao.Contains(hierarchyType))
                    .OrderBy(o => o.Nome)
                    .ToList();

                return hierarchyList;
            }

            var hierarchyDown = _context.Hierarchy
                .SelectMany(w => w.HierarchyDown)
                .Include(i => i.HierarchyType)
                .ToList();

            hierarchyList.AddRange(hierarchyDown);
            return GetHierarchyDown(hierarchyType, hierarchyList);
        }

        #endregion
    }
}
