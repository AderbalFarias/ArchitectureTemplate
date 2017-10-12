using System.Collections.Generic;
using System.Threading.Tasks;
using ArchitectureTemplate.Domain.DataEntities;
using ArchitectureTemplate.Infraestrutura.CrossCutting.Support.Extensions;

namespace ArchitectureTemplate.Domain.Interfaces.Services
{
    public interface IPerfilService : IServiceBase<Perfil>
    {
        IDictionary<int, string> GetDictionary();
        Task<IDictionary<int, string>> GetDictionaryAsync();
        IEnumerable<Perfil> Get(Pagination paginar);
        Task<IEnumerable<Perfil>> GetAsync(Pagination paginar);
        void DisableOrEnable(long perfilId, long userId);
    }
}
