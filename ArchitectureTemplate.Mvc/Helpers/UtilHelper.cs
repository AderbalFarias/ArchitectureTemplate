using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web.Mvc;

namespace ArchitectureTemplate.Mvc.Helpers
{
    public static class UtilHelper
    {
        /// <summary>
        /// Paginção de lista
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="helper">Helper</param>
        /// <param name="url">adress of action</param>
        /// <param name="pagAtual">Page that receives the event</param>
        /// <param name="qtdePag">Number of existing pages</param>
        /// <param name="formId">Form to serialize</param>
        /// <param name="divLocation">Div for pagination return</param>
        /// <param name="beginEnd">Indicates whether pagination contains the start and end elements</param>
        /// <returns>Element nav with itens for pagination of data</returns>
        public static MvcHtmlString Pagination<TModel>(this HtmlHelper<TModel> helper, string url, int pagAtual,
            int qtdePag, string formId = null, string divLocation = null, bool beginEnd = true)
        {
            if (qtdePag < 1)
                return null;

            var nav = new TagBuilder("nav");
            nav.AddCssClass("text-center");

            var ul = new TagBuilder("ul");
            ul.AddCssClass("pagination");

            var liBegin = new TagBuilder("li")
            {
                InnerHtml = formId != null && divLocation != null
                    ? (pagAtual != 1
                        ? $"<a href=\"#\" onclick=\"AjaxSubmitForm('{formId}', '#{divLocation}', '{url}', '{1}')\" " +
                          $"aria-label=\"Previous\"><span aria-hidden=\"true\">&laquo;</span></a>"
                        : "<a aria-label=\"Previous\"><span aria-hidden=\"true\">&laquo;</span></a>")
                    : formId != null
                        ? (pagAtual != 1
                            ? $"<a href=\"#\" onclick=\"SubmitForm('{formId}', '{url}', '{1}')\" " +
                              $"aria-label=\"Previous\"><span aria-hidden=\"true\">&laquo;</span></a>"
                            : "<a aria-label=\"Previous\"><span aria-hidden=\"true\">&laquo;</span></a>")
                        : (pagAtual != 1
                            ? $"<a href=\"{url}?idPag={1}\" aria-label=\"Previous\"><span aria-hidden=\"true\">&laquo;</span></a>"
                            : "<a aria-label=\"Previous\" class=\"disabled\"><span aria-hidden=\"true\">&laquo;</span></a>")
            };

            var liPrevious = new TagBuilder("li")
            {
                InnerHtml = formId != null && divLocation != null
                    ? (pagAtual != 1
                        ? $"<a href=\"#\" onclick=\"AjaxSubmitForm('{formId}', '#{divLocation}', '{url}', '{(pagAtual - 1)}')\" " +
                          $"aria-label=\"Previous\"><span aria-hidden=\"true\">&lsaquo;</span></a>"
                        : "<a aria-label=\"Previous\"><span aria-hidden=\"true\">&lsaquo;</span></a>")
                    : formId != null
                        ? (pagAtual != 1
                            ? $"<a href=\"#\" onclick=\"SubmitForm('{formId}', '{url}', '{(pagAtual - 1)}')\" " +
                              $"aria-label=\"Previous\"><span aria-hidden=\"true\">&lsaquo;</span></a>"
                            : "<a aria-label=\"Previous\"><span aria-hidden=\"true\">&lsaquo;</span></a>")
                        : (pagAtual != 1
                            ? $"<a href=\"{url}?idPag={(pagAtual - 1)}\" aria-label=\"Previous\"><span aria-hidden=\"true\">&lsaquo;</span></a>"
                            : "<a aria-label=\"Previous\" class=\"disabled\"><span aria-hidden=\"true\">&lsaquo;</span></a>")
            };

            if (pagAtual == 1)
            {
                liBegin.AddCssClass("disabled");
                liPrevious.AddCssClass("disabled");
            }

            var liList = (beginEnd ? liBegin.ToString(TagRenderMode.Normal) : null) +
                         liPrevious.ToString(TagRenderMode.Normal);

            int maxPag = qtdePag > 5 ? 5 : qtdePag;

            for (int i = 0; i < maxPag; i++)
            {
                int numPag;
                if (maxPag < 5)
                {
                    numPag = i + 1;
                }
                else if (pagAtual < 3 || (pagAtual + 2) > qtdePag)
                {
                    switch ((qtdePag - pagAtual))
                    {
                        case 0:
                            numPag = (pagAtual - 4) + i;
                            break;
                        case 1:
                            numPag = (pagAtual - 3) + i;
                            break;
                        case 2:
                            numPag = (pagAtual - 2) + i;
                            break;
                        case 3:
                            numPag = (pagAtual - 1) + i;
                            break;
                        default:
                            numPag = pagAtual + i;
                            break;
                    }
                }
                else
                {
                    switch (i)
                    {
                        case 0:
                            numPag = pagAtual - 2;
                            break;
                        case 1:
                            numPag = pagAtual - 1;
                            break;
                        case 2:
                            numPag = pagAtual;
                            break;
                        case 3:
                            numPag = pagAtual + 1;
                            break;
                        case 4:
                            numPag = pagAtual + 2;
                            break;
                        default:
                            numPag = 0;
                            break;
                    }
                }

                var li = new TagBuilder("li")
                {
                    InnerHtml = formId != null && divLocation != null
                        ? $"<a href=\"#\" onclick=\"AjaxSubmitForm('{formId}', '#{divLocation}', '{url}', '{numPag}')\">{numPag} " +
                          $"<span class=\"sr-only\">(current)</span></a>"
                        : formId != null
                            ? $"<a href=\"#\" onclick=\"SubmitForm('{formId}', '{url}', '{numPag}')\">{numPag} " +
                              $"<span class=\"sr-only\">(current)</span></a>"
                            : $"<a href=\"{url}?idPag={numPag}\">{numPag} <span class=\"sr-only\">(current)</span></a>"
                };

                if (numPag == pagAtual)
                    li.AddCssClass("active");

                liList = liList + li.ToString(TagRenderMode.Normal);
            }

            var liNext = new TagBuilder("li")
            {
                InnerHtml = formId != null && divLocation != null
                    ? (pagAtual != qtdePag
                        ? $"<a href=\"#\" onclick=\"AjaxSubmitForm('{formId}', '#{divLocation}', '{url}', '{(pagAtual + 1)}')\" " +
                          $"aria-label=\"Next\"><span aria-hidden=\"true\">&rsaquo;</span></a>"
                        : "<a aria-label=\"Next\"><span aria-hidden=\"true\">&rsaquo;</span></a>")
                    : formId != null
                        ? (pagAtual != qtdePag
                            ? $"<a href=\"#\" onclick=\"SubmitForm('{formId}', '{url}', '{(pagAtual + 1)}')\" " +
                              $"aria-label=\"Next\"><span aria-hidden=\"true\">&rsaquo;</span></a>"
                            : "<a aria-label=\"Next\"><span aria-hidden=\"true\">&rsaquo;</span></a>")
                        : (qtdePag != pagAtual
                            ? $"<a href=\"{url}?idPag={(pagAtual + 1)}\" aria-label=\"Next\"><span aria-hidden=\"true\">&rsaquo;</span></a>"
                            : "<a aria-label=\"Next\" class=\"disabled\"><span aria-hidden=\"true\">&rsaquo;</span></a>")
            };

            var liEnd = new TagBuilder("li")
            {
                InnerHtml = formId != null && divLocation != null
                    ? (pagAtual != qtdePag
                        ? $"<a href=\"#\" onclick=\"AjaxSubmitForm('{formId}', '#{divLocation}', '{url}', '{qtdePag}')\" " +
                          $"aria-label=\"Next\"><span aria-hidden=\"true\">&raquo;</span></a>"
                        : "<a aria-label=\"Next\"><span aria-hidden=\"true\">&raquo;</span></a>")
                    : formId != null
                        ? (pagAtual != qtdePag
                            ? $"<a href=\"#\" onclick=\"SubmitForm('{formId}', '{url}', '{qtdePag}')\" " +
                              $"aria-label=\"Next\"><span aria-hidden=\"true\">&raquo;</span></a>"
                            : "<a aria-label=\"Next\"><span aria-hidden=\"true\">&raquo;</span></a>")
                        : (qtdePag != pagAtual
                            ? $"<a href=\"{url}?idPag={qtdePag}\" aria-label=\"Next\"><span aria-hidden=\"true\">&raquo;</span></a>"
                            : "<a aria-label=\"Next\" class=\"disabled\"><span aria-hidden=\"true\">&raquo;</span></a>")
            };

            if (pagAtual == qtdePag)
            {
                liNext.AddCssClass("disabled");
                liEnd.AddCssClass("disabled");
            }

            ul.InnerHtml = liList + liNext.ToString(TagRenderMode.Normal) +
                           (beginEnd ? liEnd.ToString(TagRenderMode.Normal) : null);
            nav.InnerHtml = ul.ToString(TagRenderMode.Normal);

            return MvcHtmlString.Create(nav.ToString(TagRenderMode.Normal));
        }

