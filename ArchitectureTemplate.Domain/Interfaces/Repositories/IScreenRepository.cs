using System.Collections.Generic;
using System.Threading.Tasks;
using ArchitectureTemplate.Domain.DataEntities;
using ArchitectureTemplate.Infraestrutura.CrossCutting.Support.Extensions;

namespace ArchitectureTemplate.Domain.Interfaces.Repositories
{
    public interface IScreenRepository : IRepositoryBase<Screen>
    {
        void AddOrUpdate(IList<Screen> entityList, long userId);
        IEnumerable<Screen> GetAllWithDapper(); 
        IEnumerable<Screen> Get(Pagination pagination); 
        Task<IEnumerable<Screen>> GetAsync(Pagination pagination);
        void Remove(int screenId, long userId);
    }
}
