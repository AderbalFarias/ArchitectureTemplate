using ArchitectureTemplate.Business.DataEntities;
using ArchitectureTemplate.Business.Interfaces.Repositories;
using ArchitectureTemplate.Infraestrutura.CrossCutting.Support.Extensions;
using ArchitectureTemplate.Infraestrutura.CrossCutting.Support.Resources;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace ArchitectureTemplate.Infrastructure.Data.Repositories
{
    public class UsuarioRepository : RepositoryBase<Usuario>, IUsuarioRepository
    {
        #region Fields

        private readonly ILogRepository _logRepository = new LogRepository();

        #endregion

        #region Methods

        public IEnumerable<Usuario> Get(Pagination paginar)
        {
            return _context.Usuario
                .Include(i => i.Perfil)
                .Include(i => i.Hierarquia)
                .OrderBy(o => o.Nome)
                .Skip(paginar.SkipPagina(paginar))
                .Take(paginar.QtdeItensPagina)
                .ToList();
        }

        public async Task<IEnumerable<Usuario>> GetAsync(Pagination paginar)
        {
            return await _context.Usuario
                .Include(i => i.Perfil)
                .Include(i => i.Hierarquia)
                .OrderBy(o => o.Nome)
                .Skip(paginar.SkipPagina(paginar))
                .Take(paginar.QtdeItensPagina)
                .ToListAsync();
        }

        /// <param name="usuarioId"></param>
        /// <param name="userId">usuário logado</param>
        /// <param name="ativo"></param>
        public void DisableOrEnable(long usuarioId, long userId, bool ativo)
        {
            var user = _context.Usuario
                .Find(usuarioId);

            user.Ativo = ativo;
            _context.SaveChanges();

            _logRepository.Add(new Log().GeneratedForEntity(userId, user, LogTypeResource.Update));
        }

        public Usuario Login(string user, string password, string codRecover)
        {
            var usuario = _context.Usuario
                .Include(i => i.Perfil)
                .Include(i => i.Hierarquia)
                .FirstOrDefault(u => u.Ativo && u.Login == user && u.Senha == password);

            if (usuario == null)
                return _context.Usuario
                    .Include(i => i.Perfil)
                    .Include(i => i.Hierarquia)
                    .FirstOrDefault(u => u.Ativo && u.Login == user && u.Senha == codRecover && u.CodigoRecover == codRecover);

            return usuario;
        }

        public Usuario RecuperarSenha(string email, string codigoRecover, string passRecover)
        {
            var usuario = _context.Usuario
                .FirstOrDefault(u => u.Email == email);

            if (usuario == null)
                return null;

            usuario.CodigoRecover = codigoRecover;
            usuario.Senha = passRecover;

            _context.SaveChanges();

            return usuario;
        }

        public void ResetSenha(string login, string codRecover, string newPassword)
        {
            var usuario = _context.Usuario
                .Single(u => u.Login == login && u.CodigoRecover == codRecover);

            usuario.CodigoRecover = null;
            usuario.Senha = newPassword;
            _context.SaveChanges();
        }

        public void EditSenha(long userId, string password, string newPassword)
        {
            var user = _context.Usuario
                .Single(u => u.Id == userId && u.Senha == password);

            user.Senha = newPassword;
            _context.SaveChanges();

            _logRepository.Add(new Log().GeneratedForEntity(userId, user, LogTypeResource.Update));
        }

        public string GetSenha(long usuarioId)
        {
            return _context.Usuario
                .First(u => u.Id == usuarioId).Senha;
        }

        public IList<string> GetEmails(IEnumerable<int> perfisId)
        {
            return _context.Usuario.Where(w => perfisId.Contains(w.PerfilId)).Select(s => s.Email).ToList();
        }

        public List<string> GetTelefones(IEnumerable<int> perfisId)
        {
            return _context.Usuario.Where(w => perfisId.Contains(w.PerfilId)).Select(s => s.Telefone).ToList();
        }

        public void InsertToken(long userId, string token)
        {
            var user = _context.Usuario
                .Single(u => u.Id == userId);

            user.Token = token;
            _context.SaveChanges();

            _logRepository.Add(new Log().GeneratedForEntity(userId, user, LogTypeResource.Update));
        }

        #endregion
    }
}
