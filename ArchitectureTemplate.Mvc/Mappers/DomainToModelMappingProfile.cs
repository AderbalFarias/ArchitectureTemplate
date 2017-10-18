using ArchitectureTemplate.Domain.DataEntities;
using ArchitectureTemplate.Mvc.Models;

namespace ArchitectureTemplate.Mvc.Mappers
{
    public class DomainToModelMappingProfile : AutoMapper.Profile
    {
        public override string ProfileName => "DomainToModelMappings";

        protected override void Configure()
        {
            CreateMap<User, UserModel>();
            CreateMap<Domain.DataEntities.Profile, ProfileModel>();
            CreateMap<Hierarchy, HierarchyModel>();
            CreateMap<HierarchyDetail, HierarchyDetalheModel>();
            CreateMap<Log, LogModel>();
            CreateMap<Screen, ScreenModel>();
        }
    }
}