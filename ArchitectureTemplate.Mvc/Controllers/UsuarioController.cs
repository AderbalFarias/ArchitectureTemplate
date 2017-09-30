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
using System.Configuration;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ArchitectureTemplate.Mvc.Controllers
{
    public class UsuarioController : CustomController
    {
        #region Fields

        private readonly IUsuarioService _usuarioService;
        private readonly IPerfilService _perfilService;
        private readonly IHierarquiaService _hierarquiaService;
        private readonly IEmailMailService _emailMailService;
        private readonly Pagination _pagination;

        #endregion

        #region Constructors

        public UsuarioController(IUsuarioService usuarioService, IPerfilService perfilService,
            IHierarquiaService hierarquiaService, IEmailMailService emailMailService, Pagination pagination)
        {
            _usuarioService = usuarioService;
            _perfilService = perfilService;
            _hierarquiaService = hierarquiaService;
            _emailMailService = emailMailService;
            _pagination = pagination;
        }

        #endregion

        #region Actions

        // GET: Usuario
        [HttpGet]
        //[Authorize(Roles = "Administrator")]
        [IsAuthorize]
        [ActionType(AccessType.Read)]
        public async Task<ActionResult> Index(int idPag = 0)
        {
            try
            {
                _pagination.PaginaAtual = idPag;

                var entidade = _usuarioService.Get(_pagination);
                var model = Mapper.Map<IEnumerable<Usuario>, IEnumerable<UsuarioModel>>(entidade);

                var paginar = _pagination.CalcularPagination(_pagination, await _usuarioService.CountAsync());
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

        // GET: Usuario/Create
        [HttpGet]
        [IsAuthorize]
        [ActionType(AccessType.Create)]
        public async Task<ActionResult> Create()
        {
            try
            {
                return View(new UsuarioModel
                {
                    Ativo = true,
                    PerfilDictionary = await _perfilService.GetDictionaryAsync(),
                    HierarquiaDictionary = await _hierarquiaService.GetDictionaryAsync(),
                });
            }
            catch (Exception e)
            {
                ShowMessageDialog(MensagensResource.ErroCarregar, e);
                return RedirectToAction("Index");
            }
        }

        // POST: Usuario/Create
        [HttpPost]
        [IsAuthorize]
        [ActionType(AccessType.Create)]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UsuarioModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        if (_usuarioService.Get(w => w.Email.Equals(model.Email)) != null)
                        {
                            ShowMessageDialog(MensagensResource.EmailExistente, Message.MessageKind.Warning);

                            model.PerfilDictionary = _perfilService.GetDictionary();
                            model.HierarquiaDictionary = _hierarquiaService.GetDictionary();

                            return View(model);
                        }

                        model.Senha = _usuarioService.GetCodigoRecover();
                        model.CodigoRecover = _usuarioService.GetCodigoRecover();

                        var entity = Mapper.Map<UsuarioModel, Usuario>(model);
                        _usuarioService.Add(entity, CurrentUser.UserId);

                        var email = new EmailMail
                        {
                            From = ConfigurationManager.AppSettings["EmailFrom"],
                            To = new List<string> { model.Email },
                            Subject = "test",
                            Body = "test"
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
                    model.PerfilDictionary = _perfilService.GetDictionary();
                    model.HierarquiaDictionary = _hierarquiaService.GetDictionary();

                    return View(model);
                }
            }
            catch (Exception e)
            {
                ShowMessageDialog(MensagensResource.ErroCadastrar, e);
            }

            return RedirectToAction("Index");
        }

        // GET: Usuario/Edit/5
        [HttpGet]
        [IsAuthorize]
        [ActionType(AccessType.Update)]
        public async Task<ActionResult> Edit(long id)
        {
            try
            {
                var model = Mapper.Map<Usuario, UsuarioModel>(_usuarioService.GetId(id));
                model.PerfilDictionary = await _perfilService.GetDictionaryAsync();
                model.HierarquiaDictionary = await _hierarquiaService.GetDictionaryAsync();

                return View(model);
            }
            catch (Exception e)
            {
                ShowMessageDialog(MensagensResource.ErroCarregar, e);
                return RedirectToAction("Index");
            }
        }

        // POST: Usuario/Edit/5
        [HttpPut]
        [IsAuthorize]
        [ActionType(AccessType.Update)]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UsuarioModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    model.Senha = _usuarioService.GetSenha(model.Id);
                    _usuarioService.Update(Mapper.Map<UsuarioModel, Usuario>(model), CurrentUser.UserId, true);

                    ShowMessageDialog(MensagensResource.SucessoAtualizar, Message.MessageKind.Success);
                }
                else
                {
                    model.PerfilDictionary = _perfilService.GetDictionary();
                    model.HierarquiaDictionary = _hierarquiaService.GetDictionary();

                    return View(model);
                }
            }
            catch (Exception e)
            {
                ShowMessageDialog(MensagensResource.ErroAtualizar, e);
            }

            return RedirectToAction("Index");
        }

        // GET: Usuario/Delete/5
        [HttpGet]
        [IsAuthorize]
        [ActionType(AccessType.Update)]
        //[AcceptVerbs(HttpVerbs.Put | HttpVerbs.Get)]
        public ActionResult Delete(long id)
        {
            try
            {
                _usuarioService.DisableOrEnable(id, CurrentUser.UserId);
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
