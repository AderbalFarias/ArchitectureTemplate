using ArchitectureTemplate.Business.DataEntities;
using ArchitectureTemplate.Mvc.Models;
using AutoMapper;

namespace ArchitectureTemplate.Mvc.Mappers
{
    public class DomainToModelMappingProfile : Profile
    {
        public override string ProfileName => "DomainToModelMappings";

        protected override void Configure()
        {
            CreateMap<Usuario, UsuarioModel>();
            CreateMap<Perfil, PerfilModel>();
            CreateMap<Hierarquia, HierarquiaModel>();
            CreateMap<HierarquiaDetalhe, HierarquiaDetalheModel>();
            CreateMap<Log, LogModel>();
            CreateMap<Tela, TelaModel>();
        }
    }
}