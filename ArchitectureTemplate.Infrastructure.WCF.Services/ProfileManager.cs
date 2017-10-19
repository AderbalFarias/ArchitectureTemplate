using ArchitectureTemplate.Domain.Interfaces.Services;
using ArchitectureTemplate.Infraestrutura.CrossCutting.Support.Extensions;
using ArchitectureTemplate.Infrastructure.WCF.Contracts.Entities;
using ArchitectureTemplate.Infrastructure.WCF.Contracts.ServiceInterfaces;
using System.Collections.Generic;
using System.Linq;

namespace ArchitectureTemplate.Infrastructure.WCF.Services
{
    public class ProfileManager : IProfileServiceContract
    {
        private readonly IProfileService _profileService;

        public ProfileManager(IProfileService profileService)
        {
            _profileService = profileService;
        }

        public ProfileContract GetById(int id)
        {
            var screen = _profileService.GetId(id);
            return screen.Cast<ProfileContract>();
        }

        public ProfileContract GetByName(string name)
        {
            var screen = _profileService.Get(t => t.Nome == name);
            return screen.Cast<ProfileContract>();
        }

        public IEnumerable<ProfileContract> GetAll()
        {
            var screenList = _profileService.GetAll()
                .ToList();

            return screenList.CastAll<ProfileContract>();
        }
    }
}
