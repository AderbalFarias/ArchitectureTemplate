using System.Data.Entity.ModelConfiguration;
using ArchitectureTemplate.Business.DataEntities;

namespace ArchitectureTemplate.Infrastructure.Data.EntityConfig.Mapping
{
    public class PerfilPorMenuMap : EntityTypeConfiguration<PerfilPorMenu>
    {
        public PerfilPorMenuMap()
        {
            HasKey(k => k.Id);

            HasRequired(p => p.Perfil)
                .WithMany()
                .HasForeignKey(f => f.PerfilId);

            HasRequired(p => p.Menu)
                .WithMany(w => w.PerfilPorMenu)
                .HasForeignKey(f => f.MenuId)
                .WillCascadeOnDelete(true);
        }
    }
}
