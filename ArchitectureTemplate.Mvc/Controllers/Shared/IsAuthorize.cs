using System;
using System.Linq;
using System.Security.Claims;
using System.Web.Mvc;
using ArchitectureTemplate.Domain.Interfaces.Services;

namespace ArchitectureTemplate.Mvc.Controllers.Shared
{
    public class IsAuthorize : ActionFilterAttribute
    {
        private readonly IPermissaoService _permissaoService;

        public IsAuthorize()
        {
            _permissaoService = DependencyResolver.Current.GetService<IPermissaoService>();
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string id = "Indefinida";
            string redirect;

            if (filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                var clains = (ClaimsIdentity)filterContext.HttpContext.User.Identity;

                var perfilId = Convert.ToInt32(clains.FindFirst("PerfilId").Value);

                var userId = Convert.ToInt32(clains.FindFirst(ClaimTypes.Authentication).Value);
                var tokenClain = clains.FindFirst("Token").Value;
                var tokenDatabase = _permissaoService.GetToken(userId);

                if (!tokenDatabase.Equals(tokenClain))
                {
                    redirect = new UrlHelper(filterContext.RequestContext)
                        .Action("InvalidToken", "Login", new { id = userId });
                }
                else
                {
                    var controllerName = filterContext.ActionDescriptor
                    .ControllerDescriptor.ControllerName + "Controller";

                    var accessType = filterContext.ActionDescriptor
                        .GetCustomAttributes(typeof(ActionType), true).FirstOrDefault();

                    var accessTypeProperty = accessType?.GetType().GetProperty("AccessType");

                    if (accessTypeProperty != null)
                    {
                        switch ((AccessType)accessTypeProperty.GetValue(accessType))
                        {
                            case AccessType.Create:
                                {
                                    if (_permissaoService.AllowAccess(perfilId, controllerName, "Create")) return;
                                    id = "Cadastrar";
                                    break;
                                }
                            case AccessType.Read:
                                {
                                    if (_permissaoService.AllowAccess(perfilId, controllerName, "Read")) return;
                                    id = "Consultar";
                                    break;
                                }
                            case AccessType.Update:
                                {
                                    if (_permissaoService.AllowAccess(perfilId, controllerName, "Update")) return;
                                    id = "Atualizar";
                                    break;
                                }
                            case AccessType.Delete:
                                {
                                    if (_permissaoService.AllowAccess(perfilId, controllerName, "Delete")) return;
                                    id = "Deletar";
                                    break;
                                }
                                //default:
                                //    {
                                //        break;
                                //    }
                        }
                    }

                    redirect = new UrlHelper(filterContext.RequestContext)
                        .Action("AccessDenied", "Home", new { id });
                }
            }
            else
            {
                redirect = new UrlHelper(filterContext.RequestContext)
                    .Action("AccessDenied", "Login", new { id });
            }

            filterContext.Result = new RedirectResult(redirect);
            base.OnActionExecuting(filterContext);
        }

        //public override void OnActionExecuted(ActionExecutedContext filterContext)
        //{
        //    base.OnActionExecuted(filterContext);
        //}

        //public override void OnResultExecuting(ResultExecutingContext filterContext)
        //{
        //    base.OnResultExecuting(filterContext);
        //}

        //public override void OnResultExecuted(ResultExecutedContext filterContext)
        //{
        //    base.OnResultExecuted(filterContext);
        //}
    }
}