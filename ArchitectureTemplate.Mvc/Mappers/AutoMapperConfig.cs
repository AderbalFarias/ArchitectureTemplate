using AutoMapper;

namespace ArchitectureTemplate.Mvc.Mappers
{
    public class AutoMapperConfig
    {
        public static void RegisterMappings()
        {
            Mapper.Initialize(m =>
            {
                m.AddProfile<DomainToModelMappingProfile>();
                m.AddProfile<ModelToDomainMappingProfile>();
            });
        }
    }
}