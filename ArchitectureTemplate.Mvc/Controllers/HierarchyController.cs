using ArchitectureTemplate.Infraestrutura.CrossCutting.Support.Resources;
using ArchitectureTemplate.Mvc.Controllers.Shared;
using ArchitectureTemplate.Mvc.Models;
using ArchitectureTemplate.Mvc.Models.Shared;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using ArchitectureTemplate.Domain.DataEntities;
using ArchitectureTemplate.Domain.Interfaces.Services;

namespace ArchitectureTemplate.Mvc.Controllers
{
    public class HierarchyController : CustomController
    {
        #region Fields

        private readonly IHierarchyService _hierarchyService;
        private readonly IDictionaryAllService _dictionaryAllService;

        #endregion

        #region Constructors

        public HierarchyController(IHierarchyService hierarchyService, IDictionaryAllService dictionaryAllService)
        {
            _hierarchyService = hierarchyService;
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
                var entidade = await _hierarchyService.GetAsync();
                var model = Mapper.Map<IEnumerable<Hierarchy>, IEnumerable<HierarchyModel>>(entidade);
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
                return View(new HierarchyModel
                {
                    Id = 0,
                    HierarchyPaiId = idPai,
                    HierarchyTypeDictionary = await _dictionaryAllService.GetHierarchyTypeDictionaryAsync(),
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
        public async Task<ActionResult> Create(HierarchyModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _hierarchyService.Add(Mapper.Map<HierarchyModel, Hierarchy>(model), CurrentUser.UserId);
                    ShowMessageDialog(MensagensResource.SucessoCadastrar, Message.MessageKind.Success);
                }
                else
                {
                    model.HierarchyTypeDictionary = await _dictionaryAllService.GetHierarchyTypeDictionaryAsync();
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
                var model = Mapper.Map<Hierarchy, HierarchyModel>(_hierarchyService.Get(id));
                model.HierarchyTypeDictionary = await _dictionaryAllService.GetHierarchyTypeDictionaryAsync();

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
        public async Task<ActionResult> Edit(HierarchyModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var entiry = Mapper.Map<HierarchyModel, Hierarchy>(model);

                    _hierarchyService.UpdateDetalhe(entiry.HierarchyDetalhe, CurrentUser.UserId);
                    _hierarchyService.Update(entiry, CurrentUser.UserId, referenceCircular: true);
                    ShowMessageDialog(MensagensResource.SucessoAtualizar, Message.MessageKind.Success);
                }
                else
                {
                    model.HierarchyTypeDictionary = await _dictionaryAllService.GetHierarchyTypeDictionaryAsync();

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
                _hierarchyService.Remove(id, CurrentUser.UserId);
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
