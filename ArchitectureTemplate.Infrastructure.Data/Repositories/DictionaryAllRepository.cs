using ArchitectureTemplate.Infrastructure.Data.EntityConfig;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using ArchitectureTemplate.Domain.Interfaces.Repositories;

namespace ArchitectureTemplate.Infrastructure.Data.Repositories
{
    public class DictionaryAllRepository : IDictionaryAllRepository
    {
        #region Fields

        private readonly EntityContext _context = new EntityContext();

        #endregion

        #region Methods

        /// <summary>
        /// Busca tipo de hierarquia
        /// </summary>
        /// <returns>Dicionário contendo código e descrição</returns>
        public IDictionary<int, string> GetTipoHierarquiaDictionary()
        {
            return _context.TipoHierarquia
                .OrderBy(o => o.Descricao)
                .ToDictionary(k => k.Id, v => v.Descricao);
        }

        /// <summary>
        /// Busca tipo de hierarquia de forma assíncrona
        /// </summary>
        /// <returns>Dicionário contendo código e descrição</returns>
        public async Task<IDictionary<int, string>> GetTipoHierarquiaDictionaryAsync()
        {
            return await _context.TipoHierarquia
                .OrderBy(o => o.Descricao)
                .ToDictionaryAsync(k => k.Id, v => v.Descricao);
        }

        /// <summary>
        /// Busca perfil
        /// </summary>
        /// <returns>Dicionário de perfil com código e descrição</returns>
        public IDictionary<int, string> GetPerfilDictionary()
        {
            return _context.Perfil
                .Where(w => w.Ativo)
                .OrderBy(o => o.Nome)
                .ToDictionary(k => k.Id, v => v.Nome);
        }

        /// <summary>
        /// Busca perfil de forma assíncrona
        /// </summary>
        /// <returns>Dicionário de perfil com código e descrição</returns>
        public async Task<IDictionary<int, string>> GetPerfilDictionaryAsync()
        {
            return await _context.Perfil
                .Where(w => w.Ativo)
                .OrderBy(o => o.Nome)
                .ToDictionaryAsync(k => k.Id, v => v.Nome);
        }

        #endregion
    }
}
