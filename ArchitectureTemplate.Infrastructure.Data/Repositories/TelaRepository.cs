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
    public class TelaRepository : RepositoryBase<Tela>, ITelaRepository
    {
        #region Fields

        private readonly SqlConnection _contextDapper = new DapperContext().SqlConnection();
        private readonly ILogRepository _logRepository = new LogRepository();

        #endregion

        #region Methods

        public void AddOrUpdate(IList<Tela> entityList, long userId)
        {
            if (entityList.Count == 0) return;
            foreach (var entity in entityList)
            {
                _context.Tela.AddOrUpdate(entity);
            }

            _context.SaveChanges();
            _logRepository.Add(new Log().GeneratedForEntity(userId, entityList, LogTypeResource.Update, true));
        }

        public IEnumerable<Tela> GetAllWithDapper()
        {
            return _contextDapper
                .Query<Tela>($"select * from Tela")
                .ToList();
        }

        public IEnumerable<Tela> Get(Pagination pagination)
        {
            return _contextDapper
                .Query<Tela>($"select * from Tela {pagination.GeneretePaginationSql(pagination, "Id")}")
                .ToList();
        }

        public async Task<IEnumerable<Tela>> GetAsync(Pagination pagination)
        {
            return await _contextDapper
                .QueryAsync<Tela>($"select * from Tela {pagination.GeneretePaginationSql(pagination, "Id")}");
        }

        public void Remove(int telaId, long userId)
        {
            var tela = _context.Tela
                .Include(i => i.PerfilPorTela)
                .First(f => f.Id == telaId);

            _context.Tela.Remove(tela);

            _context.SaveChanges();
            _logRepository.Add(new Log().GeneratedForEntity(userId, tela, LogTypeResource.Delete, true));
        }

        #endregion
    }
}
