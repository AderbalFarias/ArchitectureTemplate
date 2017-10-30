using ArchitectureTemplate.Domain.Interfaces.Services;
using ArchitectureTemplate.Infraestrutura.CrossCutting.Support.Extensions;
using ArchitectureTemplate.Infrastructure.WCF.Contracts.Entities;
using ArchitectureTemplate.Infrastructure.WCF.Contracts.ServiceInterfaces;
using System;
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
        private const string Log = "C:\\Logs\\ArchitectureTemplate\\Infrastructure\\WCF\\Services\\";

        public ProfileManager(IProfileService profileService)
        {
            _profileService = profileService;
        }

        public ProfileContract GetById(int id)
        {
            try
            {
                var profile = _profileService.GetId(id);
                return profile.Cast<ProfileContract>();
            }
            catch (Exception e)
            {
                LogFile.Create(e, Log);
                throw new Exception("Erro on the method GetById: " + e.Message);
            }
        }

        public ProfileContract GetByName(string name)
        {
            try
            {
                var profile = _profileService.Get(t => t.Nome == name);
                return profile.Cast<ProfileContract>();
            }
            catch (Exception e)
            {
                LogFile.Create(e, Log);
                throw new Exception("Erro on the method GetByName: " + e.Message);
            }
        }

        public IEnumerable<ProfileContract> GetAll()
        {
            try
            {
                var profiles = _profileService.GetAll()
                    .ToList();

                return profiles.CastAll<ProfileContract>();
            }
            catch (Exception e)
            {
                LogFile.Create(e, Log);
                throw new Exception("Erro on the method GetAll: " + e.Message);
            }

        }
    }
}
