using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArchitectureTemplate.Domain.DataEntities;
using ArchitectureTemplate.Domain.Interfaces.Repositories;
using ArchitectureTemplate.Domain.Interfaces.Services;
using ArchitectureTemplate.Infraestrutura.CrossCutting.Support.Extensions;

namespace ArchitectureTemplate.Domain.Services
{
    public class MenuService : ServiceBase<Menu>, IMenuService
    {
        #region Fields

        private readonly IMenuRepository _menuRepository;

        #endregion
        
        #region Constructors

        public MenuService(IMenuRepository menuRepository)
            : base(menuRepository)
        {
            _menuRepository = menuRepository;
        }

        #endregion

        #region Methods
       
        public void Synchronize(IEnumerable<string> menuList, long userId)
        {
            IList<Menu> entityList = menuList
                .Select(menu => menu.Split('|'))
                .Select(splitMenu => new Menu
                {
                    Id = Convert.ToInt32(splitMenu[0]),
                    Nome = splitMenu[1].Trim()
                })
                .ToList();

            IList<Menu> menus = _menuRepository
                .GetAllWithDapper()
                .ToList();

            var menusNaoExistentes = entityList
                .Where(w => !menus.Select(s => s.Id).Contains(w.Id))
                .ToList();

            var menusExistentes = entityList
                .Where(w => menus.Select(s => s.Id).Contains(w.Id))
                .ToList();

            var menusDelete = menus
                .Where(w => !entityList.Select(s => s.Id).Contains(w.Id))
                .ToList();

            if (menusNaoExistentes.Any())
            {
                _menuRepository.AddRange(menusNaoExistentes, userId);
            }

            if (menusExistentes.Any())
            {
                _menuRepository.UpdateList(menusExistentes, userId);
            }

            foreach (var menu in menusDelete)
            {
                _menuRepository.Remove(menu.Id, userId);
            }
        }

        public IEnumerable<Menu> Get(Pagination pagination)
        {
            return _menuRepository.Get(pagination);
        }

        public async Task<IEnumerable<Menu>> GetAsync(Pagination pagination)
        {
            return await _menuRepository.GetAsync(pagination);
        }
        
        public IEnumerable<int> GetIdsPorProfile(int ProfileId)
        {
            return _menuRepository.GetIdsPorProfile(ProfileId);
        }

        public IEnumerable<ProfilePorMenu> GetPorProfile(int ProfileId)
        {
            return _menuRepository.GetPorProfile(ProfileId);
        }

        public void EnableOrDisabled(int ProfileId, int menuId, long permissaoId, long userId)
        {
            if (permissaoId != 0)
            {
                _menuRepository.RemoveProfilePorMenu(permissaoId, userId);
            }
            else
            {
                var pm = new ProfilePorMenu
                {
                    ProfileId = ProfileId,
                    MenuId = menuId,
                };

                _menuRepository.AddProfilePorMenu(pm, userId);
            }
        }

        public void EnableOrDisabledAll(int ProfileId, bool ativar, long userId)
        {
            var ProfilePorMenus = _menuRepository.GetPorProfile(ProfileId);

            if (ativar == false)
            {
                var delete = ProfilePorMenus
                    .Where(w => w.Id != 0)
                    .ToList();

                foreach (var pm in delete)
                {
                    _menuRepository.RemoveProfilePorMenu(pm.Id, userId);
                }
            }
            else
            {
                var create = ProfilePorMenus
                    .Where(w => w.Id == 0)
                    .Select(s => s.Menu)
                    .ToList();

                var insertList = create.Select(item => new ProfilePorMenu
                {
                    ProfileId = ProfileId, MenuId = item.Id
                }).ToList();

                if (insertList.Any())
                    _menuRepository.AddRangeProfilePorMenu(insertList, userId);
            }
        }

        public void Update(Menu entity, long userId)
        {
            var menu = _menuRepository.GetId(entity.Id);
            menu.Nome = entity.Nome;

            _menuRepository.Update(menu, userId, true);
        }

        #endregion
    }
}