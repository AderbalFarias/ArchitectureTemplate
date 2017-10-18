using System.Collections.Generic;
using System.Threading.Tasks;
using ArchitectureTemplate.Domain.DataEntities;
using ArchitectureTemplate.Infraestrutura.CrossCutting.Support.Extensions;

namespace ArchitectureTemplate.Domain.Interfaces.Repositories
{
    public interface IMenuRepository : IRepositoryBase<Menu>
    {
        void AddProfilePorMenu(ProfilePorMenu entity, long userId);
        void AddRangeProfilePorMenu(IList<ProfilePorMenu> entity, long userId);
        void UpdateList(IList<Menu> entityList, long userId);
        IEnumerable<Menu> GetAllWithDapper(); 
        IEnumerable<Menu> Get(Pagination pagination); 
        Task<IEnumerable<Menu>> GetAsync(Pagination pagination);
        IEnumerable<int> GetIdsPorProfile(int ProfileId);
        IEnumerable<ProfilePorMenu> GetPorProfile(int ProfileId);
        void Remove(int menuId, long userId);
        void RemoveProfilePorMenu(long ProfilePorMenuId, long userId);
    }
}
