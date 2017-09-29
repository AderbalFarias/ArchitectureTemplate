using ArchitectureTemplate.Business.DataEntities;
using ArchitectureTemplate.Business.Interfaces.Services;
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
    public class HierarquiaController : CustomController
    {
        #region Fields

        private readonly IHierarquiaService _hierarquiaService;
        private readonly IDictionaryAllService _dictionaryAllService;

        #endregion

        #region Constructors

        public HierarquiaController(IHierarquiaService hierarquiaService, IDictionaryAllService dictionaryAllService)
        {
            _hierarquiaService = hierarquiaService;
            _dictionaryAllService = dictionaryAllService;
        }

        #endregion

        #region Actions

        [HttpGet]
        [IsAuthorize]
        [ActionType(AccessType.Read)]
        public async Task<ActionResult> Index()
        {
            try
            {
                var entidade = await _hierarquiaService.GetAsync();
                var model = Mapper.Map<IEnumerable<Hierarquia>, IEnumerable<HierarquiaModel>>(entidade);
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
        public async Task<ActionResult> Create(int idPai)
        {
            try
            {
                return View(new HierarquiaModel
                {
                    Id = 0,
                    HierarquiaPaiId = idPai,
                    TipoHierarquiaDictionary = await _dictionaryAllService.GetTipoHierarquiaDictionaryAsync(),
                });
            }
            catch (Exception e)
            {
                ShowMessageDialog(MensagensResource.ErroCarregar, e);
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        [IsAuthorize]
        [ActionType(AccessType.Create)]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(HierarquiaModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _hierarquiaService.Add(Mapper.Map<HierarquiaModel, Hierarquia>(model), CurrentUser.UserId);
                    ShowMessageDialog(MensagensResource.SucessoCadastrar, Message.MessageKind.Success);
                }
                else
                {
                    model.TipoHierarquiaDictionary = await _dictionaryAllService.GetTipoHierarquiaDictionaryAsync();
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
                var model = Mapper.Map<Hierarquia, HierarquiaModel>(_hierarquiaService.Get(id));
                model.TipoHierarquiaDictionary = await _dictionaryAllService.GetTipoHierarquiaDictionaryAsync();

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
        public async Task<ActionResult> Edit(HierarquiaModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var entiry = Mapper.Map<HierarquiaModel, Hierarquia>(model);

                    _hierarquiaService.UpdateDetalhe(entiry.HierarquiaDetalhe, CurrentUser.UserId);
                    _hierarquiaService.Update(entiry, CurrentUser.UserId, referenceCircular: true);
                    ShowMessageDialog(MensagensResource.SucessoAtualizar, Message.MessageKind.Success);
                }
                else
                {
                    model.TipoHierarquiaDictionary = await _dictionaryAllService.GetTipoHierarquiaDictionaryAsync();

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
        [ActionType(AccessType.Delete)]
        public ActionResult Delete(long id)
        {
            try
            {
                _hierarquiaService.Remove(id, CurrentUser.UserId);
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
