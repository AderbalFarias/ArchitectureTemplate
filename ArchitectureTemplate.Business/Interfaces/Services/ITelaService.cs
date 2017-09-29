using System.Collections.Generic;
using System.Threading.Tasks;
using ArchitectureTemplate.Business.DataEntities;
using ArchitectureTemplate.Infraestrutura.CrossCutting.Support.Extensions;

namespace ArchitectureTemplate.Business.Interfaces.Services
{
    public interface ITelaService : IServiceBase<Tela>
    {
        void Synchronize(IList<Tela> entityList, long userId);
        IEnumerable<Tela> Get(Pagination pagination); 
        Task<IEnumerable<Tela>> GetAsync(Pagination pagination);
        void Update(Tela entity, long userId);
    }
}
