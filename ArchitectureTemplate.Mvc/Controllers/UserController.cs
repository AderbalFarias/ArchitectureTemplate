using ArchitectureTemplate.Domain.DataEntities;
using ArchitectureTemplate.Domain.Interfaces.Services;
using ArchitectureTemplate.Infraestrutura.CrossCutting.Support.Extensions;
using ArchitectureTemplate.Infraestrutura.CrossCutting.Support.Resources;
using ArchitectureTemplate.Mvc.Controllers.Shared;
using ArchitectureTemplate.Mvc.Models;
using ArchitectureTemplate.Mvc.Models.Shared;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ArchitectureTemplate.Mvc.Controllers
{
    public class UserController : CustomController
    {
        #region Fields

        private readonly IUserService _userService;
        private readonly IProfileService _ProfileService;
        private readonly IHierarchyService _hierarchyService;
        private readonly IEmailMailService _emailMailService;
        private readonly Pagination _pagination;

        #endregion

        #region Constructors

        public UserController(IUserService userService, IProfileService ProfileService,
            IHierarchyService hierarchyService, IEmailMailService emailMailService, Pagination pagination)
        {
            _userService = userService;
            _ProfileService = ProfileService;
            _hierarchyService = hierarchyService;
            _emailMailService = emailMailService;
            _pagination = pagination;
        }

        #endregion

        #region Actions

        // GET: User
        [HttpGet]
        //[Authorize(Roles = "Administrator")]
        [IsAuthorize]
        [ActionType(AccessType.Read)]
        public async Task<ActionResult> Index(int idPag = 0)
        {
            try
            {
                _pagination.PaginaAtual = idPag;

                var entidade = _userService.Get(_pagination);
                var model = Mapper.Map<IEnumerable<User>, IEnumerable<UserModel>>(entidade);

                var paginar = _pagination.CalcularPagination(_pagination, await _userService.CountAsync());
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

        // GET: User/Create
        [HttpGet]
        [IsAuthorize]
        [ActionType(AccessType.Create)]
        public async Task<ActionResult> Create()
        {
            try
            {
                return View(new UserModel
                {
                    Ativo = true,
                    ProfileDictionary = await _ProfileService.GetDictionaryAsync(),
                    HierarchyDictionary = await _hierarchyService.GetDictionaryAsync(),
                });
            }
            catch (Exception e)
            {
                ShowMessageDialog(MensagensResource.ErroCarregar, e);
                return RedirectToAction("Index");
            }
        }

        // POST: User/Create
        [HttpPost]
        [IsAuthorize]
        [ActionType(AccessType.Create)]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UserModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        if (_userService.Get(w => w.Email.Equals(model.Email)) != null)
                        {
                            ShowMessageDialog(MensagensResource.EmailExistente, Message.MessageKind.Warning);

                            model.ProfileDictionary = _ProfileService.GetDictionary();
                            model.HierarchyDictionary = _hierarchyService.GetDictionary();

                            return View(model);
                        }

                        model.Senha = _userService.GetCodigoRecover();
                        model.CodigoRecover = _userService.GetCodigoRecover();

                        var entity = Mapper.Map<UserModel, User>(model);
                        _userService.Add(entity, CurrentUser.UserId);

                        var email = new EmailMail
                        {
                            From = ConfigurationManager.AppSettings["EmailFrom"],
                            To = new List<string> { model.Email },
                            Subject = "New User",
                            Body = "You are a new user of the system..."
                        };

                        _emailMailService.SendEmail(email);
                    }
                    catch (Exception e)
                    {
                        ShowMessageDialog(MensagensResource.SucessoCadastrarErroEmail, e);
                        return RedirectToAction("Index");
                    }

                    ShowMessageDialog(MensagensResource.SucessoCadastrarSucessoEmail, Message.MessageKind.Success);
                }
                else
                {
                    model.ProfileDictionary = _ProfileService.GetDictionary();
                    model.HierarchyDictionary = _hierarchyService.GetDictionary();

                    return View(model);
                }
            }
            catch (Exception e)
            {
                ShowMessageDialog(MensagensResource.ErroCadastrar, e);
            }

            return RedirectToAction("Index");
        }

        // GET: User/Edit/5
        [HttpGet]
        [IsAuthorize]
        [ActionType(AccessType.Update)]
        public async Task<ActionResult> Edit(long id)
        {
            try
            {
                var model = Mapper.Map<User, UserModel>(_userService.GetId(id));
                model.ProfileDictionary = await _ProfileService.GetDictionaryAsync();
                model.HierarchyDictionary = await _hierarchyService.GetDictionaryAsync();

                return View(model);
            }
            catch (Exception e)
            {
                ShowMessageDialog(MensagensResource.ErroCarregar, e);
                return RedirectToAction("Index");
            }
        }

        // POST: User/Edit/5
        [HttpPut]
        [IsAuthorize]
        [ActionType(AccessType.Update)]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UserModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    model.Senha = _userService.GetSenha(model.Id);
                    _userService.Update(Mapper.Map<UserModel, User>(model), CurrentUser.UserId, true);

                    ShowMessageDialog(MensagensResource.SucessoAtualizar, Message.MessageKind.Success);
                }
                else
                {
                    model.ProfileDictionary = _ProfileService.GetDictionary();
                    model.HierarchyDictionary = _hierarchyService.GetDictionary();

                    return View(model);
                }
            }
            catch (Exception e)
            {
                ShowMessageDialog(MensagensResource.ErroAtualizar, e);
            }

            return RedirectToAction("Index");
        }

        // GET: User/Delete/5
        [HttpGet]
        [IsAuthorize]
        [ActionType(AccessType.Update)]
        //[AcceptVerbs(HttpVerbs.Put | HttpVerbs.Get)]
        public ActionResult Delete(long id)
        {
            try
            {
                _userService.DisableOrEnable(id, CurrentUser.UserId);
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
