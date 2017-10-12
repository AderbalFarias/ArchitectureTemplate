using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using ArchitectureTemplate.Domain.DataEntities;
using ArchitectureTemplate.Domain.Interfaces.Repositories;
using ArchitectureTemplate.Domain.Interfaces.Services;
using ArchitectureTemplate.Infraestrutura.CrossCutting.Support.Extensions;

namespace ArchitectureTemplate.Domain.Services
{
    public class UsuarioService : ServiceBase<Usuario>, IUsuarioService
    {
        #region Fields

        private readonly IUsuarioRepository _usuarioRepository;

        #endregion

        #region Constructors

        public UsuarioService(IUsuarioRepository usuarioRepository)
            : base(usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        #endregion

        #region Methods

        public IEnumerable<Usuario> Get(Pagination paginar)
        {
            return _usuarioRepository.Get(paginar);
        }

        public async Task<IEnumerable<Usuario>> GetAsync(Pagination paginar)
        {
            return await _usuarioRepository.GetAsync(paginar);
        }

        public void DisableOrEnable(long usuarioId, long userId)
        {
            var ativo = _usuarioRepository.GetId(usuarioId).Ativo;
            _usuarioRepository.DisableOrEnable(usuarioId, userId, !ativo);
        }

        public Usuario Login(string user, string password)
        {
            return _usuarioRepository.Login(user, CriptografarSenha(password), password);
        }

        public Usuario RecuperarSenha(string email)
        {
            return _usuarioRepository.RecuperarSenha(email, GetCodigoRecover(), CriptografarSenha(GetCodigoRecover()));
        }

        public void ResetSenha(string login, string codRecover, string newPassword)
        {
            _usuarioRepository.ResetSenha(login, codRecover, CriptografarSenha(newPassword));
        }

        public void EditSenha(long userId, string password, string newPassword)
        {
            _usuarioRepository.EditSenha(userId, CriptografarSenha(password), CriptografarSenha(newPassword));
        }

        public string GetSenha(long usuarioId)
        {
            return _usuarioRepository.GetSenha(usuarioId);
        }

        public string GetCodigoRecover()
        {
            var chars = "ABCDEF0123456789GHIJKL0123456789MNOPQR0123456789STUVWXYZ0123456789";
            var random = new Random();
            var codigoRecover = new string(Enumerable.Repeat(chars, 10)
                .Select(s => s[random.Next(s.Length)]).ToArray());

            return codigoRecover;
        }

        public string GenerateToken(long userId)
        {
            var token = GetCodigoRecover();

            _usuarioRepository.InsertToken(userId, token);

            return token;
        }

        public void CleanToken(long userId)
        {
            _usuarioRepository.InsertToken(userId, null);
        }

        #endregion

        #region Private Methods

        private string CriptografarSenha(string senha)
        {
            return Convert.ToBase64String(new MD5CryptoServiceProvider().ComputeHash(new UTF32Encoding().GetBytes(senha)));
        }

        #endregion
    }
}