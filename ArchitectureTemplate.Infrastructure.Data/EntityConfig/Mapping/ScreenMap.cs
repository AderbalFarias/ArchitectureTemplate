using System.Data.Entity.ModelConfiguration;
using ArchitectureTemplate.Domain.DataEntities;

namespace ArchitectureTemplate.Infrastructure.Data.EntityConfig.Mapping
{
    public class ScreenMap : EntityTypeConfiguration<Screen>
    {
        public ScreenMap()
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
