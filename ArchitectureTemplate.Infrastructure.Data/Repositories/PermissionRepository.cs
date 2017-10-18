using ArchitectureTemplate.Domain.DataEntities;
using ArchitectureTemplate.Domain.Interfaces.Repositories;
using ArchitectureTemplate.Infraestrutura.CrossCutting.Support.Resources;
using ArchitectureTemplate.Infrastructure.Data.DapperConfig;
using ArchitectureTemplate.Infrastructure.Data.EntityConfig;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Data.SqlClient;
using System.Linq;

namespace ArchitectureTemplate.Infrastructure.Data.Repositories
{
    public class PermissionRepository : IPermissionRepository, IDisposable
    {
        #region Fields

        private readonly EntityContext _context = new EntityContext();
        private readonly SqlConnection _contextDapper = new DapperContext().SqlConnection();
        private readonly ILogRepository _logRepository = new LogRepository();

        #endregion

        #region Methods

        public bool AllowAccess(int profileId, string controllerName, string accessType)
        {
            var screenId = _contextDapper
                .Query<int?>($"select Id from Screen where ControllerName = '{controllerName}'")
                .FirstOrDefault();

            if (screenId == null)
                return false;

            var select = $"select pt.Id from ProfilePorScreen pt where pt.ProfileId = {profileId} " +
                $"and pt.ScreenId = {screenId.Value} and pt.[{accessType}] = 'True'";

            return _contextDapper.Query<long>(select).Any();
        }

        public string GetToken(long userId)
        {
            return _contextDapper
                .Query<string>($"select Token from User where Id = {userId}")
                .FirstOrDefault();
        }

        public IEnumerable<ProfileForScreen> GetAll()
        {
            var resultList = _contextDapper.Query<ProfileForScreen, Screen, Profile, ProfileForScreen>($@"
                    select pt.Id, pt.[Create], pt.[Read], pt.[Update], pt.[Delete],
                        t.Id, t.Nome, t.ControllerName, t.DataCadastro, t.Ativo, t.[Create], t.[Read], t.[Update], t.[Delete],
                        p.Id, p.Nome, p.Ativo, p.Solicitante, p.DataCadastro
                    from ProfilePorScreen pt
                    left join Screen t on t.Id = pt.ScreenId 
                    left join Profile p on p.Id = pt.ProfileId 
                    order by p.Nome, t.Nome asc",
                    (pt, t, p) =>
                    {
                        pt.Screen = t;
                        pt.Profile = p;
                        return pt;
                    }, splitOn: "Id, Id")
                .AsQueryable();

            return resultList.ToList();
        }

        public IEnumerable<ProfileForScreen> GetProfilePorScreen(int profileId)
        {
            var resultList = _contextDapper.Query<ProfileForScreen, Screen, ProfileForScreen>($@"
                    select pt.Id, pt.[Create], pt.[Read], pt.[Update], pt.[Delete],
                        t.Id, t.Nome, t.ControllerName, t.DataCadastro, t.Ativo, t.[Create], t.[Read], t.[Update], t.[Delete]
                    from Screen t
                    left join ProfilePorScreen pt on pt.ScreenId = t.Id and pt.ProfileId = {profileId}",
                    (pt, t) =>
                    {
                        pt.Screen = t;
                        return pt;
                    }, "Id")
                .AsQueryable();

            return resultList.ToList();
        }

        public ProfileForScreen GetProfilePorScreen(long id)
        {
            return _contextDapper
                .Query<ProfileForScreen>($"select * from ProfilePorScreen where Id = {id}")
                .Single();
        }

        public ProfileForScreen GetScreen(int screenId)
        {
            return _contextDapper
                .Query<ProfileForScreen>($"select * from Screen where Id = {screenId}")
                .Single();
        }

        public void EnableOrDisabled(ProfileForScreen entity, long userId)
        {
            _context.ProfilePorScreen.AddOrUpdate(entity);
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
