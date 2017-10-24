using ArchitectureTemplate.Domain.DataEntities;
using ArchitectureTemplate.Domain.Interfaces.Repositories;
using ArchitectureTemplate.Infraestrutura.CrossCutting.Support.Extensions;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace ArchitectureTemplate.Infrastructure.Data.Repositories
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        #region Fields

        //private readonly ILogRepository _logRepository = new LogRepository();

        #endregion

        #region Methods

        public IEnumerable<User> Get(Pagination paginar)
        {
            return _context.User
                .Include(i => i.Profile)
                .Include(i => i.Hierarchy)
                .OrderBy(o => o.Nome)
                .Skip(paginar.SkipPagina(paginar))
                .Take(paginar.QtdeItensPagina)
                .ToList();
        }

        public async Task<IEnumerable<User>> GetAsync(Pagination paginar)
        {
            return await _context.User
                .Include(i => i.Profile)
                .Include(i => i.Hierarchy)
                .OrderBy(o => o.Nome)
                .Skip(paginar.SkipPagina(paginar))
                .Take(paginar.QtdeItensPagina)
                .ToListAsync();
        }

        /// <param name="userId"></param>
        /// <param name="userAutenticationId">usuário logado</param>
        /// <param name="ativo"></param>
        public void DisableOrEnable(long userId, long userAutenticationId, bool ativo)
        {
            var user = _context.User
                .Find(userId);

            if (user != null)
            {
                user.Ativo = ativo;
                _context.SaveChanges();

                //_logRepository.Add(new Log().GeneratedForEntity(userAutenticationId, user, LogTypeResource.Update));
            }
        }

        public User Login(string user, string password, string codRecover)
        {
            var userData = _context.User
                .Include(i => i.Profile)
                .Include(i => i.Hierarchy)
                .FirstOrDefault(u => u.Ativo && u.Login == user && u.Senha == password);

            if (user == null)
                return _context.User
                    .Include(i => i.Profile)
                    .Include(i => i.Hierarchy)
                    .FirstOrDefault(u => u.Ativo && u.Login == user && u.Senha == codRecover && u.CodigoRecover == codRecover);

            return userData;
        }

        public User RecoverPassword(string email, string codigoRecover, string passRecover)
        {
            var user = _context.User
                .FirstOrDefault(u => u.Email == email);

            if (user == null)
                return null;

            user.CodigoRecover = codigoRecover;
            user.Senha = passRecover;

            _context.SaveChanges();

            return user;
        }

        public void ResetPassword(string login, string codRecover, string newPassword)
        {
            var user = _context.User
                .Single(u => u.Login == login && u.CodigoRecover == codRecover);

            user.CodigoRecover = null;
            user.Senha = newPassword;
            _context.SaveChanges();
        }

        public void EditPassword(long userId, string password, string newPassword)
        {
            var user = _context.User
                .Single(u => u.Id == userId && u.Senha == password);

            user.Senha = newPassword;
            _context.SaveChanges();

            //_logRepository.Add(new Log().GeneratedForEntity(userId, user, LogTypeResource.Update));
        }

        public string GetPassword(long userId)
        {
            return _context.User
                .First(u => u.Id == userId).Senha;
        }

        public IList<string> GetEmails(IEnumerable<int> profilesId)
        {
            return _context.User.Where(w => profilesId.Contains(w.ProfileId)).Select(s => s.Email).ToList();
        }

        public List<string> GetTelefones(IEnumerable<int> profilesId)
        {
            return _context.User.Where(w => profilesId.Contains(w.ProfileId)).Select(s => s.Telefone).ToList();
        }

        public void InsertToken(long userId, string token)
        {
            var user = _context.User
                .Single(u => u.Id == userId);

            user.Token = token;
            _context.SaveChanges();

            //_logRepository.Add(new Log().GeneratedForEntity(userId, user, LogTypeResource.Update));
        }

        #endregion
    }
}
