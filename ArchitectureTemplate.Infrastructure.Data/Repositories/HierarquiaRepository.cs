using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ArchitectureTemplate.Business.DataEntities;
using ArchitectureTemplate.Business.Interfaces.Repositories;
using ArchitectureTemplate.Infraestrutura.CrossCutting.Support.Extensions;
using ArchitectureTemplate.Infraestrutura.CrossCutting.Support.Resources;

namespace ArchitectureTemplate.Infrastructure.Data.Repositories
{
    public class HierarquiaRepository : RepositoryBase<Hierarquia>, IHierarquiaRepository
    {
        #region Fields
        
        private readonly ILogRepository _logRepository = new LogRepository();

        #endregion

        #region Methods

        /// <summary>
        /// Busca hierarquia
        /// </summary>
        /// <returns>Dicionário contendo código e descrição de todas as hierarquia</returns>
        public IDictionary<long, string> GetDictionary()
        {
            return _context.Hierarquia
                .Include(i => i.TipoHierarquia)
                .Where(w => w.Ativo)
                .OrderBy(o => o.Nome)
                .ToDictionary(k => k.Id, v => $"{v.Nome} ({v.TipoHierarquia.Descricao})");
        }

        /// <summary>
        /// Busca hierarquia de forma assíncrona
        /// </summary>
        /// <returns>Dicionário contendo código e descrição de todas as hierarquia</returns>
        public async Task<IDictionary<long, string>> GetDictionaryAsync()
        {
            return await _context.Hierarquia
                .Include(i => i.TipoHierarquia)
                .Where(w => w.Ativo)
                .OrderBy(o => o.Nome)
                .ToDictionaryAsync(k => k.Id, v => $"{v.Nome} ({v.TipoHierarquia.Descricao})");
        }

        /// <summary>
        /// Buscar hierarquia
        /// </summary>
        /// <param name="paginar">propriedades para paginação</param>
        /// <returns>Lista de hierarquia limitados por paginação</returns>
        public IEnumerable<Hierarquia> Get(Pagination paginar)
        {
            return _context.Hierarquia
                .Include(i => i.TipoHierarquia)
                .Include(i => i.HierarquiaDetalhe)
                .Include(i => i.HierarquiaUp)
                .Include(i => i.HierarquiaDown)
                .OrderBy(o => o.Nome)
                .Skip(paginar.SkipPagina(paginar))
                .Take(paginar.QtdeItensPagina)
                .ToList();
        }

        /// <summary>
        /// Busca hierarquia assíncrona
        /// </summary>
        /// <param name="paginar">propriedades para paginação</param>
        /// <returns>Lista de hierarquia limitados por paginação</returns>
        public async Task<IEnumerable<Hierarquia>> GetAsync(Pagination paginar)
        {
            return await _context.Hierarquia
                .Include(i => i.TipoHierarquia)
                .Include(i => i.HierarquiaDetalhe)
                .Include(i => i.HierarquiaUp)
                .Include(i => i.HierarquiaDown)
                .OrderBy(o => o.Nome)
                .Skip(paginar.SkipPagina(paginar))
                .Take(paginar.QtdeItensPagina)
                .ToListAsync();
        }

        public new IEnumerable<Hierarquia> GetList(Expression<Func<Hierarquia, bool>> predicate)
        {
            return _context.Hierarquia
                .Include(i => i.TipoHierarquia)
                .Include(i => i.HierarquiaDetalhe)
                .Include(i => i.HierarquiaUp)
                .Include(i => i.HierarquiaDown)
                .Where(predicate)
                .OrderBy(o => o.Nome)
                .ToList();
        }

        public new async Task<IEnumerable<Hierarquia>> GetListAsync(Expression<Func<Hierarquia, bool>> predicate)
        {
            return await _context.Hierarquia
                .Include(i => i.TipoHierarquia)
                .Include(i => i.HierarquiaDetalhe)
                .Include(i => i.HierarquiaUp)
                .Include(i => i.HierarquiaDown)
                .Where(predicate)
                .OrderBy(o => o.Nome)
                .ToListAsync();
        }

