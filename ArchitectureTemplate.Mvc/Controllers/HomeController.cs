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
            ShowMessageDialog($"The role {CurrentUser.PerfilName} can't accessn the function with Id: {id}", Message.MessageKind.Error, 10);
            return View();
        }
    }
}