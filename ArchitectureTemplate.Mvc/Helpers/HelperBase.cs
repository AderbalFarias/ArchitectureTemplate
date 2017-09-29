using System;
using System.Reflection;
using System.Web.Mvc;

namespace ArchitectureTemplate.Mvc.Helpers
{
    public static class HelperBase
    {
        public static void HtmlAtributosCustom(this TagBuilder tag, object htmlAtributo)
        {
            if(htmlAtributo != null)
            {
                PropertyInfo[] atributos = htmlAtributo.GetType().GetProperties();
                foreach (PropertyInfo atributo in atributos)
                {
                    if (string.Compare(atributo.Name, "class", StringComparison.CurrentCultureIgnoreCase) == 0)
                    {
                        tag.AddCssClass(atributo.GetValue(htmlAtributo, null).ToString());
                    }
                    else
                    {
                        tag.MergeAttribute(atributo.Name, atributo.GetValue(htmlAtributo, null).ToString());
                    }
                }
            }
        }
    }
}