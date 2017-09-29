using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using ArchitectureTemplate.Business.DataEntities;

namespace ArchitectureTemplate.Infrastructure.Data.EntityConfig.Mapping
{
    public class LogTypeMap : EntityTypeConfiguration<LogType>
    {
        public LogTypeMap()
        {
            HasKey(k => k.Id);
            
            Property(p => p.Descricao)
                .IsRequired()
                .HasMaxLength(30)
                .HasColumnAnnotation(IndexAnnotation.AnnotationName, new IndexAnnotation(new IndexAttribute("UQ_LogType_Description", 1) { IsUnique = true }));
        }
    }
}
