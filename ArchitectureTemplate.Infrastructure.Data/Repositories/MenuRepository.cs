using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using ArchitectureTemplate.Business.DataEntities;
using ArchitectureTemplate.Business.Interfaces.Repositories;
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

        public void AddPerfilPorMenu(PerfilPorMenu entity, long userId)
        {
            _context.PerfilPorMenu.Add(entity);
            _context.SaveChanges();

            _logRepository.Add(new Log().GeneratedForEntity(userId, entity, LogTypeResource.Insert));
        }

        public void AddRangePerfilPorMenu(IList<PerfilPorMenu> entity, long userId)
        {
            _context.PerfilPorMenu.AddRange(entity);
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

        public IEnumerable<int> GetIdsPorPerfil(int perfilId)
        {
            return _contextDapper
                .Query<int>($"select distinct m.Id from Menu m " +
                    $"inner join PerfilPorMenu pm on pm.MenuId = m.Id " +
                    $"where pm.PerfilId = {perfilId} " +
                    $"order by m.Id")
                .ToList();
        }


        public IEnumerable<PerfilPorMenu> GetPorPerfil(int perfilId)
        {
            var resultList = _contextDapper.Query<PerfilPorMenu, Menu, PerfilPorMenu>($@"
                select pm.Id, pm.MenuId, pm.PerfilId, 
                    m.Id, m.Nome 
                from Menu m
                left join PerfilPorMenu pm on pm.MenuId = m.Id and pm.PerfilId = {perfilId}",
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
                .Include(i => i.PerfilPorMenu)
                .First(f => f.Id == menuId);

            _context.Menu.Remove(menu);

            _context.SaveChanges();
            _logRepository.Add(new Log().GeneratedForEntity(userId, menu, LogTypeResource.Delete, true));
        }
        
        public void RemovePerfilPorMenu(long perfilPorMenuId, long userId)
        {
            var perfilPorMenu = _context.PerfilPorMenu
                .First(f => f.Id == perfilPorMenuId);

            _context.PerfilPorMenu.Remove(perfilPorMenu);

            _context.SaveChanges();
            _logRepository.Add(new Log().GeneratedForEntity(userId, perfilPorMenu, LogTypeResource.Delete, true));
        }

        #endregion
    }
}
