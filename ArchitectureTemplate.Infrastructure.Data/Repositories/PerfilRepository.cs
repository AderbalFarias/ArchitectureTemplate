using ArchitectureTemplate.Infraestrutura.CrossCutting.Support.Extensions;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using ArchitectureTemplate.Domain.DataEntities;
using ArchitectureTemplate.Domain.Interfaces.Repositories;

namespace ArchitectureTemplate.Infrastructure.Data.Repositories
{
    public class ProfileRepository : RepositoryBase<Profile>, IProfileRepository
    {
        #region Fields

        #endregion

        #region Methods

        /// <summary>
        /// Busca Profile
        /// </summary>
        /// <returns>Dicionário de Profile com código e descrição</returns>
        public IDictionary<int, string> GetDictionary()
        {
            return _context.Profile
                .Where(w => w.Ativo)
                .ToDictionary(k => k.Id, v => v.Nome);
        }

        /// <summary>
        /// Busca Profile de forma assíncrona
        /// </summary>
        /// <returns>Dicionário de Profile com código e descrição</returns>
        public async Task<IDictionary<int, string>> GetDictionaryAsync()
        {
            return await _context.Profile
                .Where(w => w.Ativo)
                .ToDictionaryAsync(k => k.Id, v => v.Nome);
        }

        /// <summary>
        /// Busca Profile
        /// </summary>
        /// <param name="paginar">Propriedades para paginação</param>
        /// <returns>Lista de Profile, limitados por paginação</returns>
        public IEnumerable<Profile> Get(Pagination paginar)
        {
            return _context.Profile
                .OrderBy(o => o.Nome)
                .Skip(paginar.SkipPagina(paginar))
                .Take(paginar.QtdeItensPagina)
                .ToList();
        }

        /// <summary>
        /// Busca Profile de forma assíncrona
        /// </summary>
        /// <param name="paginar">Propriedades para paginação</param>
        /// <returns>Lista de Profile, limitados por paginação</returns>
        public async Task<IEnumerable<Profile>> GetAsync(Pagination paginar)
        {
            return await _context.Profile
                .OrderBy(o => o.Nome)
                .Skip(paginar.SkipPagina(paginar))
                .Take(paginar.QtdeItensPagina)
                .ToListAsync();
        }

        #endregion
    }
}
