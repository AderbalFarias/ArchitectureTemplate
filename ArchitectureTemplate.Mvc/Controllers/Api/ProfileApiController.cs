using ArchitectureTemplate.Domain.DataEntities;
using ArchitectureTemplate.Domain.Interfaces.Services;
using ArchitectureTemplate.Mvc.Controllers.Shared;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace ArchitectureTemplate.Mvc.Controllers.Api
{
    [Authorize]
    public class ProfileApiController : ApiController
    {
        #region Fields

        private readonly IProfileService _ProfileService;

        #endregion

        #region Constructors

        public ProfileApiController(IProfileService ProfileService)
        {
            _ProfileService = ProfileService;
        }

        #endregion

        [IsAuthorize]
        [ActionType(AccessType.Read)]
        public IEnumerable<Profile> Get()
        {
            return _ProfileService.GetAll().ToList();
        }

        [IsAuthorize]
        [ActionType(AccessType.Read)]
        // GET api/<controller>/5
        public string Get(int id)
        {
            return _ProfileService.GetId(id).Nome;
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