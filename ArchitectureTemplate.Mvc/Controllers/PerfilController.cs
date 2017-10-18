﻿using ArchitectureTemplate.Infraestrutura.CrossCutting.Support.Extensions;
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
    public class ProfileController : CustomController
    {
        #region Fields

        private readonly IProfileService _ProfileService;
        //private readonly IDictionaryAllService _dictionaryAllService;
        private readonly Pagination _pagination;

        #endregion

        #region Constructors

        public ProfileController(IProfileService ProfileService,
            /*IDictionaryAllService dictionaryAllService,*/ Pagination pagination)
        {
            _ProfileService = ProfileService;
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

                var entidade = _ProfileService.GetAsync(_pagination);
                var model = Mapper.Map<IEnumerable<Profile>, IEnumerable<ProfileModel>>(await entidade);

                var paginar = _pagination.CalcularPagination(_pagination, await _ProfileService.CountAsync());
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
            return View(new ProfileModel { Ativo = true });
        }

        [HttpPost]
        [IsAuthorize]
        [ActionType(AccessType.Create)]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProfileModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        model.DataCadastro = DateTime.Now;
                        _ProfileService.Add(Mapper.Map<ProfileModel, Profile>(model), CurrentUser.UserId);
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
                var model = Mapper.Map<Profile, ProfileModel>(await _ProfileService.GetIdAsync(id));
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
        public ActionResult Edit(ProfileModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _ProfileService.Update(Mapper.Map<ProfileModel, Profile>(model), CurrentUser.UserId);
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
                _ProfileService.DisableOrEnable(id, CurrentUser.UserId);
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