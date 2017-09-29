using System.Data.Entity.Migrations;
using ArchitectureTemplate.Infrastructure.Data.EntityConfig;

namespace ArchitectureTemplate.Infrastructure.Data.Migrations
{
    /// <summary>
    /// Observa��o: No package manager console escolha o projeto de acesso a dados ArchitectureTemplate.Infrastructure.Data
    /// 
    /// Comandos:
    /// PM> Add-Migration = Gera uma migra��o a partir do c�digo escrito nas classes e mappins
    /// PM> Update-Database -Verbose = Atualiza a base de dados com a �ltima �ltima vers�o de migration gerada
    /// PM> Update-Database -TargetMigration:NomeVersaoRetorno -Verbose = Retorna a base de dados para uma vers�o espec�fica
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
