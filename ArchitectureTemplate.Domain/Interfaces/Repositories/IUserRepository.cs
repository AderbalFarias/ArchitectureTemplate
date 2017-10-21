using ArchitectureTemplate.Domain.DataEntities;
using ArchitectureTemplate.Infraestrutura.CrossCutting.Support.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ArchitectureTemplate.Domain.Interfaces.Repositories
{
    public interface IUserRepository : IRepositoryBase<User>
    {
        IEnumerable<User> Get(Pagination paginar);
        Task<IEnumerable<User>> GetAsync(Pagination paginar);
        void DisableOrEnable(long userId, long userAutenticationId, bool ativo);
        User Login(string user, string password, string codRecover);
        User RecoverPassword(string email, string codigoRecover, string passRecover);
        void ResetPassword(string login, string codRecover, string newPassword);
        void EditPassword(long userId, string password, string newPassword);
        string GetPassword(long userId);
        IList<string> GetEmails(IEnumerable<int> profilesId);
        List<string> GetTelefones(IEnumerable<int> profilesId);
        void InsertToken(long userId, string token);
    }
}
