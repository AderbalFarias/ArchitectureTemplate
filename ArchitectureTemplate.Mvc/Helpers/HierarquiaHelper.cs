using ArchitectureTemplate.Mvc.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

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

            var root = hList.First(f => f.TipoHierarquia.Descricao.Equals("Root"));
            var filhosRoot = hList.Where(w => w.HierarquiaPaiId == root.Id).ToList();

            var conteudoArvore = filhosRoot
                .Aggregate(string.Empty, (current, item) => current + item.TreeHierarquia(item, openAll));

            var acoes = $"<a href=\"\\Hierarquia\\Create?idPai={root.Id}\">" +
                        $"<i class=\"fa fa-plus-square-o\"></i></a>" +
                    $"<a href=\"\\Hierarquia\\Edit\\{root.Id}\" ><i class=\"fa fa-edit\"></i></a>" +
                    $"<a onclick=\"ConfirmDialog('/Hierarquia/Delete/{root.Id}', " +
                        $"'Deseja realmente excluir a hierarquia {root.Nome}')\">" +
                            $"<i class=\"fa fa-remove\"></i></a>";

            var li = new TagBuilder("li")
            {
                InnerHtml = $"{root.Nome} {acoes} <ul>{conteudoArvore}</ul>"
            };

            ul.InnerHtml = $"{MvcHtmlString.Create(li.ToString(TagRenderMode.Normal))}";
            divMain.InnerHtml = $"{MvcHtmlString.Create(ul.ToString(TagRenderMode.Normal))}";

            return MvcHtmlString.Create(divMain.ToString(TagRenderMode.Normal));
        }
    }
}
