using System;
using System.Collections.Generic;
using System.Web.Mvc;
using AutoMapper;
using ArchitectureTemplate.Business.DataEntities;
using ArchitectureTemplate.Business.Interfaces.Services;
using ArchitectureTemplate.Infraestrutura.CrossCutting.Support.Extensions;
using ArchitectureTemplate.Infraestrutura.CrossCutting.Support.Resources;
using ArchitectureTemplate.Mvc.Controllers.Shared;
using ArchitectureTemplate.Mvc.Models;

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
        public ActionResult Index(int idPag = 0, long? processoId = null, string key = null)
        {
            try
            {
                _pagination.PaginaAtual = idPag;

                var entidade = _logService.Get(_pagination, processoId, key);
                var model = Mapper.Map<IEnumerable<Log>, IEnumerable<LogModel>>(entidade);

                var paginar = _pagination.CalcularPagination(_pagination, _logService.Count(processoId, key));
                ViewBag.PaginaAtual = paginar.PaginaAtual;
                ViewBag.QtdePaginas = paginar.QtdePaginas;
                ViewBag.ProcessoId = processoId;
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