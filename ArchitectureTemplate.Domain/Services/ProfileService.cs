using ArchitectureTemplate.Domain.DataEntities;
using ArchitectureTemplate.Domain.Interfaces.Repositories;
using ArchitectureTemplate.Domain.Interfaces.Services;
using ArchitectureTemplate.Infraestrutura.CrossCutting.Support.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ArchitectureTemplate.Domain.Services
{
    public class ProfileService : ServiceBase<Profile>, IProfileService
    {
        #region Fields

        private readonly IProfileRepository _profileRepository;

        #endregion

        #region Constructors

        public ProfileService(IProfileRepository profileRepository)
            : base(profileRepository)
        {
            _profileRepository = profileRepository;
        }

        #endregion

        #region Methods

        public IDictionary<int, string> GetDictionary()
        {
            return _profileRepository.GetDictionary();
        }

        public async Task<IDictionary<int, string>> GetDictionaryAsync()
        {
            return await _profileRepository.GetDictionaryAsync();

        }

        public IEnumerable<Profile> Get(Pagination paginar)
        {
            return _profileRepository.Get(paginar);
        }

        public async Task<IEnumerable<Profile>> GetAsync(Pagination paginar)
        {
            return await _profileRepository.GetAsync(paginar);
        }

        public void DisableOrEnable(long profileId, long userId)
        {
            var profile = _profileRepository.GetId(profileId);
            profile.Ativo = !profile.Ativo;

            _profileRepository.Update(profile, userId, true);
        }

        #endregion
    }
}