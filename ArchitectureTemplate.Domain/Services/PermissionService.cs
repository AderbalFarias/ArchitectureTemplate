using ArchitectureTemplate.Domain.DataEntities;
using ArchitectureTemplate.Domain.Interfaces.Repositories;
using ArchitectureTemplate.Domain.Interfaces.Services;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Collections.Generic;
using System.Drawing;

namespace ArchitectureTemplate.Domain.Services
{
    public class PermissionService : IPermissionService
    {
        #region Fields

        private readonly IPermissionRepository _permissionRepository;

        #endregion

        #region Constructors

        public PermissionService(IPermissionRepository permissionRepository)
        {
            _permissionRepository = permissionRepository;
        }

        #endregion

        #region Methods

        public bool AllowAccess(int profileId, string controllerName, string accessType)
        {
            return _permissionRepository.AllowAccess(profileId, controllerName, accessType);
        }

        public string GetToken(long userId)
        {
            return _permissionRepository.GetToken(userId);
        }

        public IEnumerable<ProfileForScreen> GetProfilePorScreen(int profileId)
        {
            return _permissionRepository.GetProfilePorScreen(profileId);
        }

        public void EnableOrDisabled(int profileId, int screenId, string parametro, long permissionId, long userId)
        {
            ProfileForScreen permission;

            if (permissionId != 0)
            {
                permission = _permissionRepository.GetProfilePorScreen(permissionId);
            }
            else
            {
                permission = new ProfileForScreen
                {
                    ProfileId = profileId,
                    ScreenId = screenId,
                };
            }

            if (parametro.Equals("Read"))
            {
                permission.Read = !permission.Read;
            }
            else if (parametro.Equals("Create"))
            {
                permission.Create = !permission.Create;
            }
            else if (parametro.Equals("Update"))
            {
                permission.Update = !permission.Update;
            }
            else if (parametro.Equals("Delete"))
            {
                permission.Delete = !permission.Delete;
            }
            else
            {
                return;
            }

            _permissionRepository.EnableOrDisabled(permission, userId);
        }

        public void EnableOrDisabled(int profileId, int screenId, bool ativar, long permissionId, long userId)
        {
            ProfileForScreen permission;

            if (permissionId != 0)
            {
                permission = _permissionRepository.GetProfilePorScreen(permissionId);
            }
            else
            {
                permission = new ProfileForScreen
                {
                    ProfileId = profileId,
                    ScreenId = screenId,
                };
            }

            var screen = _permissionRepository.GetScreen(screenId);

            if (screen.Read)
            {
                permission.Read = ativar;
            }
            if (screen.Create)
            {
                permission.Create = ativar;
            }
            if (screen.Update)
            {
                permission.Update = ativar;
            }
            if (screen.Delete)
            {
                permission.Delete = ativar;
            }

            _permissionRepository.EnableOrDisabled(permission, userId);
        }

        public ExcelWorksheet LoadExportToExcel(ExcelPackage pck)
        {
            //Referências para EPPlus
            //https://computacaoemfoco.wordpress.com/2015/02/26/manipulando-arquivo-de-planilha-excel-no-c-com-epplus/
            //http://zeeshanumardotnet.blogspot.com.br/2011/06/creating-reports-in-excel-2007-using.html
            //http://csharpcode.org/asp-net-mvc-export-data-to-excel-from-database-using-epplus/

            var dados = _permissionRepository.GetAll();

            int linha = 2;
            int coluna = 1;
            ExcelWorksheet wsRelatorio = pck.Workbook.Worksheets.Add("Dados");

            //Escrevendo cabeçalho da tabela
            wsRelatorio.Cells[linha, coluna++].Value = "Profile";
            wsRelatorio.Cells[linha, coluna++].Value = "Nome da Screen";
            wsRelatorio.Cells[linha, coluna++].Value = "Funcionalidades";
            wsRelatorio.Cells[linha, coluna++].Value = "Consultar";
            wsRelatorio.Cells[linha, coluna++].Value = "Cadastrar";
            wsRelatorio.Cells[linha, coluna++].Value = "Atualizar";
            wsRelatorio.Cells[linha, coluna].Value = "Deletar";

            //Inserindo o estilo para a tabela (obedecendo as celulas)
            wsRelatorio.Cells.Style.Font.Size = 12; //Default font size for whole sheet
            wsRelatorio.Cells.Style.Font.Name = "Calibri"; //Default Font name for whole sheet
            wsRelatorio.Cells[1, 1].Value = "Lista de Permissões"; //Heading Name
            wsRelatorio.Cells[1, 1].Style.Font.Size = 14; //Tamanho da fonte da primeira linha
            wsRelatorio.Cells[1, 1, 1, coluna].Merge = true; //Merge columns start and end range
            wsRelatorio.Cells[1, 1, 1, coluna].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center; //Alinhamento no centro
            wsRelatorio.Cells[1, 1, linha, coluna].Style.Fill.PatternType = ExcelFillStyle.Solid; //Preenchimento de fundo sólido
            wsRelatorio.Cells[1, 1, linha, coluna].Style.Fill.BackgroundColor.SetColor(Color.LightGray); //Cor do preenchimento de fundo
            wsRelatorio.Cells[1, 1].Style.Fill.BackgroundColor.SetColor(Color.DimGray); //Cor do preenchimento de fundo
            wsRelatorio.Cells[1, 1, linha, coluna].Style.Font.Bold = true; //Deixando negrito o cabecalho da tabela

            //Escrevendo tabela com os dados
            foreach (var item in dados)
            {
                coluna = 1;
                linha++;

                wsRelatorio.Cells[linha, coluna++].Value = item.Profile.Nome;
                wsRelatorio.Cells[linha, coluna++].Value = item.Screen.Nome;
                wsRelatorio.Cells[linha, coluna++].Value = item.DisplayFuncionalidades(item);
                wsRelatorio.Cells[linha, coluna++].Value = item.DisplayBool(item.Read);
                wsRelatorio.Cells[linha, coluna++].Value = item.DisplayBool(item.Create);
                wsRelatorio.Cells[linha, coluna++].Value = item.DisplayBool(item.Update);
                wsRelatorio.Cells[linha, coluna].Value = item.DisplayBool(item.Delete);
            }

            //Inserindo estilo nas celulas de toda a tabela
            var border = wsRelatorio.Cells[1, 1, linha, coluna].Style.Border;
            border.Bottom.Style = border.Top.Style = border.Left.Style = border.Right.Style = ExcelBorderStyle.Thin;
            wsRelatorio.Cells[1, 1, linha, coluna].AutoFitColumns();

            return wsRelatorio;
        }


        #endregion
    }
}