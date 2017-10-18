using ArchitectureTemplate.Domain.Interfaces.Services;
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

        private readonly IUserService _userService;
        private readonly IEmailMailService _emailMailService;
        private readonly IMenuService _menuService;
        private readonly IHierarchyService _hierarchyService;

        private IAuthenticationManager AuthenticationManager => HttpContext
            .GetOwinContext().Authentication;

        #endregion

        #region Constructors

        public LoginController(IUserService userService, IEmailMailService emailMailService,
            IMenuService menuService, IHierarchyService hierarchyService)
        {
            _userService = userService;
            _emailMailService = emailMailService;
            _menuService = menuService;
            _hierarchyService = hierarchyService;
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
                var user = _userService.Login(model.Login, model.Password);
                if (user != null)
                {
                    if (user.CodigoRecover != null)
                        return RedirectToAction("ChangePassword",
                            new { @login = user.Login, @codRecover = user.CodigoRecover });

                    SignInAsync(user, model.RememberMe);

                    return RedirectToAction("Index");
                }

                ShowMessageDialog("User and/or passward invalid", Message.MessageKind.Error);
            }
            catch (Exception exception)
            {
                ShowMessageDialog("Something wrong happened when you tried to login!", exception);
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
                var user = _userService.RecuperarSenha(model.Email);

                if (user != null)
                {
                    var modelEmail = new EmailMail
                    {
                        From = ConfigurationManager.AppSettings["EmailFrom"],
                        To = new List<string> { model.Email },
                        Subject = "Password",
                        Body = "test"
                    };

                    _emailMailService.SendEmail(modelEmail);
                    ShowMessageDialog("Email successfully sent", Message.MessageKind.Success);
                }
                else
                {
                    ShowMessageDialog("Email not found in database", Message.MessageKind.Warning);
                }
            }
            catch (Exception exception)
            {
                ShowMessageDialog("Something wrong happened when you tried to send email for password recover", exception);
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
                _userService.ResetSenha(model.Login, model.CodigoRecover, model.NewPassword);
                ShowMessageDialog("Password changed successfully", Message.MessageKind.Success);
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
                _userService.EditSenha(CurrentUser.UserId, model.Password, model.NewPassword);
                ShowMessageDialog("Password changed successfully", Message.MessageKind.Success);

                return RedirectToAction("LogOff");
            }
            catch (Exception exception)
            {
                ShowMessageDialog("Could not change password!", exception);
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult AccessDenied(string id)
        {
            ShowMessageDialog($"There is no access permission for functionality {id} " +
                $"without of the system, without login on system, You have to log in", Message.MessageKind.Error, 10);
            return View("Index");
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult InvalidToken(long id)
        {
            ShowMessageDialog($"Your user has been activated in a later session!", Message.MessageKind.Error, 20);
            return View("Index");
        }

        #endregion

        #region Private Methods

        private void SignInAsync(dynamic user, bool isPersistent)
        {
            var userId = user.Id.ToString(CultureInfo.InvariantCulture);
            var menus = _menuService.GetIdsPorProfile(user.ProfileId);
            var hierarchys = user.HierarchyId != null
                ? _hierarchyService.GetHierarchyIdsForUser(user.HierarchyId)
                : user.ProfileId.Equals(ProfileResource.Administrator)
                    ? _hierarchyService.GetAllHierarchyIds()
                    : null;

            var clains = new List<Claim>
            {
                new Claim(ClaimTypes.Authentication, userId),
                new Claim(ClaimTypes.NameIdentifier, userId),
                new Claim(ClaimTypes.Role, user.Profile.Nome),
                new Claim(ClaimTypes.Name, user.Nome),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim("Login", user.Login),
                new Claim("Token", _userService.GenerateToken((long)user.Id)),
                new Claim("Cpf", user.Cpf.ToString(CultureInfo.InvariantCulture)),
                new Claim("ProfileId", user.ProfileId.ToString(CultureInfo.InvariantCulture)),
                new Claim("HierarchyId", user.HierarchyId != null
                    ? user.HierarchyId.ToString(CultureInfo.InvariantCulture)
                    : string.Empty),
                new Claim("IdsMenu", string.Join("|", menus)),
                new Claim("HierarchyIds", string.Join("|", hierarchys ?? "0"))
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