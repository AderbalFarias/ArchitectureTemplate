using System.Collections.Generic;
using System.Threading.Tasks;
using ArchitectureTemplate.Domain.Interfaces.Repositories;
using ArchitectureTemplate.Domain.Interfaces.Services;

namespace ArchitectureTemplate.Domain.Services
{
    public class DictionaryAllService : IDictionaryAllService
    {
        #region Fields

        private readonly IDictionaryAllRepository _dictionaryAllRepository;

        #endregion

        #region Constructors

        public DictionaryAllService(IDictionaryAllRepository dictionaryAllRepository)
        {
            _dictionaryAllRepository = dictionaryAllRepository;
        }

        #endregion

        #region Methods

        public IDictionary<int, string> GetTipoHierarquiaDictionary()
        {
            return _dictionaryAllRepository.GetTipoHierarquiaDictionary();
        }

        public async Task<IDictionary<int, string>> GetTipoHierarquiaDictionaryAsync()
        {
            return await _dictionaryAllRepository.GetTipoHierarquiaDictionaryAsync();
        }

        public IDictionary<int, string> GetPerfilDictionary()
        {
            return _dictionaryAllRepository.GetPerfilDictionary();
        }

        public async Task<IDictionary<int, string>> GetPerfilDictionaryAsync()
        {
            return await _dictionaryAllRepository.GetPerfilDictionaryAsync();
        }

        #endregion
    }
}