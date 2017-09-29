using System.Data.Entity.Migrations;
using ArchitectureTemplate.Infrastructure.Data.EntityConfig;

namespace ArchitectureTemplate.Infrastructure.Data.Migrations
{
    /// <summary>
    /// Observação: No package manager console escolha o projeto de acesso a dados ArchitectureTemplate.Infrastructure.Data
    /// 
    /// Comandos:
    /// PM> Add-Migration = Gera uma migração a partir do código escrito nas classes e mappins
    /// PM> Update-Database -Verbose = Atualiza a base de dados com a última última versão de migration gerada
    /// PM> Update-Database -TargetMigration:NomeVersaoRetorno -Verbose = Retorna a base de dados para uma versão específica
    /// </summary>

    internal sealed class Configuration : DbMigrationsConfiguration<EntityContext>
    { 
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "ArchitectureTemplate.Infrastructure.Data.EntityConfig.EntityContext";
        }

        protected override void Seed(EntityContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}
