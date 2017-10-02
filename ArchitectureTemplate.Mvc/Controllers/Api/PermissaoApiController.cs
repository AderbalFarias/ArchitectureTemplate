using ArchitectureTemplate.Business.DataEntities;
using ArchitectureTemplate.Business.Interfaces.Services;
using ArchitectureTemplate.Mvc.Controllers.Shared;
using ArchitectureTemplate.Mvc.Models;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace ArchitectureTemplate.Mvc.Controllers.Api
{
    public class PermissaoApiController : ApiController
    {
        #region Fields

        private readonly IMenuService _menuService;
        private readonly IDictionaryAllService _dictionaryAllService;

        #endregion

        #region Constructors

        public PermissaoApiController(IMenuService menuService,
            IDictionaryAllService dictionaryAllService)
        {
            _menuService = menuService;
            _dictionaryAllService = dictionaryAllService;
        }

        #endregion

        #region Actions

        [IsAuthorize]
        [ActionType(AccessType.Read)]
        public IHttpActionResult Index(int id = 0)
        {
            try
            {
                var model = new MenuModel
                {
                    PerfilId = id,
                    PerfilDictionary = _dictionaryAllService.GetPerfilDictionary()
                };

                if (id == 0)
                {
                    model.MenuList = _menuService.GetAll();
                }
                else
                {
                    model.PerfilPorMenuList = new List<PerfilPorMenu>();
                }

                return Ok(model);
            }
            catch (Exception e)
            {
                //ShowMessageDialog(MensagensResource.ErroCarregar, e);
                return BadRequest(e.ToString());
            }
        }

        [IsAuthorize]
        [ActionType(AccessType.Read)]
        public IHttpActionResult List(int id)
        {
            try
            {
                var model = new MenuModel
                {
                    PerfilId = id,
                    PerfilPorMenuList = new List<PerfilPorMenu>()
                };

                return Ok(model);
            }
            catch (Exception e)
            {
                //LogException(e);
                return BadRequest(e.ToString());
            }
        }

        #endregion
    }
}