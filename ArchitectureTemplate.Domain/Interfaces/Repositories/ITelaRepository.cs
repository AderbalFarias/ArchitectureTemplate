using System.Collections.Generic;
using System.Threading.Tasks;
using ArchitectureTemplate.Domain.DataEntities;
using ArchitectureTemplate.Infraestrutura.CrossCutting.Support.Extensions;

namespace ArchitectureTemplate.Domain.Interfaces.Repositories
{
    public interface ITelaRepository : IRepositoryBase<Tela>
    {
        void AddOrUpdate(IList<Tela> entityList, long userId);
        IEnumerable<Tela> GetAllWithDapper(); 
        IEnumerable<Tela> Get(Pagination pagination); 
        Task<IEnumerable<Tela>> GetAsync(Pagination pagination);
        void Remove(int telaId, long userId);
    }
}
