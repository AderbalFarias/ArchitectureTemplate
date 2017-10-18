namespace ArchitectureTemplate.Infrastructure.Data.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class UpdateData : DbMigration
    {
        public override void Up()
        {
            Sql(@"DECLARE @ProfileId INTEGER 
                INSERT [dbo].[Profile](Nome, Ativo, DataCadastro) VALUES ('Administrator', 1, GETDATE())
                set @ProfileId = SCOPE_IDENTITY() 
                INSERT [dbo].[User](ProfileId, Nome, Cpf, Email, Telefone, Login, Senha, Ativo) 
                    VALUES (@ProfileId, 'Admin Test', 22815616319, 'test@test.com', 1232242412, 'Admin', 'T8BDdQokQd79jjXS4j6E8A==', 1)

                UPDATE [dbo].[Menu] set [Nome] = 'Settings' where [Id] = 1
                UPDATE [dbo].[Menu] set [Nome] = 'Menus Setting' where [Id] = 2
                UPDATE [dbo].[Menu] set [Nome] = 'Permission' where [Id] = 3
                UPDATE [dbo].[Menu] set [Nome] = 'Screen' where [Id] = 4

                INSERT [dbo].[ProfileForMenu] (MenuId, ProfileId) SELECT Id, @ProfileId FROM [dbo].[Menu]");
        }

        public override void Down()
        {
            Sql("DELETE [dbo].[User]");
            Sql("DELETE [dbo].[Profile]");
        }
    }
}
