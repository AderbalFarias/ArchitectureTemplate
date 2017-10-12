using System.Data.Entity.ModelConfiguration;
using ArchitectureTemplate.Domain.DataEntities;

namespace ArchitectureTemplate.Infrastructure.Data.EntityConfig.Mapping
{
    public class PerfilPorTelaMap : EntityTypeConfiguration<PerfilPorTela>
    {
        public PerfilPorTelaMap()
        {
            HasKey(k => k.Id);

            HasRequired(p => p.Perfil)
                .WithMany(w => w.PerfilPorTela)
                .HasForeignKey(f => f.PerfilId);

            HasRequired(p => p.Tela)
                .WithMany(w => w.PerfilPorTela)
                .HasForeignKey(f => f.TelaId)
                .WillCascadeOnDelete(true);
        }
    }
}
