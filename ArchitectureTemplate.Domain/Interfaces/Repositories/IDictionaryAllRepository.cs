using System.Collections.Generic;
using System.Threading.Tasks;

namespace ArchitectureTemplate.Domain.Interfaces.Repositories
{
    public interface IDictionaryAllRepository
    {
        IDictionary<int, string> GetTipoHierarquiaDictionary();
        Task<IDictionary<int, string>> GetTipoHierarquiaDictionaryAsync();

        IDictionary<int, string> GetPerfilDictionary();
        Task<IDictionary<int, string>> GetPerfilDictionaryAsync();
    }
}
