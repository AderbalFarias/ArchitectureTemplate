using System.Collections.Generic;
using System.Threading.Tasks;

namespace ArchitectureTemplate.Domain.Interfaces.Services
{
    public interface IDictionaryAllService
    {
        IDictionary<int, string> GetHierarchyTypeDictionary();
        Task<IDictionary<int, string>> GetHierarchyTypeDictionaryAsync();

        IDictionary<int, string> GetProfileDictionary();
        Task<IDictionary<int, string>> GetProfileDictionaryAsync();
    }
}
