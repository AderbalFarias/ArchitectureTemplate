using System.Data.Entity.ModelConfiguration;
using ArchitectureTemplate.Domain.DataEntities;

namespace ArchitectureTemplate.Infrastructure.Data.EntityConfig.Mapping
{
    public class PerfilMap : EntityTypeConfiguration<Perfil>
    {
        public PerfilMap()
        {
            HasKey(k => k.Id);

            Property(p => p.Nome)
                .IsRequired()
                .HasMaxLength(50);
        }
    }
}
