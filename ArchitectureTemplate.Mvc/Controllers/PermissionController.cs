using ArchitectureTemplate.Infraestrutura.CrossCutting.Support.Resources;
using ArchitectureTemplate.Mvc.Controllers.Shared;
using ArchitectureTemplate.Mvc.Models;
using OfficeOpenXml;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using ArchitectureTemplate.Domain.Interfaces.Services;

namespace ArchitectureTemplate.Mvc.Controllers
{
    public class PermissionController : CustomController
    {
        #region Fields

        private readonly IPermissionService _permissionService;
        private readonly IScreenService _screenService;
        private readonly IDictionaryAllService _dictionaryAllService;

        #endregion

        #region Constructors

        public PermissionController(IPermissionService permissionService, IScreenService screenService,
            IDictionaryAllService dictionaryAllService)
        {
            _permissionService = permissionService;
            _screenService = screenService;
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
                var model = new PermissionModel
                {
                    ProfileId = id,
                    ProfileDictionary = await _dictionaryAllService.GetProfileDictionaryAsync(),
                    Scroll = scroll
                };

                if (id == 0)
                {
                    model.ScreenList = await _screenService.GetAllAsync();
                }
                else
                {
                    model.ProfilePorScreenList = _permissionService.GetProfilePorScreen(id);
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
                var model = new PermissionModel
                {
                    ProfileId = id,
                    ProfilePorScreenList = _permissionService.GetProfilePorScreen(id)
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
        //[IsAuthorize]
        [Authorize(Roles = "Administrator")]
        [ActionType(AccessType.Update)]
        //public PartialViewResult List(int id)
        public ActionResult EnableOrDisable(int id, int screenId, string parametro, long permissionId = 0, double scroll = 0)
        {
            try
            {
                _permissionService.EnableOrDisabled(id, screenId, parametro, permissionId, CurrentUser.UserId);
                //ShowMessageDialog(MensagensResource.SucessoAtualizar, Message.MessageKind.Success);
            }
            catch (Exception e)
            {
                ShowMessageDialog("There was an error when you trying to update the permissions", e);
            }

            return RedirectToAction("Index", new { id, scroll });
        }

        [HttpGet]
        //[IsAuthorize]
        [Authorize(Roles = "Administrator")]
        [ActionType(AccessType.Update)]
        //public PartialViewResult List(int id)
        public ActionResult EnableOrDisableAll(int id, int screenId, bool ativar, long permissionId = 0, double scroll = 0)
        {
            try
            {
                _permissionService.EnableOrDisabled(id, screenId, ativar, permissionId, CurrentUser.UserId);
                //ShowMessageDialog(MensagensResource.SucessoAtualizar, Message.MessageKind.Success);
            }
            catch (Exception e)
            {
                ShowMessageDialog("There was an error when you trying to update the permissions", e);
            }

            return RedirectToAction("Index", new { id, scroll });
        }

        [HttpGet]
        //[IsAuthorize]
        [Authorize(Roles = "Administrator")]
        [ActionType(AccessType.Read)]
        //public FileResult ExportToExcel()
        public ActionResult ExportToExcel()
        {
            try
            {
                using (ExcelPackage pck = new ExcelPackage())
                {
                    _permissionService.LoadExportToExcel(pck);

                    return File(pck.GetAsByteArray(), "application/vnd.ms-excel",
                        $"Permissions_{DateTime.Now.ToString("yyyyMMdd_HHmm")}.xlsx");
                }
            }
            catch (Exception e)
            {
                ShowMessageDialog(MensagensResource.ErroDownload, e);
                return RedirectToAction("Index");
            }
        }

        #endregion
    }
}