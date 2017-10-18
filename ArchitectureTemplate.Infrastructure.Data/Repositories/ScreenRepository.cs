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
    public class ScreenRepository : RepositoryBase<Screen>, IScreenRepository
    {
        #region Fields

        private readonly SqlConnection _contextDapper = new DapperContext().SqlConnection();
        private readonly ILogRepository _logRepository = new LogRepository();

        #endregion

        #region Methods

        public void AddOrUpdate(IList<Screen> entityList, long userId)
        {
            if (entityList.Count == 0) return;
            foreach (var entity in entityList)
            {
                _context.Screen.AddOrUpdate(entity);
            }

            _context.SaveChanges();
            _logRepository.Add(new Log().GeneratedForEntity(userId, entityList, LogTypeResource.Update, true));
        }

        public IEnumerable<Screen> GetAllWithDapper()
        {
            return _contextDapper
                .Query<Screen>($"select * from Screen")
                .ToList();
        }

        public IEnumerable<Screen> Get(Pagination pagination)
        {
            return _contextDapper
                .Query<Screen>($"select * from Screen {pagination.GeneretePaginationSql(pagination, "Id")}")
                .ToList();
        }

        public async Task<IEnumerable<Screen>> GetAsync(Pagination pagination)
        {
            return await _contextDapper
                .QueryAsync<Screen>($"select * from Screen {pagination.GeneretePaginationSql(pagination, "Id")}");
        }

        public void Remove(int screenId, long userId)
        {
            var screen = _context.Screen
                .Include(i => i.ProfilePorScreen)
                .First(f => f.Id == screenId);

            _context.Screen.Remove(screen);

            _context.SaveChanges();
            _logRepository.Add(new Log().GeneratedForEntity(userId, screen, LogTypeResource.Delete, true));
        }

        #endregion
    }
}
