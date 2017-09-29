using ArchitectureTemplate.Business.DataEntities;
using ArchitectureTemplate.Infraestrutura.CrossCutting.Support.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ArchitectureTemplate.Business.Interfaces.Services
{
    public interface IUsuarioService : IServiceBase<Usuario>
    {
        IEnumerable<Usuario> Get(Pagination paginar);
        Task<IEnumerable<Usuario>> GetAsync(Pagination paginar);
        void DisableOrEnable(long usuarioId, long userId);
        Usuario Login(string user, string password);
        Usuario RecuperarSenha(string email);
        void ResetSenha(string login, string codRecover, string newPassword);
        void EditSenha(long userId, string password, string newPassword);
        string GetSenha(long usuarioId);
        string GetCodigoRecover();
        string GenerateToken(long userId);
        void CleanToken(long userId);
    }
}
