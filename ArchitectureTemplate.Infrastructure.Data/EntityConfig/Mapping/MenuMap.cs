using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using ArchitectureTemplate.Domain.DataEntities;

namespace ArchitectureTemplate.Infrastructure.Data.EntityConfig.Mapping
{
    public class MenuMap : EntityTypeConfiguration<Menu>
    {
        public MenuMap()
        {
            HasKey(k => k.Id);
            
            Property(p => p.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            Property(p => p.Nome)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnAnnotation(IndexAnnotation.AnnotationName, new IndexAnnotation(new IndexAttribute("UQ_Menu_Nome", 1) { IsUnique = true }));
        }
    }
}
