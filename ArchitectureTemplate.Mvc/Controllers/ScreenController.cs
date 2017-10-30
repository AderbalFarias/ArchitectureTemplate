using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using ArchitectureTemplate.Domain.DataEntities;
using ArchitectureTemplate.Domain.Interfaces.Services;
using AutoMapper;
using ArchitectureTemplate.Infraestrutura.CrossCutting.Support.Extensions;
using ArchitectureTemplate.Infraestrutura.CrossCutting.Support.Resources;
using ArchitectureTemplate.Mvc.Controllers.Shared;
using ArchitectureTemplate.Mvc.Models;
using ArchitectureTemplate.Mvc.Models.Shared;

namespace ArchitectureTemplate.Mvc.Controllers
{
    public class ScreenController : CustomController
    {
        #region Fields
        
        private readonly IScreenService _screenService;
        private readonly Pagination _pagination;

        #endregion

        #region Constructors

        public ScreenController(IScreenService screenService, Pagination pagination)
        {
            _screenService = screenService;
            _pagination = pagination;
        }

        #endregion

        #region Actions

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        [ActionType(AccessType.Read)]
        public ActionResult Index(int idPag = 0)
        {
            try
            {
                _pagination.PaginaAtual = idPag;
                _pagination.QtdeItensPagina = 20;

                var entidade = _screenService.Get(_pagination);
                var model = Mapper.Map<IEnumerable<Screen>, IEnumerable<ScreenModel>>(entidade);

                var paginar = _pagination.CalculatePaging(_pagination, _screenService.Count());
                ViewBag.PaginaAtual = paginar.PaginaAtual;
                ViewBag.QtdePaginas = paginar.QtdePaginas;

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
        [ActionType(AccessType.Update)]
        public ActionResult Synchronize()
        {
            try
            {
                var model = new ScreenModel();
                var controllersList = model.MapperControllers();
                _screenService.Synchronize(controllersList, CurrentUser.UserId);

                ShowMessageDialog("Screens synchronized successfully", Message.MessageKind.Success);
            }
            catch (Exception e)
            {
                ShowMessageDialog(MensagensResource.ErroCadastrar, e);
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        [ActionType(AccessType.Update)]
        public async Task<ActionResult> Edit(long id)
        {
            try
            {
                var model = Mapper.Map<Screen, ScreenModel>(await _screenService.GetIdAsync(id));
                return View(model);
            }
            catch (Exception e)
            {
                ShowMessageDialog(MensagensResource.ErroCarregar, e);
                return RedirectToAction("Index");
            }
        }

        [HttpPut]
        [Authorize(Roles = "Administrator")]
        [ActionType(AccessType.Update)]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ScreenModel model)
        {
            try
            {
                _screenService.Update(Mapper.Map<ScreenModel, Screen>(model), CurrentUser.UserId);
                ShowMessageDialog(MensagensResource.SucessoAtualizar, Message.MessageKind.Success);
            }
            catch (Exception e)
            {
                ShowMessageDialog(MensagensResource.ErroAtualizar, e);
            }

            return RedirectToAction("Index");
        }

        #endregion
    }
}