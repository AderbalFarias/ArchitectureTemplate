using System.Data.Entity.ModelConfiguration;
using ArchitectureTemplate.Business.DataEntities;

namespace ArchitectureTemplate.Infrastructure.Data.EntityConfig.Mapping
{
    public class HierarquiaMap : EntityTypeConfiguration<Hierarquia>
    {
        public HierarquiaMap()
        {
            HasKey(k => k.Id);

            Property(p => p.Nome)
                .IsRequired()
                .HasMaxLength(100);

            Property(p => p.Descricao)
                .HasMaxLength(500);
            
            HasOptional(p => p.HierarquiaUp)
                .WithMany(p => p.HierarquiaDown)
                .HasForeignKey(f => f.HierarquiaPaiId);

            HasOptional(p => p.TipoHierarquia)
                .WithMany()
                .HasForeignKey(f => f.TipoHierarquiaId);
        }
    }
}
