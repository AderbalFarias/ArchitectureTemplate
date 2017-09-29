using ArchitectureTemplate.Business.DataEntities;
using ArchitectureTemplate.Business.Interfaces.Services;
using ArchitectureTemplate.Infraestrutura.CrossCutting.Support.Extensions;
using ArchitectureTemplate.Infraestrutura.CrossCutting.Support.Resources;
using ArchitectureTemplate.Mvc.Controllers.Shared;
using ArchitectureTemplate.Mvc.Models;
using ArchitectureTemplate.Mvc.Models.Shared;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ArchitectureTemplate.Mvc.Controllers
{
    public class PerfilController : CustomController
    {
        #region Fields

        private readonly IPerfilService _perfilService;
        //private readonly IDictionaryAllService _dictionaryAllService;
        private readonly Pagination _pagination;

        #endregion

        #region Constructors

        public PerfilController(IPerfilService perfilService,
            /*IDictionaryAllService dictionaryAllService,*/ Pagination pagination)
        {
            _perfilService = perfilService;
            //_dictionaryAllService = dictionaryAllService;
            _pagination = pagination;
        }

        #endregion

        #region Actions

        [HttpGet]
        [IsAuthorize]
        [ActionType(AccessType.Read)]
        public async Task<ActionResult> Index(int idPag = 0)
        {
            try
            {
                _pagination.PaginaAtual = idPag;

                var entidade = _perfilService.GetAsync(_pagination);
                var model = Mapper.Map<IEnumerable<Perfil>, IEnumerable<PerfilModel>>(await entidade);

                var paginar = _pagination.CalcularPagination(_pagination, await _perfilService.CountAsync());
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
        [IsAuthorize]
        [ActionType(AccessType.Create)]
        public ActionResult Create()
        {
            return View(new PerfilModel { Ativo = true });
        }

        [HttpPost]
        [IsAuthorize]
        [ActionType(AccessType.Create)]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PerfilModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        model.DataCadastro = DateTime.Now;
                        _perfilService.Add(Mapper.Map<PerfilModel, Perfil>(model), CurrentUser.UserId);
                        ShowMessageDialog(MensagensResource.SucessoCadastrar, Message.MessageKind.Success);
                    }
                    catch (Exception e)
                    {
                        ShowMessageDialog(MensagensResource.ErroCadastrar, e);
                    }
                }
                else
                {
                    return View(model);
                }
            }
            catch (Exception e)
            {
                ShowMessageDialog(MensagensResource.ErroCadastrar, e);
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        [IsAuthorize]
        [ActionType(AccessType.Update)]
        public async Task<ActionResult> Edit(long id)
        {
            try
            {
                var model = Mapper.Map<Perfil, PerfilModel>(await _perfilService.GetIdAsync(id));
                return View(model);
            }
            catch (Exception e)
            {
                ShowMessageDialog(MensagensResource.ErroCarregar, e);
                return RedirectToAction("Index");
            }
        }

        [HttpPut]
        [IsAuthorize]
        [ActionType(AccessType.Update)]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PerfilModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _perfilService.Update(Mapper.Map<PerfilModel, Perfil>(model), CurrentUser.UserId);
                    ShowMessageDialog(MensagensResource.SucessoAtualizar, Message.MessageKind.Success);
                }
                else
                {
                    return View(model);
                }
            }
            catch (Exception e)
            {
                ShowMessageDialog(MensagensResource.ErroAtualizar, e);
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        [IsAuthorize]
        [ActionType(AccessType.Update)]
        public ActionResult Delete(long id)
        {
            try
            {
                _perfilService.DisableOrEnable(id, CurrentUser.UserId);
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