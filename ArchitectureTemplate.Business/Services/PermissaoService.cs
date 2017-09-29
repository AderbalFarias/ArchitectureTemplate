using System.Collections.Generic;
using System.Drawing;
using ArchitectureTemplate.Business.DataEntities;
using ArchitectureTemplate.Business.Interfaces.Repositories;
using ArchitectureTemplate.Business.Interfaces.Services;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace ArchitectureTemplate.Business.Services
{
    public class PermissaoService : IPermissaoService
    {
        #region Fields

        private readonly IPermissaoRepository _permissaoRepository;

        #endregion

        #region Constructors

        public PermissaoService(IPermissaoRepository permissaoRepository)
        {
            _permissaoRepository = permissaoRepository;
        }

        #endregion

        #region Methods

        public bool AllowAccess(int perfilId, string controllerName, string accessType)
        {
            return _permissaoRepository.AllowAccess(perfilId, controllerName, accessType);
        }

        public string GetToken(long userId)
        {
            return _permissaoRepository.GetToken(userId);
        }

        public IEnumerable<PerfilPorTela> GetPerfilPorTela(int perfilId)
        {
            return _permissaoRepository.GetPerfilPorTela(perfilId);
        }

        public void EnableOrDisabled(int perfilId, int telaId, string parametro, long permissaoId, long userId)
        {
            PerfilPorTela permissao;

            if (permissaoId != 0)
            {
                permissao = _permissaoRepository.GetPerfilPorTela(permissaoId);
            }
            else
            {
                permissao = new PerfilPorTela
                {
                    PerfilId = perfilId,
                    TelaId = telaId,
                };
            }

            if (parametro.Equals("Read"))
            {
                permissao.Read = !permissao.Read;
            }
            else if (parametro.Equals("Create"))
            {
                permissao.Create = !permissao.Create;
            }
            else if (parametro.Equals("Update"))
            {
                permissao.Update = !permissao.Update;
            }
            else if (parametro.Equals("Delete"))
            {
                permissao.Delete = !permissao.Delete;
            }
            else
            {
                return;
            }

            _permissaoRepository.EnableOrDisabled(permissao, userId);
        }

        public void EnableOrDisabled(int perfilId, int telaId, bool ativar, long permissaoId, long userId)
        {
            PerfilPorTela permissao;

            if (permissaoId != 0)
            {
                permissao = _permissaoRepository.GetPerfilPorTela(permissaoId);
            }
            else
            {
                permissao = new PerfilPorTela
                {
                    PerfilId = perfilId,
                    TelaId = telaId,
                };
            }

            var tela = _permissaoRepository.GetTela(telaId);

            if (tela.Read)
            {
                permissao.Read = ativar;
            }
            if (tela.Create)
            {
                permissao.Create = ativar;
            }
            if (tela.Update)
            {
                permissao.Update = ativar;
            }
            if (tela.Delete)
            {
                permissao.Delete = ativar;
            }

            _permissaoRepository.EnableOrDisabled(permissao, userId);
        }

        public ExcelWorksheet LoadExportToExcel(ExcelPackage pck)
        {
            //Referências para EPPlus
            //https://computacaoemfoco.wordpress.com/2015/02/26/manipulando-arquivo-de-planilha-excel-no-c-com-epplus/
            //http://zeeshanumardotnet.blogspot.com.br/2011/06/creating-reports-in-excel-2007-using.html
            //http://csharpcode.org/asp-net-mvc-export-data-to-excel-from-database-using-epplus/

            var dados = _permissaoRepository.GetAll();

            int linha = 2;
            int coluna = 1;
            ExcelWorksheet wsRelatorio = pck.Workbook.Worksheets.Add("Dados");

            //Escrevendo cabeçalho da tabela
            wsRelatorio.Cells[linha, coluna++].Value = "Perfil";
            wsRelatorio.Cells[linha, coluna++].Value = "Nome da Tela";
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

                wsRelatorio.Cells[linha, coluna++].Value = item.Perfil.Nome;
                wsRelatorio.Cells[linha, coluna++].Value = item.Tela.Nome;
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