using ArchitectureTemplate.Infrastructure.Data.EntityConfig.Mapping;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using ArchitectureTemplate.Domain.DataEntities;

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

        public virtual DbSet<Hierarquia> Hierarquia { get; set; }
        public virtual DbSet<HierarquiaDetalhe> HierarquiaDetalhe { get; set; }
        public virtual DbSet<Log> Log { get; set; }
        public virtual DbSet<LogType> LogType { get; set; }
        public virtual DbSet<Profile> Profile { get; set; }
        public virtual DbSet<ProfilePorTela> ProfilePorTela { get; set; }
        public virtual DbSet<Tela> Tela { get; set; }
        public virtual DbSet<TipoHierarquia> TipoHierarquia { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }
        public virtual DbSet<Menu> Menu { get; set; }
        public virtual DbSet<ProfilePorMenu> ProfilePorMenu { get; set; }

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

            modelBuilder.Configurations.Add(new HierarquiaMap());
            modelBuilder.Configurations.Add(new HierarquiaDetalhesMap());
            modelBuilder.Configurations.Add(new LogMap());
            modelBuilder.Configurations.Add(new LogTypeMap());
            modelBuilder.Configurations.Add(new ProfileMap());
            modelBuilder.Configurations.Add(new ProfilePorTelaMap());
            modelBuilder.Configurations.Add(new TelaMap());
            modelBuilder.Configurations.Add(new UsuarioMap());
            modelBuilder.Configurations.Add(new MenuMap());
            modelBuilder.Configurations.Add(new ProfilePorMenuMap());
        }
    }
}






