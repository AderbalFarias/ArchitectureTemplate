using ArchitectureTemplate.Domain.DataEntities;
using ArchitectureTemplate.Domain.Interfaces.Services;
using ArchitectureTemplate.Infraestrutura.CrossCutting.Support.Extensions;
using ArchitectureTemplate.Infraestrutura.CrossCutting.Support.Resources;
using ArchitectureTemplate.Mvc.Controllers.Shared;
using ArchitectureTemplate.Mvc.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace ArchitectureTemplate.Mvc.Controllers
{
    public class LogController : CustomController
    {
        #region Fields

        private readonly ILogService _logService;
        private readonly Pagination _pagination;

        #endregion

        #region Constructors

        public LogController(ILogService logService, Pagination pagination)
        {
            _logService = logService;
            _pagination = pagination;
        }

        #endregion

        #region Actions

        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        [IsAuthorize]
        [ActionType(AccessType.Read)]
        public ActionResult Index(int idPag = 0, long? testId = null, string key = null)
        {
            try
            {
                _pagination.PaginaAtual = idPag;

                var entidade = _logService.Get(_pagination, testId, key);
                var model = Mapper.Map<IEnumerable<Log>, IEnumerable<LogModel>>(entidade);

                var paginar = _pagination.CalculatePaging(_pagination, _logService.Count(testId, key));
                ViewBag.PaginaAtual = paginar.PaginaAtual;
                ViewBag.QtdePaginas = paginar.QtdePaginas;
                ViewBag.TestId = testId;
                ViewBag.Key = key;

                return View(model);
            }
            catch (Exception e)
            {
                ShowMessageDialog(MensagensResource.ErroCarregar, e);
                return RedirectToAction("Index", "Home");
            }
        }

        #endregion
    }
}