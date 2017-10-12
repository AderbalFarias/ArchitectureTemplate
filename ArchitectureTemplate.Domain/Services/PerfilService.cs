using System.Collections.Generic;
using System.Threading.Tasks;
using ArchitectureTemplate.Domain.DataEntities;
using ArchitectureTemplate.Domain.Interfaces.Repositories;
using ArchitectureTemplate.Domain.Interfaces.Services;
using ArchitectureTemplate.Infraestrutura.CrossCutting.Support.Extensions;

namespace ArchitectureTemplate.Domain.Services
{
    public class PerfilService : ServiceBase<Perfil>, IPerfilService
    {
        #region Fields

        private readonly IPerfilRepository _perfilRepository;

        #endregion
        
        #region Constructors

        public PerfilService(IPerfilRepository perfilRepository)
            : base(perfilRepository)
        {
            _perfilRepository = perfilRepository;
        }

        #endregion

        #region Methods

        public IDictionary<int, string> GetDictionary()
        {
            return _perfilRepository.GetDictionary();
        }

        public async Task<IDictionary<int, string>> GetDictionaryAsync()
        {
            return await _perfilRepository.GetDictionaryAsync();

        }

        public IEnumerable<Perfil> Get(Pagination paginar)
        {
            return _perfilRepository.Get(paginar);
        }

        public async Task<IEnumerable<Perfil>> GetAsync(Pagination paginar)
        {
            return await _perfilRepository.GetAsync(paginar);
        }

        public void DisableOrEnable(long perfilId, long userId)
        {
            var perfil = _perfilRepository.GetId(perfilId);
            perfil.Ativo = !perfil.Ativo;

            _perfilRepository.Update(perfil, userId, true);
        }

        #endregion
    }
}