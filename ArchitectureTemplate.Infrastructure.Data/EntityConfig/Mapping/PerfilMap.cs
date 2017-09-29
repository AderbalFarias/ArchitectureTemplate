using ArchitectureTemplate.Business.DataEntities;
using System.Data.Entity.ModelConfiguration;

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
