using ArchitectureTemplate.Domain.DataEntities;
using ArchitectureTemplate.Infraestrutura.CrossCutting.Support.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ArchitectureTemplate.Domain.Interfaces.Services
{
    public interface IMenuService : IServiceBase<Menu>
    {
        void Synchronize(IEnumerable<string> menuList, long userId);
        IEnumerable<Menu> Get(Pagination pagination);
        Task<IEnumerable<Menu>> GetAsync(Pagination pagination);
        IEnumerable<int> GetIdsPorProfile(int profileId);
        IEnumerable<ProfileForMenu> GetPorProfile(int profileId);
        void EnableOrDisabled(int profileId, int menuId, long permissionId, long userId);
        void EnableOrDisabledAll(int profileId, bool ativar, long userId);
        void Update(Menu entity, long userId);
    }
}
