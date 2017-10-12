using System.Collections.Generic;
using System.Threading.Tasks;

namespace ArchitectureTemplate.Domain.Interfaces.Services
{
    public interface IDictionaryAllService
    {
        IDictionary<int, string> GetTipoHierarquiaDictionary();
        Task<IDictionary<int, string>> GetTipoHierarquiaDictionaryAsync();

        IDictionary<int, string> GetPerfilDictionary();
        Task<IDictionary<int, string>> GetPerfilDictionaryAsync();
    }
}
