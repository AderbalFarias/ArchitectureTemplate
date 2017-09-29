﻿using ArchitectureTemplate.Business.DataEntities;
using ArchitectureTemplate.Mvc.Models;
using AutoMapper;

namespace ArchitectureTemplate.Mvc.Mappers
{
    public class ModelToDomainMappingProfile : Profile
    {
        public override string ProfileName => "ModelToDomainMappings";

        protected override void Configure()
        {
            CreateMap<UsuarioModel, Usuario>();
            CreateMap<PerfilModel, Perfil>();
            CreateMap<HierarquiaModel, Hierarquia>();
            CreateMap<HierarquiaDetalheModel, HierarquiaDetalhe>();
            CreateMap<LogModel, Log>();
            CreateMap<TelaModel, Tela>();
        }
    }
}