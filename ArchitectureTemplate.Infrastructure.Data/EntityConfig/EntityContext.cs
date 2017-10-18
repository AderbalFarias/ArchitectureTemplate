using ArchitectureTemplate.Domain.DataEntities;
using ArchitectureTemplate.Infrastructure.Data.EntityConfig.Mapping;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace ArchitectureTemplate.Infrastructure.Data.EntityConfig
{
    public class EntityContext : DbContext
    {
        public EntityContext()
            : base("name=EntityContext")
        {
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
        }

        public virtual DbSet<Hierarchy> Hierarchy { get; set; }
        public virtual DbSet<HierarchyDetail> HierarchyDetalhe { get; set; }
        public virtual DbSet<Log> Log { get; set; }
        public virtual DbSet<LogType> LogType { get; set; }
        public virtual DbSet<Profile> Profile { get; set; }
        public virtual DbSet<ProfileForScreen> ProfileForScreen { get; set; }
        public virtual DbSet<Screen> Screen { get; set; }
        public virtual DbSet<HierarchyType> HierarchyType { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<Menu> Menu { get; set; }
        public virtual DbSet<ProfileForMenu> ProfileForMenu { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            //modelBuilder.Properties()
            //    .Where(p => p.Name == (p.ReflectedType != null ?
            //        p.ReflectedType.Name + "Id" : "Id"))
            //            .Configure(p => p.IsKey());

            modelBuilder.Properties<string>()
                .Configure(p => p.HasColumnType("varchar"));

            modelBuilder.Properties<decimal>()
                .Configure(p => p.HasPrecision(12, 2));

            modelBuilder.Properties<string>()
                .Configure(p => p.HasMaxLength(100));

            modelBuilder.Configurations.Add(new HierarchyMap());
            modelBuilder.Configurations.Add(new HierarchyDetailMap());
            modelBuilder.Configurations.Add(new LogMap());
            modelBuilder.Configurations.Add(new LogTypeMap());
            modelBuilder.Configurations.Add(new ProfileMap());
            modelBuilder.Configurations.Add(new ProfileForScreenMap());
            modelBuilder.Configurations.Add(new ScreenMap());
            modelBuilder.Configurations.Add(new UserMap());
            modelBuilder.Configurations.Add(new MenuMap());
            modelBuilder.Configurations.Add(new ProfileForMenuMap());
        }
    }
}






