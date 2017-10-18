using ArchitectureTemplate.Mvc.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace ArchitectureTemplate.Mvc.Helpers
{
    public static class HierarchyHelper
    {
        public static MvcHtmlString HierarchyTree<TModel>(this HtmlHelper<TModel> helper, IList<HierarchyModel> hList, bool openAll)
        {
            var divMain = new TagBuilder("div");
            divMain.AddCssClass("col-md-12");
            divMain.GenerateId("divTree");

            var ul = new TagBuilder("ul");
            ul.GenerateId("ulTreeOne");

            var root = hList.First(f => f.HierarchyType.Descricao.Equals("Root"));
            var filhosRoot = hList.Where(w => w.HierarchyPaiId == root.Id).ToList();

            var conteudoArvore = filhosRoot
                .Aggregate(string.Empty, (current, item) => current + item.TreeHierarchy(item, openAll));

            var acoes = $"<a href=\"\\Hierarchy\\Create?idPai={root.Id}\">" +
                        $"<i class=\"fa fa-plus-square-o\"></i></a>" +
                    $"<a href=\"\\Hierarchy\\Edit\\{root.Id}\" ><i class=\"fa fa-edit\"></i></a>" +
                    $"<a onclick=\"ConfirmDialog('/Hierarchy/Delete/{root.Id}', " +
                        $"'Deseja realmente excluir a hierarchy {root.Nome}')\">" +
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
