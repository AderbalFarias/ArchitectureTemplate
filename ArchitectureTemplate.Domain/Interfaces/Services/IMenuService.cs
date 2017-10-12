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
        IEnumerable<int> GetIdsPorPerfil(int perfilId);
        IEnumerable<PerfilPorMenu> GetPorPerfil(int perfilId);
        void EnableOrDisabled(int perfilId, int menuId, long permissaoId, long userId);
        void EnableOrDisabledAll(int perfilId, bool ativar, long userId);
        void Update(Menu entity, long userId);
    }
}
