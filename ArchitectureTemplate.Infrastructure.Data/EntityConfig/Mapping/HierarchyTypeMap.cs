using ArchitectureTemplate.Domain.DataEntities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace ArchitectureTemplate.Infrastructure.Data.EntityConfig.Mapping
{
    public class HierarchyTypeMap : EntityTypeConfiguration<HierarchyType>
    {
        public HierarchyTypeMap()
        {
            HasKey(k => k.Id);

            Property(p => p.Descricao)
                .IsRequired()
                .HasMaxLength(30)
                .HasColumnAnnotation(IndexAnnotation.AnnotationName, new IndexAnnotation(new IndexAttribute("UQ_HierarchyType_Description", 1) { IsUnique = true }));
        }
    }
}