        /// <summary>
        /// Display em tabela
        /// </summary>
        /// <param name="helper">Helper</param>
        /// <param name="text">Texto que será exibido</param>
        /// <param name="cols">Quantidade de colunas da table</param>
        /// <param name="html">Html para customização</param>
        /// <returns>Elemento tr para ser incluso em uma table</returns>
        public static MvcHtmlString DisplayTrInfo(this HtmlHelper helper, string text, int cols, Object html = null)
        {
            var tr = new TagBuilder("tr");
            var td = new TagBuilder("td");
            var b = new TagBuilder("b");

            td.MergeAttribute("colspan", cols.ToString(CultureInfo.InvariantCulture));
            td.HtmlAtributosCustom(html);
            td.AddCssClass("text-center");

            b.InnerHtml = text;
            td.InnerHtml = b.ToString(TagRenderMode.Normal);
            tr.InnerHtml = td.ToString(TagRenderMode.Normal);

            return MvcHtmlString.Create(tr.ToString(TagRenderMode.Normal));
        }

        /// <summary>
        /// Separador de conteúdo
        /// </summary>
        /// <param name="helper">Helper</param>
        /// <param name="cssClass">class css</param>
        /// <param name="html">Html for customize</param>
        /// <returns>Hr element to separate content</returns>
        public static MvcHtmlString Separador(this HtmlHelper helper, string cssClass = null, Object html = null)
        {
            var tag = new TagBuilder("hr"); //gera uma tag input no html

            if (cssClass != null)
                tag.AddCssClass(cssClass);

            if (html != null)
                tag.HtmlAtributosCustom(html);

            var hr = MvcHtmlString.Create(tag.ToString(TagRenderMode.SelfClosing));

            return hr;
        }