        public Hierarquia Get(long id)
        {
            return _context.Hierarquia
                .Include(i => i.TipoHierarquia)
                .Include(i => i.HierarquiaDetalhe)
                .Include(i => i.HierarquiaUp)
                .Include(i => i.HierarquiaDown)
                .First(f => f.Id == id);
        }

        public async Task<Hierarquia> GetAsync(long id)
        {
            return await _context.Hierarquia
                .Include(i => i.TipoHierarquia)
                .Include(i => i.HierarquiaDetalhe)
                .Include(i => i.HierarquiaUp)
                .Include(i => i.HierarquiaDown)
                .FirstAsync(f => f.Id == id);
        }

        public void Remove(long hierarquiaId, long userId)
        {
            var hierarquia = _context.Hierarquia.Find(hierarquiaId);
            var detalhe = _context.HierarquiaDetalhe.Find(hierarquiaId);

            _context.HierarquiaDetalhe.Remove(detalhe);
            _context.Hierarquia.Remove(hierarquia);
            _context.SaveChanges();

            _logRepository.Add(new Log().GeneratedForEntity(userId, hierarquia, LogTypeResource.Delete));
            _logRepository.Add(new Log().GeneratedForEntity(userId, detalhe, LogTypeResource.Delete));
        }

        public void UpdateDetalhe(HierarquiaDetalhe entity, long userId)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();

            _logRepository.Add(new Log().GeneratedForEntity(userId, entity, LogTypeResource.Update));
        }

        public Hierarquia GetHierarquiaForUser(long userId)
        {
            return _context.Usuario
                .Include(i => i.Hierarquia)
                .Include(i => i.Hierarquia.HierarquiaDown)
                .Include(i => i.Hierarquia.HierarquiaUp)
                .Include(i => i.Hierarquia.TipoHierarquia)
                .First(f => f.Id == userId)?.Hierarquia;
        }

        public IEnumerable<Hierarquia> GetHierarquiaDown(long? hirarquiaId = null, List<Hierarquia> hierarquiaList = null)
        {
            hierarquiaList = hierarquiaList ?? new List<Hierarquia>();

            var idsHierarquia = hierarquiaList
                .Select(s => s.Id)
                .ToList();

            var hieraquia = _context.Hierarquia
                .Include(i => i.HierarquiaDown);

            var entity = hirarquiaId != null
                ? hieraquia.Where(w => w.Id == hirarquiaId).ToList()
                : hieraquia.Where(w => idsHierarquia.Contains(w.HierarquiaPaiId ?? 0)
                    && !idsHierarquia.Contains(w.Id)).ToList();

            hierarquiaList.AddRange(entity);

            if (entity.SelectMany(sm => sm.HierarquiaDown).Any())
            {
                GetHierarquiaDown(hierarquiaList: hierarquiaList);
            }

            return hierarquiaList.OrderBy(o => o.Nome);
        }

        public IEnumerable<Hierarquia> GetHierarquiaDown(string tipoHierarquia, List<Hierarquia> hierarquiaList, bool all = false)
        {
            if (hierarquiaList.Any(a => a.TipoHierarquia.Descricao.Contains(tipoHierarquia)))
            {
                if (all)
                    return hierarquiaList
                    .OrderBy(o => o.Nome)
                    .ToList();

                hierarquiaList = hierarquiaList
                    .Where(w => w.TipoHierarquia.Descricao.Contains(tipoHierarquia))
                    .OrderBy(o => o.Nome)
                    .ToList();

                return hierarquiaList;
            }

            var hierarquiaDown = _context.Hierarquia
                .SelectMany(w => w.HierarquiaDown)
                .Include(i => i.TipoHierarquia)
                .ToList();

            hierarquiaList.AddRange(hierarquiaDown);
            return GetHierarquiaDown(tipoHierarquia, hierarquiaList);
        }

        #endregion
    }
}
