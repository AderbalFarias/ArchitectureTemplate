using ArchitectureTemplate.Domain.Interfaces.Services;
using ArchitectureTemplate.Mvc.Controllers.Shared;
using System;
using System.Security.Claims;
using System.Web.Http;

namespace ArchitectureTemplate.Mvc.Controllers.Api
{
    public class PermissionApiController : ApiController
    {
        #region Fields

        private readonly IPermissionService _permissionService;

        #endregion

        #region Constructors

        public PermissionApiController(IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }

        #endregion

        #region Actions

        [HttpGet]
        [IsAuthorize]
        [Authorize(Roles = "Administrator")]
        [ActionType(AccessType.Update)]
        //public PartialViewResult List(int id)
        public IHttpActionResult EnableOrDisable(int id, int screenId, string parametro, long permissionId = 0, double scroll = 0)
        {
            try
            {
                var userClain = User.Identity as ClaimsIdentity;
                var x = Convert.ToInt64(userClain?.FindFirst(ClaimTypes.NameIdentifier).Value);

                _permissionService.EnableOrDisabled(id, screenId, parametro, permissionId, x);
                //ShowMessageDialog(MensagensResource.SucessoAtualizar, Message.MessageKind.Success);
            }
            catch (Exception e)
            {
                //ShowMessageDialog("Ocorreu um erro ao tentar atualizar as permissões", e);
                //LogException(e);
                return BadRequest(e.ToString());
            }

            return Ok();
        }

        [HttpGet]
        [IsAuthorize]
        [Authorize(Roles = "Administrator")]
        [ActionType(AccessType.Update)]
        //public PartialViewResult List(int id)
        public IHttpActionResult EnableOrDisableAll(int id, int screenId, bool ativar, long permissionId = 0, double scroll = 0)
        {
            try
            {
                var userClain = User.Identity as ClaimsIdentity;
                var x = Convert.ToInt64(userClain?.FindFirst(ClaimTypes.NameIdentifier).Value);

                _permissionService.EnableOrDisabled(id, screenId, ativar, permissionId, x);
                //ShowMessageDialog(MensagensResource.SucessoAtualizar, Message.MessageKind.Success);
            }
            catch (Exception e)
            {
                //ShowMessageDialog("Ocorreu um erro ao tentar atualizar as permissões", e);
                //LogException(e);
                return BadRequest(e.ToString());
            }

            return Ok();
        }


        #endregion
    }
}