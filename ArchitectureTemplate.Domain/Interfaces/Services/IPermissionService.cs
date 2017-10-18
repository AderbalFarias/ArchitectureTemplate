using ArchitectureTemplate.Domain.DataEntities;
using OfficeOpenXml;
using System.Collections.Generic;

namespace ArchitectureTemplate.Domain.Interfaces.Services
{
    public interface IPermissionService
    {
        bool AllowAccess(int profileId, string controllerName, string accessType);
        string GetToken(long userId);
        IEnumerable<ProfileForScreen> GetProfileForScreen(int profileId);
        void EnableOrDisabled(int profileId, int screenId, string parametro, long permissionId, long userId);
        void EnableOrDisabled(int profileId, int screenId, bool ativar, long permissionId, long userId);
        ExcelWorksheet LoadExportToExcel(ExcelPackage pck);
    }
}
