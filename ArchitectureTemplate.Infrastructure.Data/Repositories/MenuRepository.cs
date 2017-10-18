using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using ArchitectureTemplate.Domain.DataEntities;
using ArchitectureTemplate.Domain.Interfaces.Repositories;
using Dapper;
using ArchitectureTemplate.Infraestrutura.CrossCutting.Support.Extensions;
using ArchitectureTemplate.Infraestrutura.CrossCutting.Support.Resources;
using ArchitectureTemplate.Infrastructure.Data.DapperConfig;

namespace ArchitectureTemplate.Infrastructure.Data.Repositories
{
    public class MenuRepository : RepositoryBase<Menu>, IMenuRepository
    {
        #region Fields
        
        private readonly SqlConnection _contextDapper = new DapperContext().SqlConnection();
        private readonly ILogRepository _logRepository = new LogRepository();

        #endregion

        #region Methods

        public void AddProfilePorMenu(ProfilePorMenu entity, long userId)
        {
            _context.ProfilePorMenu.Add(entity);
            _context.SaveChanges();

            _logRepository.Add(new Log().GeneratedForEntity(userId, entity, LogTypeResource.Insert));
        }

        public void AddRangeProfilePorMenu(IList<ProfilePorMenu> entity, long userId)
        {
            _context.ProfilePorMenu.AddRange(entity);
            _context.SaveChanges();

            _logRepository.Add(new Log().GeneratedForEntity(userId, entity, LogTypeResource.Insert));
        }

        public void UpdateList(IList<Menu> entityList, long userId)
        {
            foreach (var entity in entityList)
            {
                _context.Menu.AddOrUpdate(entity);
            }

            _context.SaveChanges();
            _logRepository.Add(new Log().GeneratedForEntity(userId, entityList, LogTypeResource.Update, true));
        }

        public IEnumerable<Menu> GetAllWithDapper()
        {
            return _contextDapper
                .Query<Menu>($"select * from Menu order by Id")
                .ToList();
        }

        public IEnumerable<Menu> Get(Pagination pagination)
        {
            return _contextDapper
                .Query<Menu>($"select * from Menu {pagination.GeneretePaginationSql(pagination, "Id")}")
                .ToList();
        }

        public async Task<IEnumerable<Menu>> GetAsync(Pagination pagination)
        {
            return await _contextDapper
                .QueryAsync<Menu>($"select * from Menu {pagination.GeneretePaginationSql(pagination, "Id")}");
        }

        public IEnumerable<int> GetIdsPorProfile(int ProfileId)
        {
            return _contextDapper
                .Query<int>($"select distinct m.Id from Menu m " +
                    $"inner join ProfilePorMenu pm on pm.MenuId = m.Id " +
                    $"where pm.ProfileId = {ProfileId} " +
                    $"order by m.Id")
                .ToList();
        }


        public IEnumerable<ProfilePorMenu> GetPorProfile(int ProfileId)
        {
            var resultList = _contextDapper.Query<ProfilePorMenu, Menu, ProfilePorMenu>($@"
                select pm.Id, pm.MenuId, pm.ProfileId, 
                    m.Id, m.Nome 
                from Menu m
                left join ProfilePorMenu pm on pm.MenuId = m.Id and pm.ProfileId = {ProfileId}",
                (pm, m) =>
                {
                    pm.Menu = m;
                    return pm;
                }, "Id")
            .AsQueryable();

            return resultList.ToList();
        }
        
        public void Remove(int menuId, long userId)
        {
            var menu = _context.Menu
                .Include(i => i.ProfilePorMenu)
                .First(f => f.Id == menuId);

            _context.Menu.Remove(menu);

            _context.SaveChanges();
            _logRepository.Add(new Log().GeneratedForEntity(userId, menu, LogTypeResource.Delete, true));
        }
        
        public void RemoveProfilePorMenu(long ProfilePorMenuId, long userId)
        {
            var ProfilePorMenu = _context.ProfilePorMenu
                .First(f => f.Id == ProfilePorMenuId);

            _context.ProfilePorMenu.Remove(ProfilePorMenu);

            _context.SaveChanges();
            _logRepository.Add(new Log().GeneratedForEntity(userId, ProfilePorMenu, LogTypeResource.Delete, true));
        }

        #endregion
    }
}
