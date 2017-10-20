using ArchitectureTemplate.Domain.Interfaces.Services;
using ArchitectureTemplate.Infraestrutura.CrossCutting.Support.Extensions;
using ArchitectureTemplate.Infrastructure.WCF.Contracts.Entities;
using ArchitectureTemplate.Infrastructure.WCF.Contracts.ServiceInterfaces;
using System.Collections.Generic;
using System.Linq;

namespace ArchitectureTemplate.Infrastructure.WCF.Services
{
    //[ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    //[ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    //[ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession)]
    //[ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple)]
    //[ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Single)]
    //[ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple)]
    //[ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class ProfileManager : IProfileServiceContract
    {
        private readonly IProfileService _profileService;

        public ProfileManager(IProfileService profileService)
        {
            _profileService = profileService;
        }

        public ProfileContract GetById(int id)
        {
            var profile = _profileService.GetId(id);
            return profile.Cast<ProfileContract>();
        }

        public ProfileContract GetByName(string name)
        {
            var profile = _profileService.Get(t => t.Nome == name);
            return profile.Cast<ProfileContract>();
        }

        public IEnumerable<ProfileContract> GetAll()
        {
            var profiles = _profileService.GetAll()
                .ToList();

            return profiles.CastAll<ProfileContract>();
        }
    }
}
