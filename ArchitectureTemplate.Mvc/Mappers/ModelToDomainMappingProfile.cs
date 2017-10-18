using ArchitectureTemplate.Domain.DataEntities;
using ArchitectureTemplate.Mvc.Models;

namespace ArchitectureTemplate.Mvc.Mappers
{
    public class ModelToDomainMappingProfile : AutoMapper.Profile
    {
        public override string ProfileName => "ModelToDomainMappings";

        protected override void Configure()
        {
            CreateMap<UserModel, User>();
            CreateMap<ProfileModel, Domain.DataEntities.Profile>();
            CreateMap<HierarchyModel, Hierarchy>();
            CreateMap<HierarchyDetalheModel, HierarchyDetail>();
            CreateMap<LogModel, Log>();
            CreateMap<ScreenModel, Screen>();
        }
    }
}