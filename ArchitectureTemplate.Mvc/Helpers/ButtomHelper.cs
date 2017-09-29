using System;
using System.Linq;
using System.Web.Mvc;

namespace ArchitectureTemplate.Mvc.Helpers
{
    public static class ButtomHelper
    {
        /// <summary>
        /// Cria um helper para utilização de botão com chamada js
        /// </summary>
        /// <param name="helper">Helper .Net Razor</param>
        /// <param name="text">Texto do botão</param>
        /// <param name="document">Expecifica o tipo</param>
        /// <param name="url">Url da requisição</param>
        /// <param name="html"></param>
        /// <returns>retorna helper que gera html</returns>
        public static MvcHtmlString ButtonOnclick(this HtmlHelper helper, string text, string document, string url, Object html = null)
        {
            var caminho = $"document.{document} ='/{url}'";

            var id = "idBotao" + text.Replace(" ", "").Replace("_", "").Replace(".", "").Replace("´", "").Replace("^", "").Replace("~", "").Replace("`", "").Replace("?", "");

            var tag = new TagBuilder("input"); //gera uma tag input no html
            tag.MergeAttribute("value", text); //exibe o texto passado 
            tag.MergeAttribute("type", "button");

            if (url != null)
                tag.MergeAttribute("onclick", caminho);

            tag.GenerateId(id);
            tag.HtmlAtributosCustom(html);
            return MvcHtmlString.Create(tag.ToString(TagRenderMode.SelfClosing));
        }

        /// <summary>
        /// Cria helper que submita o form via js
        /// </summary>
        /// <param name="helper">Helper .Net Razor</param>
        /// <param name="text">Texto do botão</param>
        /// <param name="url">Url da requisição</param>
        /// <param name="html">Inclusão de atributos html caso necessário</param>
        /// <returns>retorna helper que gera html</returns>
        public static MvcHtmlString ButtonSubmit(this HtmlHelper helper, string text, string url, Object html = null)
        {
            var caminho = $"formSubmit(this, '{url}')";

            var id = "idBotao" + text.Replace(" ", "").Replace("_", "").Replace(".", "").Replace("´", "").Replace("^", "").Replace("~", "").Replace("`", "").Replace("?", "");

            var tag = new TagBuilder("input"); //gera uma tag input no html
            tag.MergeAttribute("value", text); //exibe o texto passado 
            tag.MergeAttribute("type", "button");
            tag.MergeAttribute("onclick", caminho);
            tag.AddCssClass("btn");
            tag.GenerateId(id);
            return MvcHtmlString.Create(tag.ToString(TagRenderMode.SelfClosing));
        }

        /// <summary>
        /// Cria um helper input / botão do tipo específicado
        /// </summary>
        /// <param name="helper">Helper .Net Razor</param>
        /// <param name="text">texto do input (botão)</param>
        /// <param name="type">tipo do botão (Ex: submit, button, reset, ...)</param>
        /// <param name="html">Inclusão de atributos html caso necessário</param>
        /// <returns>retorna helper que gera html</returns>
        public static MvcHtmlString Button(this HtmlHelper helper, string text, string type, Object html = null)
        {
            var id = "idBotao" + text.Replace(" ", "").Replace("_", "").Replace(".", "").Replace("´", "").Replace("^", "").Replace("~", "").Replace("`", "").Replace("?", "");

            var tag = new TagBuilder("input");
            tag.MergeAttribute("value", text);
            tag.MergeAttribute("type", type);

            tag.GenerateId(id);
            tag.HtmlAtributosCustom(html);
            return MvcHtmlString.Create(tag.ToString(TagRenderMode.SelfClosing));
        }

        /// <summary>
        /// Cria helper com a seguinte estrutura: '<a><input></input></a>'
        /// </summary>
        /// <param name="helper">Helper .Net Razor</param>
        /// <param name="buttonText">Texto do button</param>
        /// <param name="contentPath">Mapeamento de rota</param>
        /// <param name="actionName">Action da controller indicada</param>
        /// <param name="controllerName">Controller que será realizada a requisição</param>
        /// <param name="aHtml">Inclusão de atributos html para a tag: '<a></a>', caso necessári</param>
        /// <param name="inputHtml">Inclusão de atributos html para o button caso necessário</param>
        /// <returns>retorna helper que gera html</returns>
        public static MvcHtmlString ActionLinkButton(this HtmlHelper helper, string buttonText, string contentPath, string actionName, string controllerName, Object aHtml = null, Object inputHtml = null)
        {
            var id = "idBotao" + buttonText.Replace(" ", "").Replace("_", "").Replace(".", "").Replace("´", "").Replace("^", "").Replace("~", "").Replace("`", "").Replace("?", "");
         
            var tagA = new TagBuilder("a");
            var tagInput = new TagBuilder("input");

            var aUrl = (actionName == "Index" 
                ? $"{contentPath}{controllerName}"
                : $"{contentPath}{controllerName}/{actionName}");

            tagA.MergeAttribute("href", aUrl);
            tagA.HtmlAtributosCustom(aHtml);

            tagInput.MergeAttribute("value", buttonText);
            tagInput.MergeAttribute("type", "button");
            tagInput.GenerateId(id);
            tagInput.HtmlAtributosCustom(inputHtml);

            tagA.InnerHtml = tagInput.ToString(TagRenderMode.Normal);

            return MvcHtmlString.Create(tagA.ToString(TagRenderMode.Normal));
        }

        public static MvcHtmlString ModalLink(this HtmlHelper helper, string buttonText, string idModal, string htmlEspecifico = null, Object html = null)
        {
            var tagA = new TagBuilder("a");

            tagA.MergeAttribute("type", "button");
            tagA.MergeAttribute("href", $"#{idModal}");
            tagA.HtmlAtributosCustom(html);
            tagA.InnerHtml = buttonText;

            if (htmlEspecifico != null)
            {
                var htmlList = htmlEspecifico.Trim().Split(',');

                foreach (var value in htmlList.Select(item => item.Trim().Split('=').ToArray()))
                {
                    tagA.MergeAttribute(value[0], value[1]);
                }
            }

            return MvcHtmlString.Create(tagA.ToString(TagRenderMode.Normal));
        }
    }
}