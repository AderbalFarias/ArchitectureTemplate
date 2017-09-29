using ArchitectureTemplate.Business.DataEntities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ArchitectureTemplate.Infrastructure.Data.EntityConfig.Mapping
{
    public class HierarquiaDetalhesMap : EntityTypeConfiguration<HierarquiaDetalhe>
    {
        public HierarquiaDetalhesMap()
        {
            // Primary Key
            HasKey(k => k.HierarquiaId);

            // Properties
            Property(p => p.HierarquiaId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            Ignore(i => i.PessoaFisica);

            Property(p => p.PessoaContato)
                .HasMaxLength(70);

            Property(p => p.Telefone)
                .HasMaxLength(15);

            Property(p => p.Email)
                .HasMaxLength(100);

            // Relationships
            HasRequired(p => p.Hierarquia)
                .WithOptional(p => p.HierarquiaDetalhe);
        }
    }
}
