using ArchitectureTemplate.Business.DataEntities;
using ArchitectureTemplate.Business.Interfaces.Repositories;
using ArchitectureTemplate.Infraestrutura.CrossCutting.Support.Extensions;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace ArchitectureTemplate.Infrastructure.Data.Repositories
{
    public class PerfilRepository : RepositoryBase<Perfil>, IPerfilRepository
    {
        #region Fields

        #endregion

        #region Methods

        /// <summary>
        /// Busca perfil
        /// </summary>
        /// <returns>Dicionário de perfil com código e descrição</returns>
        public IDictionary<int, string> GetDictionary()
        {
            return _context.Perfil
                .Where(w => w.Ativo)
                .ToDictionary(k => k.Id, v => v.Nome);
        }

        /// <summary>
        /// Busca perfil de forma assíncrona
        /// </summary>
        /// <returns>Dicionário de perfil com código e descrição</returns>
        public async Task<IDictionary<int, string>> GetDictionaryAsync()
        {
            return await _context.Perfil
                .Where(w => w.Ativo)
                .ToDictionaryAsync(k => k.Id, v => v.Nome);
        }

        /// <summary>
        /// Busca perfil
        /// </summary>
        /// <param name="paginar">Propriedades para paginação</param>
        /// <returns>Lista de perfil, limitados por paginação</returns>
        public IEnumerable<Perfil> Get(Pagination paginar)
        {
            return _context.Perfil
                .OrderBy(o => o.Nome)
                .Skip(paginar.SkipPagina(paginar))
                .Take(paginar.QtdeItensPagina)
                .ToList();
        }

        /// <summary>
        /// Busca perfil de forma assíncrona
        /// </summary>
        /// <param name="paginar">Propriedades para paginação</param>
        /// <returns>Lista de perfil, limitados por paginação</returns>
        public async Task<IEnumerable<Perfil>> GetAsync(Pagination paginar)
        {
            return await _context.Perfil
                .OrderBy(o => o.Nome)
                .Skip(paginar.SkipPagina(paginar))
                .Take(paginar.QtdeItensPagina)
                .ToListAsync();
        }

        #endregion
    }
}
