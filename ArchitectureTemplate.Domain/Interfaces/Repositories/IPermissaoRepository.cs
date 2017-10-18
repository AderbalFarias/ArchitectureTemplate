using System.Collections.Generic;
using ArchitectureTemplate.Domain.DataEntities;

namespace ArchitectureTemplate.Domain.Interfaces.Repositories
{
    public interface IPermissaoRepository
    {
        bool AllowAccess(int ProfileId, string controllerName, string accessType);
        string GetToken(long userId);
        IEnumerable<ProfilePorTela> GetAll();
        IEnumerable<ProfilePorTela> GetProfilePorTela(int ProfileId);
        ProfilePorTela GetProfilePorTela(long id);
        ProfilePorTela GetTela(int telaId);
        void EnableOrDisabled(ProfilePorTela entity, long userId);
    }
}
