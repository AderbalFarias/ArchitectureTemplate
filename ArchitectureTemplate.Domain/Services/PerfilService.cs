using System.Collections.Generic;
using System.Threading.Tasks;
using ArchitectureTemplate.Domain.DataEntities;
using ArchitectureTemplate.Domain.Interfaces.Repositories;
using ArchitectureTemplate.Domain.Interfaces.Services;
using ArchitectureTemplate.Infraestrutura.CrossCutting.Support.Extensions;

namespace ArchitectureTemplate.Domain.Services
{
    public class ProfileService : ServiceBase<Profile>, IProfileService
    {
        #region Fields

        private readonly IProfileRepository _ProfileRepository;

        #endregion
        
        #region Constructors

        public ProfileService(IProfileRepository ProfileRepository)
            : base(ProfileRepository)
        {
            _ProfileRepository = ProfileRepository;
        }

        #endregion

        #region Methods

        public IDictionary<int, string> GetDictionary()
        {
            return _ProfileRepository.GetDictionary();
        }

        public async Task<IDictionary<int, string>> GetDictionaryAsync()
        {
            return await _ProfileRepository.GetDictionaryAsync();

        }

        public IEnumerable<Profile> Get(Pagination paginar)
        {
            return _ProfileRepository.Get(paginar);
        }

        public async Task<IEnumerable<Profile>> GetAsync(Pagination paginar)
        {
            return await _ProfileRepository.GetAsync(paginar);
        }

        public void DisableOrEnable(long ProfileId, long userId)
        {
            var Profile = _ProfileRepository.GetId(ProfileId);
            Profile.Ativo = !Profile.Ativo;

            _ProfileRepository.Update(Profile, userId, true);
        }

        #endregion
    }
}