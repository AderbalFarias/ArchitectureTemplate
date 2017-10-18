using ArchitectureTemplate.Domain.DataEntities;
using System.Data.Entity.ModelConfiguration;

namespace ArchitectureTemplate.Infrastructure.Data.EntityConfig.Mapping
{
    public class ProfileForMenuMap : EntityTypeConfiguration<ProfileForMenu>
    {
        public ProfileForMenuMap()
        {
            HasKey(k => k.Id);

            HasRequired(p => p.Profile)
                .WithMany()
                .HasForeignKey(f => f.ProfileId);

            HasRequired(p => p.Menu)
                .WithMany(w => w.ProfileForMenu)
                .HasForeignKey(f => f.MenuId)
                .WillCascadeOnDelete(true);
        }
    }
}
