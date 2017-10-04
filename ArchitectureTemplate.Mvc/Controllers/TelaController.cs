using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using AutoMapper;
using ArchitectureTemplate.Business.DataEntities;
using ArchitectureTemplate.Business.Interfaces.Services;
using ArchitectureTemplate.Infraestrutura.CrossCutting.Support.Extensions;
using ArchitectureTemplate.Infraestrutura.CrossCutting.Support.Resources;
using ArchitectureTemplate.Mvc.Controllers.Shared;
using ArchitectureTemplate.Mvc.Models;
using ArchitectureTemplate.Mvc.Models.Shared;

namespace ArchitectureTemplate.Mvc.Controllers
{
    public class TelaController : CustomController
    {
        #region Fields
        
        private readonly ITelaService _telaService;
        private readonly Pagination _pagination;

        #endregion

        #region Constructors

        public TelaController(ITelaService telaService, Pagination pagination)
        {
            _telaService = telaService;
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

                var entidade = _telaService.Get(_pagination);
                var model = Mapper.Map<IEnumerable<Tela>, IEnumerable<TelaModel>>(entidade);

                var paginar = _pagination.CalcularPagination(_pagination, _telaService.Count());
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
                var model = new TelaModel();
                var controllersList = model.MapperControllers();
                _telaService.Synchronize(controllersList, CurrentUser.UserId);

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
                var model = Mapper.Map<Tela, TelaModel>(await _telaService.GetIdAsync(id));
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
        public ActionResult Edit(TelaModel model)
        {
            try
            {
                _telaService.Update(Mapper.Map<TelaModel, Tela>(model), CurrentUser.UserId);
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