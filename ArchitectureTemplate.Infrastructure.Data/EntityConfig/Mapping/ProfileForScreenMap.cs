using ArchitectureTemplate.Domain.DataEntities;
using System.Data.Entity.ModelConfiguration;

namespace ArchitectureTemplate.Infrastructure.Data.EntityConfig.Mapping
{
    public class ProfileForScreenMap : EntityTypeConfiguration<ProfileForScreen>
    {
        public ProfileForScreenMap()
        {
            HasKey(k => k.Id);

            HasRequired(p => p.Profile)
                .WithMany(w => w.ProfileForScreen)
                .HasForeignKey(f => f.ProfileId);

            HasRequired(p => p.Screen)
                .WithMany(w => w.ProfileForScreen)
                .HasForeignKey(f => f.ScreenId)
                .WillCascadeOnDelete(true);
        }
    }
}
