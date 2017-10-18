using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Data.SqlClient;
using System.Linq;
using ArchitectureTemplate.Domain.DataEntities;
using ArchitectureTemplate.Domain.Interfaces.Repositories;
using Dapper;
using ArchitectureTemplate.Infraestrutura.CrossCutting.Support.Resources;
using ArchitectureTemplate.Infrastructure.Data.DapperConfig;
using ArchitectureTemplate.Infrastructure.Data.EntityConfig;

namespace ArchitectureTemplate.Infrastructure.Data.Repositories
{
    public class PermissaoRepository : IPermissaoRepository, IDisposable
    {
        #region Fields
        
        private readonly EntityContext _context = new EntityContext();
        private readonly SqlConnection _contextDapper = new DapperContext().SqlConnection();
        private readonly ILogRepository _logRepository = new LogRepository();

        #endregion

        #region Methods

        public bool AllowAccess(int ProfileId, string controllerName, string accessType)
        {
            var telaId = _contextDapper
                .Query<int?>($"select Id from Tela where ControllerName = '{controllerName}'")
                .FirstOrDefault();

            if (telaId == null)
                return false;

            var select = $"select pt.Id from ProfilePorTela pt where pt.ProfileId = {ProfileId} " +
                $"and pt.TelaId = {telaId.Value} and pt.[{accessType}] = 'True'";

            return _contextDapper.Query<long>(select).Any();
        }

        public string GetToken(long userId)
        {
            return _contextDapper
                .Query<string>($"select Token from Usuario where Id = {userId}")
                .FirstOrDefault();
        }

        public IEnumerable<ProfilePorTela> GetAll()
        {
            var resultList = _contextDapper.Query<ProfilePorTela, Tela, Profile, ProfilePorTela>($@"
                    select pt.Id, pt.[Create], pt.[Read], pt.[Update], pt.[Delete],
                        t.Id, t.Nome, t.ControllerName, t.DataCadastro, t.Ativo, t.[Create], t.[Read], t.[Update], t.[Delete],
                        p.Id, p.Nome, p.Ativo, p.Solicitante, p.DataCadastro
                    from ProfilePorTela pt
                    left join Tela t on t.Id = pt.TelaId 
                    left join Profile p on p.Id = pt.ProfileId 
                    order by p.Nome, t.Nome asc",
                    (pt, t, p) =>
                    {
                        pt.Tela = t;
                        pt.Profile = p;
                        return pt;
                    }, splitOn: "Id, Id")
                .AsQueryable();

            return resultList.ToList();
        }

        public IEnumerable<ProfilePorTela> GetProfilePorTela(int ProfileId)
        {
            var resultList = _contextDapper.Query<ProfilePorTela, Tela, ProfilePorTela>($@"
                    select pt.Id, pt.[Create], pt.[Read], pt.[Update], pt.[Delete],
                        t.Id, t.Nome, t.ControllerName, t.DataCadastro, t.Ativo, t.[Create], t.[Read], t.[Update], t.[Delete]
                    from Tela t
                    left join ProfilePorTela pt on pt.TelaId = t.Id and pt.ProfileId = {ProfileId}",
                    (pt, t) =>
                    {
                        pt.Tela = t;
                        return pt;
                    }, "Id")
                .AsQueryable();

            return resultList.ToList();
        }

        public ProfilePorTela GetProfilePorTela(long id)
        {
            return _contextDapper
                .Query<ProfilePorTela>($"select * from ProfilePorTela where Id = {id}")
                .Single();
        }

        public ProfilePorTela GetTela(int telaId)
        {
            return _contextDapper
                .Query<ProfilePorTela>($"select * from Tela where Id = {telaId}")
                .Single();
        }

        public void EnableOrDisabled(ProfilePorTela entity, long userId)
        {
            _context.ProfilePorTela.AddOrUpdate(entity);
            _context.SaveChanges();

            _logRepository.Add(new Log().GeneratedForEntity(userId, entity, entity.Id != 0 ? LogTypeResource.Update : LogTypeResource.Insert));
        }

        public void Dispose()
        {
            _context.Dispose();
            _contextDapper.Close();
        }
        
        #endregion
    }
}
