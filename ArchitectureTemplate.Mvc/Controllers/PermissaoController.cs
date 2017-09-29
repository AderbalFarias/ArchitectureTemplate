﻿using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using ArchitectureTemplate.Business.Interfaces.Services;
using ArchitectureTemplate.Infraestrutura.CrossCutting.Support.Resources;
using ArchitectureTemplate.Mvc.Controllers.Shared;
using ArchitectureTemplate.Mvc.Models;
using OfficeOpenXml;

namespace ArchitectureTemplate.Mvc.Controllers
{
    public class PermissaoController : CustomController
    {
        #region Fields
        
        private readonly IPermissaoService _permissaoService;
        private readonly ITelaService _telaService;
        private readonly IDictionaryAllService _dictionaryAllService;

        #endregion

        #region Constructors

        public PermissaoController(IPermissaoService permissaoService, ITelaService telaService, 
            IDictionaryAllService dictionaryAllService)
        {
            _permissaoService = permissaoService;
            _telaService = telaService;
            _dictionaryAllService = dictionaryAllService;
        }

        #endregion

        #region Actions

        [HttpGet]
        //[IsAuthorize]
        [Authorize(Roles = "Administrador")]
        [ActionType(AccessType.Read)]
        public async Task<ActionResult> Index(int id = 0, double scroll = 0)
        {
            try
            {
                var model = new PermissaoModel
                {
                    PerfilId = id,
                    PerfilDictionary = await _dictionaryAllService.GetPerfilDictionaryAsync(),
                    Scroll = scroll
                };

                if (id == 0)
                {
                    model.TelaList = await _telaService.GetAllAsync();
                }
                else
                {
                    model.PerfilPorTelaList = _permissaoService.GetPerfilPorTela(id);
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
                var model = new PermissaoModel
                {
                    PerfilId = id,
                    PerfilPorTelaList = _permissaoService.GetPerfilPorTela(id)
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
        [Authorize(Roles = "Administrador")]
        [ActionType(AccessType.Update)]
        //public PartialViewResult List(int id)
        public ActionResult EnableOrDisable(int id, int telaId, string parametro, long permissaoId = 0, double scroll = 0)
        {
            try
            {
                _permissaoService.EnableOrDisabled(id, telaId, parametro, permissaoId, CurrentUser.UserId);
                //ShowMessageDialog(MensagensResource.SucessoAtualizar, Message.MessageKind.Success);
            }
            catch (Exception e)
            {
                ShowMessageDialog("Ocorreu um erro ao tentar atualizar as permissões", e);
            }

            return RedirectToAction("Index", new { id, scroll });
        }

        [HttpGet]
        //[IsAuthorize]
        [Authorize(Roles = "Administrador")]
        [ActionType(AccessType.Update)]
        //public PartialViewResult List(int id)
        public ActionResult EnableOrDisableAll(int id, int telaId, bool ativar, long permissaoId = 0, double scroll = 0)
        {
            try
            {
                _permissaoService.EnableOrDisabled(id, telaId, ativar, permissaoId, CurrentUser.UserId);
                //ShowMessageDialog(MensagensResource.SucessoAtualizar, Message.MessageKind.Success);
            }
            catch (Exception e)
            {
                ShowMessageDialog("Ocorreu um erro ao tentar atualizar as permissões", e);
            }

            return RedirectToAction("Index", new { id, scroll });
        }

        [HttpGet]
        //[IsAuthorize]
        [Authorize(Roles = "Administrador")]
        [ActionType(AccessType.Read)]
        //public FileResult ExportToExcel()
        public ActionResult ExportToExcel()
        {
            try
            {
                using (ExcelPackage pck = new ExcelPackage())
                {
                    _permissaoService.LoadExportToExcel(pck);

                    return File(pck.GetAsByteArray(), "application/vnd.ms-excel",
                        $"Permissoes_{DateTime.Now.ToString("yyyyMMdd_HHmm")}.xlsx");
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