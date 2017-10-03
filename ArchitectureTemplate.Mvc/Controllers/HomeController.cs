using ArchitectureTemplate.Mvc.Controllers.Shared;
using ArchitectureTemplate.Mvc.Models.Shared;
using System.Web.Mvc;

namespace ArchitectureTemplate.Mvc.Controllers
{
    public class HomeController : CustomController
    {
        [HttpGet]
        //[IsAuthorize]
        [ActionType(AccessType.Read)]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        //[IsAuthorize]
        [ActionType(AccessType.Read)]
        public ActionResult AccessDenied(string id)
        {
            ShowMessageDialog($"O perfil {CurrentUser.PerfilName} não possui permissão de acesso para a funcionalidade {id}", Message.MessageKind.Error, 10);
            return View();
        }
    }
}