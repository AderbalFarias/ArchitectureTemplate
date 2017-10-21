using ArchitectureTemplate.Domain.DataEntities;
using ArchitectureTemplate.Infraestrutura.CrossCutting.Support.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ArchitectureTemplate.Domain.Interfaces.Services
{
    public interface IUserService : IServiceBase<User>
    {
        IEnumerable<User> Get(Pagination paginar);
        Task<IEnumerable<User>> GetAsync(Pagination paginar);
        void DisableOrEnable(long userId, long userAutenticationId);
        User Login(string user, string password);
        User RecoverPassword(string email);
        void ResetPassword(string login, string codRecover, string newPassword);
        void EditPassword(long userId, string password, string newPassword);
        string GetPassword(long userId);
        string GetCodigoRecover();
        string GenerateToken(long userId);
        void CleanToken(long userId);
    }
}
