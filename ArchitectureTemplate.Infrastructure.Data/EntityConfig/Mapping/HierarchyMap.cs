using System.Data.Entity.ModelConfiguration;
using ArchitectureTemplate.Domain.DataEntities;

namespace ArchitectureTemplate.Infrastructure.Data.EntityConfig.Mapping
{
    public class HierarchyMap : EntityTypeConfiguration<Hierarchy>
    {
        public HierarchyMap()
        {
            HasKey(k => k.Id);

            Property(p => p.Nome)
                .IsRequired()
                .HasMaxLength(100);

            Property(p => p.Descricao)
                .HasMaxLength(500);
            
            HasOptional(p => p.HierarchyUp)
                .WithMany(p => p.HierarchyDown)
                .HasForeignKey(f => f.HierarchyPaiId);

            HasOptional(p => p.HierarchyType)
                .WithMany()
                .HasForeignKey(f => f.HierarchyTypeId);
        }
    }
}
