using System.Collections.Generic;
using System.Threading.Tasks;

namespace ArchitectureTemplate.Domain.Interfaces.Repositories
{
    public interface IDictionaryAllRepository
    {
        IDictionary<int, string> GetHierarchyTypeDictionary();
        Task<IDictionary<int, string>> GetHierarchyTypeDictionaryAsync();

        IDictionary<int, string> GetProfileDictionary();
        Task<IDictionary<int, string>> GetProfileDictionaryAsync();
    }
}
