using System.Collections.Generic;
using ArchitectureTemplate.Domain.DataEntities;
using OfficeOpenXml;

namespace ArchitectureTemplate.Domain.Interfaces.Services
{
    public interface IPermissaoService
    {
        bool AllowAccess(int ProfileId, string controllerName, string accessType);
        string GetToken(long userId);
        IEnumerable<ProfilePorTela> GetProfilePorTela(int ProfileId);
        void EnableOrDisabled(int ProfileId, int telaId, string parametro, long permissaoId, long userId);
        void EnableOrDisabled(int ProfileId, int telaId, bool ativar, long permissaoId, long userId);
        ExcelWorksheet LoadExportToExcel(ExcelPackage pck);
    }
}
