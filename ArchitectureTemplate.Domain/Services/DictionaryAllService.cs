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

        public IDictionary<int, string> GetHierarchyTypeDictionary()
        {
            return _dictionaryAllRepository.GetHierarchyTypeDictionary();
        }

        public async Task<IDictionary<int, string>> GetHierarchyTypeDictionaryAsync()
        {
            return await _dictionaryAllRepository.GetHierarchyTypeDictionaryAsync();
        }

        public IDictionary<int, string> GetProfileDictionary()
        {
            return _dictionaryAllRepository.GetProfileDictionary();
        }

        public async Task<IDictionary<int, string>> GetProfileDictionaryAsync()
        {
            return await _dictionaryAllRepository.GetProfileDictionaryAsync();
        }

        #endregion
    }
}