using ArchitectureTemplate.Domain.DataEntities;
using System.Collections.Generic;

namespace ArchitectureTemplate.Domain.Interfaces.Repositories
{
    public interface IPermissionRepository
    {
        bool AllowAccess(int profileId, string controllerName, string accessType);
        string GetToken(long userId);
        IEnumerable<ProfileForScreen> GetAll();
        IEnumerable<ProfileForScreen> GetProfilePorScreen(int profileId);
        ProfileForScreen GetProfilePorScreen(long id);
        ProfileForScreen GetScreen(int screenId);
        void EnableOrDisabled(ProfileForScreen entity, long userId);
    }
}
