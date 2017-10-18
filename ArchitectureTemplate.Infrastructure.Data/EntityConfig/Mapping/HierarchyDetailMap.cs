using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using ArchitectureTemplate.Domain.DataEntities;

namespace ArchitectureTemplate.Infrastructure.Data.EntityConfig.Mapping
{
    public class HierarchyDetailMap : EntityTypeConfiguration<HierarchyDetail>
    {
        public HierarchyDetailMap()
        {
            // Primary Key
            HasKey(k => k.HierarchyId);

            // Properties
            Property(p => p.HierarchyId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            Ignore(i => i.PessoaFisica);

            Property(p => p.PessoaContato)
                .HasMaxLength(70);

            Property(p => p.Telefone)
                .HasMaxLength(15);

            Property(p => p.Email)
                .HasMaxLength(100);

            // Relationships
            HasRequired(p => p.Hierarchy)
                .WithOptional(p => p.HierarchyDetalhe);
        }
    }
}
