using ArchitectureTemplate.Business.DataEntities;
using ArchitectureTemplate.Business.Interfaces.Services;
using ArchitectureTemplate.Mvc.Controllers.Shared;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace ArchitectureTemplate.Mvc.Controllers.Api
{
    [Authorize]
    public class PerfilApiController : ApiController
    {
        #region Fields

        private readonly IPerfilService _perfilService;

        #endregion

        #region Constructors

        public PerfilApiController(IPerfilService perfilService)
        {
            _perfilService = perfilService;
        }

        #endregion

        [IsAuthorize]
        [ActionType(AccessType.Read)]
        public IEnumerable<Perfil> Get()
        {
            return _perfilService.GetAll().ToList();
        }

        [IsAuthorize]
        [ActionType(AccessType.Read)]
        // GET api/<controller>/5
        public string Get(int id)
        {
            return _perfilService.GetId(id).Nome;
        }

        // POST api/<controller>
        [IsAuthorize]
        [ActionType(AccessType.Create)]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        [IsAuthorize]
        [ActionType(AccessType.Update)]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [IsAuthorize]
        [ActionType(AccessType.Delete)]
        public void Delete(int id)
        {
        }
    }
}