using System.Data.Entity.ModelConfiguration;
using ArchitectureTemplate.Domain.DataEntities;

namespace ArchitectureTemplate.Infrastructure.Data.EntityConfig.Mapping
{
    public class ProfileMap : EntityTypeConfiguration<Profile>
    {
        public ProfileMap()
        {
            HasKey(k => k.Id);

            Property(p => p.Nome)
                .IsRequired()
                .HasMaxLength(50);
        }
    }
}
