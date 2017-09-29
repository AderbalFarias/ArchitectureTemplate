using System.Data.Entity.ModelConfiguration;
using ArchitectureTemplate.Business.DataEntities;

namespace ArchitectureTemplate.Infrastructure.Data.EntityConfig.Mapping
{
    public class TelaMap : EntityTypeConfiguration<Tela>
    {
        public TelaMap()
        {
            HasKey(k => k.Id);
            
            Property(p => p.Nome)
                .IsRequired()
                .HasMaxLength(50);
            
            Property(p => p.ControllerName)
                .IsRequired()
                .HasMaxLength(120);
        }
    }
}
