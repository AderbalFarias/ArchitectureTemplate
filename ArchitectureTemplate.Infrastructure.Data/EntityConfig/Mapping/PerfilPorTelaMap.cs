using System.Data.Entity.ModelConfiguration;
using ArchitectureTemplate.Domain.DataEntities;

namespace ArchitectureTemplate.Infrastructure.Data.EntityConfig.Mapping
{
    public class ProfilePorTelaMap : EntityTypeConfiguration<ProfilePorTela>
    {
        public ProfilePorTelaMap()
        {
            HasKey(k => k.Id);

            HasRequired(p => p.Profile)
                .WithMany(w => w.ProfilePorTela)
                .HasForeignKey(f => f.ProfileId);

            HasRequired(p => p.Tela)
                .WithMany(w => w.ProfilePorTela)
                .HasForeignKey(f => f.TelaId)
                .WillCascadeOnDelete(true);
        }
    }
}