        public static MvcHtmlString DisplayAtivo<TModel>(this HtmlHelper<TModel> helper, bool ativo = false)
        {
            var display = ativo ? "Yes" : "No";
            return MvcHtmlString.Create(display);
        }

        public static MvcHtmlString DisplayFuncionalidades<TModel>(this HtmlHelper<TModel> helper,
            bool create, bool read, bool update, bool delete)
        {
            IList<string> list = new List<string>();

            if (read)
                list.Add("Read");
            if (create)
                list.Add("Create");
            if (update)
                list.Add("Update");
            if (delete)
                list.Add("Delete");

            var display = string.Join("; ", list);
            return MvcHtmlString.Create(display);
        }

        public static MvcHtmlString ButtonDisableOrEnable<TModel>(this HtmlHelper<TModel> helper, bool ativo, string url, string description)
        {
            var a = new TagBuilder("a");
            a.MergeAttribute("href", "#");

            if (ativo)
            {
                a.MergeAttribute("title", "Desativar");
                a.MergeAttribute("onclick", $"ConfirmDialog('{url}', 'Deseja realmente desativar {description}?')");
                a.InnerHtml = "<i class=\"fa fa-md fa-remove\"></i>";
            }
            else
            {
                a.MergeAttribute("title", "Ativar");
                a.MergeAttribute("onclick", $"ConfirmDialog('{url}', 'Deseja realmente ativar {description}?')");
                a.InnerHtml = "<i class=\"fa fa-md fa-plus-circle\"></i>";
            }

            return MvcHtmlString.Create(a.ToString(TagRenderMode.Normal));
        }

        public static MvcHtmlString ButtonDisableOrEnable<TModel>(this HtmlHelper<TModel> helper, bool ativo, string url, bool permitido = false, bool scroll = false)
        {
            if (!permitido)
                return null;

            var a = new TagBuilder("a");

            a.MergeAttribute(scroll ? "href" : "link", url);

            if (ativo)
            {
                a.MergeAttribute("title", "Desativar");
                a.InnerHtml = "<i class=\"fa fa-md fa-remove\"></i>";
            }
            else
            {
                a.MergeAttribute("title", "Ativar");
                a.InnerHtml = "<i class=\"fa fa-md fa-plus-circle\"></i>";
            }

            return MvcHtmlString.Create(a.ToString(TagRenderMode.Normal));
        }
    }
}
