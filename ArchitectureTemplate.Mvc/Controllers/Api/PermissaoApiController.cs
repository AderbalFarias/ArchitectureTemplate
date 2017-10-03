using ArchitectureTemplate.Business.Interfaces.Services;
using ArchitectureTemplate.Mvc.Controllers.Shared;
using System;
using System.Security.Claims;
using System.Web.Http;

namespace ArchitectureTemplate.Mvc.Controllers.Api
{
    public class PermissaoApiController : ApiController
    {
        #region Fields

        #region Fields

        private readonly IPermissaoService _permissaoService;

        #endregion

        #region Constructors

        public PermissaoApiController(IPermissaoService permissaoService)
        {
            _permissaoService = permissaoService;
        }

        #endregion


        [HttpGet]
        [IsAuthorize]
        [Authorize(Roles = "Administrator")]
        [ActionType(AccessType.Update)]
        //public PartialViewResult List(int id)
        public IHttpActionResult EnableOrDisable(int id, int telaId, string parametro, long permissaoId = 0, double scroll = 0)
        {
            try
            {
                var userClain = this.User.Identity as ClaimsIdentity;
                var x = Convert.ToInt64(userClain?.FindFirst(ClaimTypes.NameIdentifier).Value);

                _permissaoService.EnableOrDisabled(id, telaId, parametro, permissaoId, x);
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
        public IHttpActionResult EnableOrDisableAll(int id, int telaId, bool ativar, long permissaoId = 0, double scroll = 0)
        {
            try
            {
                var userClain = this.User.Identity as ClaimsIdentity;
                var x = Convert.ToInt64(userClain?.FindFirst(ClaimTypes.NameIdentifier).Value);

                _permissaoService.EnableOrDisabled(id, telaId, ativar, permissaoId, x);
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