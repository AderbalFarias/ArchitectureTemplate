using System.Collections.Generic;
using System.Threading.Tasks;
using ArchitectureTemplate.Domain.DataEntities;
using ArchitectureTemplate.Infraestrutura.CrossCutting.Support.Extensions;

namespace ArchitectureTemplate.Domain.Interfaces.Services
{
    public interface IScreenService : IServiceBase<Screen>
    {
        void Synchronize(IList<Screen> entityList, long userId);
        IEnumerable<Screen> Get(Pagination pagination); 
        Task<IEnumerable<Screen>> GetAsync(Pagination pagination);
        void Update(Screen entity, long userId);
    }
}
