using ArchitectureTemplate.Business.Interfaces.Repositories;
using ArchitectureTemplate.Business.Interfaces.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ArchitectureTemplate.Business.Services
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