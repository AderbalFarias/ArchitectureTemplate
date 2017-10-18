using System.Collections.Generic;
using System.Threading.Tasks;
using ArchitectureTemplate.Domain.DataEntities;
using ArchitectureTemplate.Infraestrutura.CrossCutting.Support.Extensions;

namespace ArchitectureTemplate.Domain.Interfaces.Services
{
    public interface IMenuService : IServiceBase<Menu>
    {
        void Synchronize(IEnumerable<string> menuList, long userId);
        IEnumerable<Menu> Get(Pagination pagination); 
        Task<IEnumerable<Menu>> GetAsync(Pagination pagination);
        IEnumerable<int> GetIdsPorProfile(int ProfileId);
        IEnumerable<ProfilePorMenu> GetPorProfile(int ProfileId);
        void EnableOrDisabled(int ProfileId, int menuId, long permissaoId, long userId);
        void EnableOrDisabledAll(int ProfileId, bool ativar, long userId);
        void Update(Menu entity, long userId);
    }
}
