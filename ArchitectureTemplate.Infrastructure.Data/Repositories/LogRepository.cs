using ArchitectureTemplate.Infraestrutura.CrossCutting.Support.Extensions;
using ArchitectureTemplate.Infrastructure.Data.DapperConfig;
using ArchitectureTemplate.Infrastructure.Data.EntityConfig;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using ArchitectureTemplate.Domain.DataEntities;
using ArchitectureTemplate.Domain.Interfaces.Repositories;

namespace ArchitectureTemplate.Infrastructure.Data.Repositories
{
    public class LogRepository : ILogRepository, IDisposable
    {
        #region Fields

        private readonly EntityContext _context = new EntityContext();
        private readonly SqlConnection _contextDapper = new DapperContext().SqlConnection();
        private readonly Log _log = new Log();

        #endregion

        #region Methods

        public void Add(Log entity)
        {
            _context.Log.Add(entity);
            _context.SaveChanges();
        }

        public IEnumerable<Log> Get()
        {
            return _contextDapper
                .Query<Log>("select * from Log")
                .ToList();
        }

        public IEnumerable<Log> Get(Pagination paginar, long? testId = null, string key = null)
        {
            //return _context.Log
            //    .Include(i => i.LogType)
            //    .Include(i => i.User)
            //    .Include(i => i.Screen)
            //    .Skip(paginar.SkipPagina(paginar))
            //    .Take(paginar.QtdeItensPagina)
            //    .ToList();

            var resultList = _contextDapper.Query<Log, LogType, User, Log>($@"
                    select l.Id, l.ScreenId, l.Mensagem, l.Conteudo, l.DataCadastro, l.NomeClasse,
                        l.LogTypeId, lt.Id, lt.Descricao,
                        l.UserId, u.Id, u.Nome, u.Login
                    from Log l
                    inner join LogType lt on lt.Id = l.LogTypeId                
                    left join User u on u.Id = l.UserId
                    where {_log.Where(testId, key)} 
                    {paginar.GeneretePaginationSql(paginar, "l.Id")}",
                    (l, lt, u) =>
                    {
                        l.LogType = lt;
                        l.User = u;
                        return l;
                    }, splitOn: "LogTypeId, UserId")
                .AsQueryable();

            return resultList.ToList();
        }

        public int Count(long? testId = null, string key = null)
        {
            return _contextDapper
                .Query<int>($@"select Count(l.Id)
                    from Log l
                    inner join LogType lt on lt.Id = l.LogTypeId                
                    left join User u on u.Id = l.UserId
                    where {_log.Where(testId, key)}")
                .Single();
        }

        public void Dispose()
        {
            _contextDapper.Close();
            _context.Dispose();
        }

        #endregion
    }
}
