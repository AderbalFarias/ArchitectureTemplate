using System.Collections.Generic;
using System.Threading.Tasks;
using ArchitectureTemplate.Business.DataEntities;
using ArchitectureTemplate.Infraestrutura.CrossCutting.Support.Extensions;

namespace ArchitectureTemplate.Business.Interfaces.Repositories
{
    public interface IMenuRepository : IRepositoryBase<Menu>
    {
        void AddPerfilPorMenu(PerfilPorMenu entity, long userId);
        void AddRangePerfilPorMenu(IList<PerfilPorMenu> entity, long userId);
        void UpdateList(IList<Menu> entityList, long userId);
        IEnumerable<Menu> GetAllWithDapper(); 
        IEnumerable<Menu> Get(Pagination pagination); 
        Task<IEnumerable<Menu>> GetAsync(Pagination pagination);
        IEnumerable<int> GetIdsPorPerfil(int perfilId);
        IEnumerable<PerfilPorMenu> GetPorPerfil(int perfilId);
        void Remove(int menuId, long userId);
        void RemovePerfilPorMenu(long perfilPorMenuId, long userId);
    }
}
