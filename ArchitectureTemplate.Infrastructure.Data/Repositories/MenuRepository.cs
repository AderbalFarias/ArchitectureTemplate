using ArchitectureTemplate.Domain.DataEntities;
using ArchitectureTemplate.Domain.Interfaces.Repositories;
using ArchitectureTemplate.Infraestrutura.CrossCutting.Support.Extensions;
using ArchitectureTemplate.Infraestrutura.CrossCutting.Support.Resources;
using ArchitectureTemplate.Infrastructure.Data.DapperConfig;
using Dapper;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ArchitectureTemplate.Infrastructure.Data.Repositories
{
    public class MenuRepository : RepositoryBase<Menu>, IMenuRepository
    {
        #region Fields

        private readonly SqlConnection _contextDapper = new DapperContext().SqlConnection();
        private readonly ILogRepository _logRepository = new LogRepository();

        #endregion

        #region Methods

        public void AddProfileForMenu(ProfileForMenu entity, long userId)
        {
            _context.ProfileForMenu.Add(entity);
            _context.SaveChanges();

            _logRepository.Add(new Log().GeneratedForEntity(userId, entity, LogTypeResource.Insert));
        }

        public void AddRangeProfileForMenu(IList<ProfileForMenu> entity, long userId)
        {
            _context.ProfileForMenu.AddRange(entity);
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

        public IEnumerable<int> GetIdsPorProfile(int profileId)
        {
            return _contextDapper
                .Query<int>($"select distinct m.Id from Menu m " +
                    $"inner join ProfileForMenu pm on pm.MenuId = m.Id " +
                    $"where pm.ProfileId = {profileId} " +
                    $"order by m.Id")
                .ToList();
        }


        public IEnumerable<ProfileForMenu> GetPorProfile(int profileId)
        {
            var resultList = _contextDapper.Query<ProfileForMenu, Menu, ProfileForMenu>($@"
                select pm.Id, pm.MenuId, pm.ProfileId, 
                    m.Id, m.Nome 
                from Menu m
                left join ProfileForMenu pm on pm.MenuId = m.Id and pm.ProfileId = {profileId}",
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
                .Include(i => i.ProfileForMenu)
                .First(f => f.Id == menuId);

            _context.Menu.Remove(menu);

            _context.SaveChanges();
            _logRepository.Add(new Log().GeneratedForEntity(userId, menu, LogTypeResource.Delete, true));
        }

        public void RemoveProfileForMenu(long profileForMenuId, long userId)
        {
            var profileForMenu = _context.ProfileForMenu
                .First(f => f.Id == profileForMenuId);

            _context.ProfileForMenu.Remove(profileForMenu);

            _context.SaveChanges();
            _logRepository.Add(new Log().GeneratedForEntity(userId, profileForMenu, LogTypeResource.Delete, true));
        }

        #endregion
    }
}
