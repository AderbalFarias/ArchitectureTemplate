using ArchitectureTemplate.Domain.Interfaces.Services;
using ArchitectureTemplate.Infraestrutura.CrossCutting.Support.Resources;
using ArchitectureTemplate.Mvc.Controllers.Shared;
using ArchitectureTemplate.Mvc.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ArchitectureTemplate.Mvc.Controllers
{
    public class MenuController : CustomController
    {
        #region Fields

        private readonly IMenuService _menuService;
        private readonly IDictionaryAllService _dictionaryAllService;

        #endregion

        #region Constructors

        public MenuController(IMenuService menuService, IDictionaryAllService dictionaryAllService)
        {
            _menuService = menuService;
            _dictionaryAllService = dictionaryAllService;
        }

        #endregion

        #region Actions

        [HttpGet]
        //[IsAuthorize]
        [Authorize(Roles = "Administrator")]
        [ActionType(AccessType.Read)]
        public async Task<ActionResult> Index(int id = 0, double scroll = 0)
        {
            try
            {
                var model = new MenuModel
                {
                    ProfileId = id,
                    ProfileDictionary = await _dictionaryAllService.GetProfileDictionaryAsync(),
                    Scroll = scroll
                };

                if (id == 0)
                {
                    model.MenuList = await _menuService.GetAllAsync();
                }
                else
                {
                    model.ProfileForMenuList = _menuService.GetPorProfile(id);
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
        [Authorize(Roles = "Administrator")]
        [ActionType(AccessType.Read)]
        //public PartialViewResult List(int id)
        public ActionResult List(int id)
        {
            try
            {
                var model = new MenuModel
                {
                    ProfileId = id,
                    ProfileForMenuList = _menuService.GetPorProfile(id)
                };

                return PartialView("_List", model);
            }
            catch (Exception e)
            {
                LogException(e);
                return Content("Error");
            }
        }

        [HttpPost]
        //[IsAuthorize]
        [Authorize(Roles = "Administrator")]
        [ActionType(AccessType.Update)]
        public JsonResult Synchronize(IEnumerable<string> menuList)
        {
            try
            {
                _menuService.Synchronize(menuList, CurrentUser.UserId);
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                LogException(e);
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        //[IsAuthorize]
        [Authorize(Roles = "Administrator")]
        [ActionType(AccessType.Update)]
        public ActionResult EnableOrDisable(int id, int menuId, long permissionId = 0, double scroll = 0)
        {
            try
            {
                _menuService.EnableOrDisabled(id, menuId, permissionId, CurrentUser.UserId);
                //ShowMessageDialog(MensagensResource.SucessoAtualizar, Message.MessageKind.Success);
            }
            catch (Exception e)
            {
                ShowMessageDialog("There was an error when you trying to update the menu", e);
            }

            return RedirectToAction("Index", new { id, scroll });
        }

        [HttpGet]
        //[IsAuthorize]
        [Authorize(Roles = "Administrator")]
        [ActionType(AccessType.Update)]
        public ActionResult EnableOrDisableAll(int id, bool ativar, double scroll = 0)
        {
            try
            {
                _menuService.EnableOrDisabledAll(id, ativar, CurrentUser.UserId);
            }
            catch (Exception e)
            {
                ShowMessageDialog("There was an error when you trying to update the menu", e);
            }

            return RedirectToAction("Index", new { id, scroll });
        }

        [HttpGet]
        //[IsAuthorize]
        [Authorize(Roles = "Administrator")]
        [ActionType(AccessType.Read)]
        public JsonResult GetHtml()
        {
            try
            {
                var menuTop = System.IO.File
                    .ReadAllText(Server.MapPath("~/Views/Shared/_MenuTop.cshtml"))
                    .Replace("~/wwwroot/images/logo.png", "");

                var menuLateral = System.IO.File
                    .ReadAllText(Server.MapPath("~/Views/Shared/_MenuLateral.cshtml"));

                return Json($"{menuTop} {menuLateral}", JsonRequestBehavior.AllowGet);
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