using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ArchitectureTemplate.Mvc.Models;

namespace ArchitectureTemplate.Mvc.Helpers
{
    public static class HierarquiaHelper
    {
        public static MvcHtmlString HierarquiaTree<TModel>(this HtmlHelper<TModel> helper, IList<HierarquiaModel> hList, bool openAll)
        {
            var divMain = new TagBuilder("div");
            divMain.AddCssClass("col-md-12");
            divMain.GenerateId("divTree");

            var ul = new TagBuilder("ul");
            ul.GenerateId("ulTreeOne");

            var raiz = hList.First(f => f.TipoHierarquia.Descricao.Equals("Raiz"));
            var filhosRaiz = hList.Where(w => w.HierarquiaPaiId == raiz.Id).ToList();

            var conteudoArvore = filhosRaiz
                .Aggregate(string.Empty, (current, item) => current + item.TreeHierarquia(item, openAll));

            var acoes = $"<a href=\"\\Hierarquia\\Create?idPai={raiz.Id}\">" +
                        $"<i class=\"fa fa-plus-square-o\"></i></a>" +
                    $"<a href=\"\\Hierarquia\\Edit\\{raiz.Id}\" ><i class=\"fa fa-edit\"></i></a>" +
                    $"<a onclick=\"ConfirmDialog('/Hierarquia/Delete/{raiz.Id}', " +
                        $"'Deseja realmente excluir a hierarquia {raiz.Nome}')\">" +
                            $"<i class=\"fa fa-remove\"></i></a>";

            var li = new TagBuilder("li")
            {
                InnerHtml = $"{raiz.Nome} {acoes} <ul>{conteudoArvore}</ul>"
            };

            ul.InnerHtml = $"{MvcHtmlString.Create(li.ToString(TagRenderMode.Normal))}";
            divMain.InnerHtml = $"{MvcHtmlString.Create(ul.ToString(TagRenderMode.Normal))}";

            return MvcHtmlString.Create(divMain.ToString(TagRenderMode.Normal));
        }
    }
}
