using ArchitectureTemplate.Business.Interfaces.Services;
using ArchitectureTemplate.Infraestrutura.CrossCutting.Support.Extensions;
using ArchitectureTemplate.Infraestrutura.CrossCutting.Support.Resources;
using ArchitectureTemplate.Mvc.Controllers.Shared;
using ArchitectureTemplate.Mvc.Models;
using ArchitectureTemplate.Mvc.Models.Shared;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace ArchitectureTemplate.Mvc.Controllers
{
    public class LoginController : CustomController
    {
        #region Fields

        private readonly IUsuarioService _usuarioService;
        private readonly IEmailMailService _emailMailService;
        private readonly IMenuService _menuService;
        private readonly IHierarquiaService _hierarquiaService;

        private IAuthenticationManager AuthenticationManager => HttpContext
            .GetOwinContext().Authentication;

        #endregion

        #region Constructors

        public LoginController(IUsuarioService usuarioService, IEmailMailService emailMailService,
            IMenuService menuService, IHierarquiaService hierarquiaService)
        {
            _usuarioService = usuarioService;
            _emailMailService = emailMailService;
            _menuService = menuService;
            _hierarquiaService = hierarquiaService;
        }

        #endregion

        #region Actions 

        [HttpGet]
        [AllowAnonymous]
        [ActionType(AccessType.Read)]
        [Route("Login")]
        public ActionResult Index()
        {
            if (!AuthenticationManager.User.Identity.IsAuthenticated)
                return View();

            //return View();
            LogAcesso(CurrentUser.Login);
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [AllowAnonymous]
        [ActionType(AccessType.Read)]
        [ValidateAntiForgeryToken]
        public ActionResult LogIn(LoginModel model)
        {
            try
            {
                LogAcesso(model.Login);
                var user = _usuarioService.Login(model.Login, model.Password);
                if (user != null)
                {
                    if (user.CodigoRecover != null)
                        return RedirectToAction("ChangePassword",
                            new { @login = user.Login, @codRecover = user.CodigoRecover });

                    SignInAsync(user, model.RememberMe);

                    return RedirectToAction("Index");
                }

                ShowMessageDialog("Usuário e/ou senha inválidos", Message.MessageKind.Error);
            }
            catch (Exception exception)
            {
                ShowMessageDialog("Ocorreu um erro ao tentar efetuar login!", exception);
            }

            return View("Index");
        }

        [HttpGet]
        [ActionType(AccessType.Read)]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut();

            return RedirectToAction("Index");
        }

        [HttpGet]
        [AllowAnonymous]
        [ActionType(AccessType.Read)]
        public ActionResult RecoverPassword()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ActionType(AccessType.Update)]
        [ValidateAntiForgeryToken]
        public ActionResult RecoverPassword(LoginModel model)
        {
            try
            {
                var usuario = _usuarioService.RecuperarSenha(model.Email);

                if (usuario != null)
                {
                    var modelEmail = new EmailMail
                    {
                        From = ConfigurationManager.AppSettings["EmailFrom"],
                        To = new List<string> { model.Email },
                        Subject = "Password",
                        Body = "test"
                    };

                    _emailMailService.SendEmail(modelEmail);
                    ShowMessageDialog("Email enviado com sucesso!", Message.MessageKind.Success);
                }
                else
                {
                    ShowMessageDialog("Email não encontrado na base de dados!", Message.MessageKind.Warning);
                }
            }
            catch (Exception exception)
            {
                ShowMessageDialog("Ocorreu um erro ao tentar enviar o email de recuperação da senha!", exception);
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        [AllowAnonymous]
        [ActionType(AccessType.Update)]
        public ActionResult ResetPassword()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ActionType(AccessType.Update)]
        [ValidateAntiForgeryToken]
        public ActionResult ResetPassword(LoginModel model)
        {
            try
            {
                _usuarioService.ResetSenha(model.Login, model.CodigoRecover, model.NewPassword);
                ShowMessageDialog("Senha alterada com sucesso!", Message.MessageKind.Success);
            }
            catch (Exception exception)
            {
                ShowMessageDialog("Ocorreu um erro ao tentar alterar a senha!", exception);
            }

            return View("Index");
        }

        [HttpGet]
        [AllowAnonymous]
        [ActionType(AccessType.Read)]
        public ViewResult ChangePassword(string login, string codRecover)
        {
            var model = new LoginModel
            {
                Login = login,
                CodigoRecover = codRecover
            };

            return View("ResetPassword", model);
        }

        [HttpGet]
        [IsAuthorize]
        [ActionType(AccessType.Update)]
        public ActionResult EditPassword()
        {
            return View();
        }

        [HttpPut]
        [IsAuthorize]
        [ActionType(AccessType.Update)]
        [ValidateAntiForgeryToken]
        public ActionResult EditPassword(LoginModel model)
        {
            try
            {
                _usuarioService.EditSenha(CurrentUser.UserId, model.Password, model.NewPassword);
                ShowMessageDialog("Senha alterada com sucesso!", Message.MessageKind.Success);

                return RedirectToAction("LogOff");
            }
            catch (Exception exception)
            {
                ShowMessageDialog("Não foi possível alterar a senha!", exception);
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult AccessDenied(string id)
        {
            ShowMessageDialog($"Não há permissão de acesso para a funcionalidade {id} sem acessar o sitema", Message.MessageKind.Error, 10);
            return View("Index");
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult InvalidToken(long id)
        {
            ShowMessageDialog($"Seu usuário foi ativado em uma outra sessão mais recente!", Message.MessageKind.Error, 20);
            return View("Index");
        }

        #endregion

        #region Private Methods

        private void SignInAsync(dynamic user, bool isPersistent)
        {
            var userId = user.Id.ToString(CultureInfo.InvariantCulture);
            var menus = _menuService.GetIdsPorPerfil(user.PerfilId);
            var hierarquias = user.HierarquiaId != null
                ? _hierarquiaService.GetHierarquiaIdsForUser(user.HierarquiaId)
                : user.PerfilId.Equals(PerfilResource.Administrator)
                    ? _hierarquiaService.GetAllHierarquiaIds()
                    : null;

            var clains = new List<Claim>
            {
                new Claim(ClaimTypes.Authentication, userId),
                new Claim(ClaimTypes.NameIdentifier, userId),
                new Claim(ClaimTypes.Role, user.Perfil.Nome),
                new Claim(ClaimTypes.Name, user.Nome),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim("Login", user.Login),
                new Claim("Token", _usuarioService.GenerateToken((long)user.Id)),
                new Claim("Cpf", user.Cpf.ToString(CultureInfo.InvariantCulture)),
                new Claim("PerfilId", user.PerfilId.ToString(CultureInfo.InvariantCulture)),
                new Claim("HierarquiaId", user.HierarquiaId != null
                    ? user.HierarquiaId.ToString(CultureInfo.InvariantCulture)
                    : string.Empty),
                new Claim("IdsMenu", string.Join("|", menus)),
                new Claim("IdsHierarquia", string.Join("|", hierarquias ?? "0"))
            };

            var identity = new ClaimsIdentity(clains, DefaultAuthenticationTypes.ApplicationCookie);
            var ctx = Request.GetOwinContext();
            var authManager = ctx.Authentication;
            authManager.SignIn(new AuthenticationProperties { IsPersistent = isPersistent }, identity);
        }

        private void LogAcesso(string login)
        {
            var description = $"{DateTime.Now} | " +
                $"IP Fixo:{Request.ServerVariables["HTTP_X_FORWARDED_FOR"]} | " +
                $"IP Virtual: {Request.UserHostAddress} | Host Name: {Request.UserHostName}";

            var local = Server.MapPath(@"~/Files/LogAcesso");

            if (!Directory.Exists(local))
                Directory.CreateDirectory(local);

            var fileName = Path.Combine(local, login + ".txt");

            using (var file = new StreamWriter(fileName, true))
            {
                file.WriteLine(description);
            }
        }

        //private HttpCookie CreateMenuCookie(List<string> list)
        //{
        //    HttpCookie studentCookies = new HttpCookie("MenuCookie")
        //    {
        //        Value = string.Join("|", list),
        //        Expires = DateTime.Now.AddDays(2)
        //    };

        //    return studentCookies;
        //}

        #endregion
    }
}