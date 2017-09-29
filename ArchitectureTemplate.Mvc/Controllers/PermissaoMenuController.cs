using ArchitectureTemplate.Business.DataEntities;
using ArchitectureTemplate.Business.Interfaces.Services;
using ArchitectureTemplate.Infraestrutura.CrossCutting.Support.Resources;
using ArchitectureTemplate.Mvc.Controllers.Shared;
using ArchitectureTemplate.Mvc.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ArchitectureTemplate.Mvc.Controllers
{
    public class PermissaoMenuController : CustomController
    {
        #region Fields

        private readonly IMenuService _menuService;
        private readonly IDictionaryAllService _dictionaryAllService;

        #endregion

        #region Constructors

        public PermissaoMenuController(IMenuService menuService, IDictionaryAllService dictionaryAllService)
        {
            _menuService = menuService;
            _dictionaryAllService = dictionaryAllService;
        }

        #endregion

        #region Actions

        [HttpGet]
        //[IsAuthorize]
        [Authorize(Roles = "Administrador")]
        [ActionType(AccessType.Read)]
        public async Task<ActionResult> Index(int id = 0)
        {
            try
            {
                var model = new MenuModel
                {
                    PerfilId = id,
                    PerfilDictionary = await _dictionaryAllService.GetPerfilDictionaryAsync()
                };

                if (id == 0)
                {
                    model.MenuList = await _menuService.GetAllAsync();
                }
                else
                {
                    model.PerfilPorMenuList = new List<PerfilPorMenu>();//_menuService.GetPorPerfil(id);
                }

                return View(model);
            }
            catch (Exception e)
            {
                ShowMessageDialog(MensagensResource.ErroCarregar, e);
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpGet]
        //[IsAuthorize]
        [Authorize(Roles = "Administrador")]
        [ActionType(AccessType.Read)]
        //public PartialViewResult List(int id)
        public ActionResult List(int id)
        {
            try
            {
                var model = new MenuModel
                {
                    PerfilId = id,
                    PerfilPorMenuList = new List<PerfilPorMenu>()//_menuService.GetPorPerfil(id);
                };

                return PartialView("_List", model);
            }
            catch (Exception e)
            {
                LogException(e);
                return Content("Error");
            }
        }

        [HttpGet]
        [IsAuthorize]
        [ActionType(AccessType.Read)]
        public JsonResult GetHtml()
        {
            try
            {
                var menu = System.IO.File.ReadAllText(Server.MapPath("~/Views/Shared/_MenuLateral.cshtml"));

                return Json(menu, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                LogException(e);
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion
    }
}