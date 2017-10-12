using System.Collections.Generic;
using System.Threading.Tasks;
using ArchitectureTemplate.Domain.DataEntities;
using ArchitectureTemplate.Infraestrutura.CrossCutting.Support.Extensions;

namespace ArchitectureTemplate.Domain.Interfaces.Repositories
{
    public interface IUsuarioRepository : IRepositoryBase<Usuario>
    {
        IEnumerable<Usuario> Get(Pagination paginar);
        Task<IEnumerable<Usuario>> GetAsync(Pagination paginar);
        void DisableOrEnable(long usuarioId, long userId, bool ativo);
        Usuario Login(string user, string password, string codRecover);
        Usuario RecuperarSenha(string email, string codigoRecover, string passRecover);
        void ResetSenha(string login, string codRecover, string newPassword);
        void EditSenha(long userId, string password, string newPassword);
        string GetSenha(long usuarioId);
        IList<string> GetEmails(IEnumerable<int> perfisId);
        List<string> GetTelefones(IEnumerable<int> perfisId);
        void InsertToken(long userId, string token);
    }
}
