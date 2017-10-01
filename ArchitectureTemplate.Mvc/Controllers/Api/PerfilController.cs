using ArchitectureTemplate.Business.DataEntities;
using ArchitectureTemplate.Business.Interfaces.Services;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace ArchitectureTemplate.Mvc.Controllers.Api
{
    public class PerfilController : ApiController
    {
        #region Fields

        private readonly IPerfilService _perfilService;

        #endregion

        #region Constructors

        public PerfilController(IPerfilService perfilService)
        {
            _perfilService = perfilService;
        }

        #endregion


        public IEnumerable<Perfil> Get()
        {
            return _perfilService.GetAll().ToList();
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return _perfilService.GetId(id).Nome;
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}