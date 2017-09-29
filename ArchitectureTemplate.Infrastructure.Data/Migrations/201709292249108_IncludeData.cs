namespace ArchitectureTemplate.Infrastructure.Data.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class IncludeData : DbMigration
    {
        public override void Up()
        {
            Sql(@"declare @PerfilId integer 
                insert into Perfil(Nome, Ativo, DataCadastro) values('Administrator', 1, GETDATE())
                set @PerfilId = SCOPE_IDENTITY() 
                insert into Usuario(PerfilId, Nome, Cpf, Email, Telefone, Login, Senha, Ativo) 
                    values(@PerfilId, 'Admin Test', 22815616319, 'test@test.com', 1232242412, 'Admin', 'T8BDdQokQd79jjXS4j6E8A==', 1)");
        }

        public override void Down()
        {
        }
    }
}
