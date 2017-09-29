using System.Collections.Generic;
using ArchitectureTemplate.Business.DataEntities;
using OfficeOpenXml;

namespace ArchitectureTemplate.Business.Interfaces.Services
{
    public interface IPermissaoService
    {
        bool AllowAccess(int perfilId, string controllerName, string accessType);
        string GetToken(long userId);
        IEnumerable<PerfilPorTela> GetPerfilPorTela(int perfilId);
        void EnableOrDisabled(int perfilId, int telaId, string parametro, long permissaoId, long userId);
        void EnableOrDisabled(int perfilId, int telaId, bool ativar, long permissaoId, long userId);
        ExcelWorksheet LoadExportToExcel(ExcelPackage pck);
    }
}
