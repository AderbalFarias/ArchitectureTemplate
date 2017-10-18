using System.Data.Entity.ModelConfiguration;
using ArchitectureTemplate.Domain.DataEntities;

namespace ArchitectureTemplate.Infrastructure.Data.EntityConfig.Mapping
{
    public class ProfilePorMenuMap : EntityTypeConfiguration<ProfilePorMenu>
    {
        public ProfilePorMenuMap()
        {
            HasKey(k => k.Id);

            HasRequired(p => p.Profile)
                .WithMany()
                .HasForeignKey(f => f.ProfileId);

            HasRequired(p => p.Menu)
                .WithMany(w => w.ProfilePorMenu)
                .HasForeignKey(f => f.MenuId)
                .WillCascadeOnDelete(true);
        }
    }
}
