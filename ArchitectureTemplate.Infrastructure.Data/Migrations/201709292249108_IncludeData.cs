namespace ArchitectureTemplate.Infrastructure.Data.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class IncludeData : DbMigration
    {
        public override void Up()
        {
            Sql(@"DECLARE @PerfilId INTEGER 
                INSERT [dbo].[Perfil](Nome, Ativo, DataCadastro) VALUES ('Administrator', 1, GETDATE())
                set @PerfilId = SCOPE_IDENTITY() 
                INSERT [dbo].[Usuario](PerfilId, Nome, Cpf, Email, Telefone, Login, Senha, Ativo) 
                    VALUES (@PerfilId, 'Admin Test', 22815616319, 'test@test.com', 1232242412, 'Admin', 'T8BDdQokQd79jjXS4j6E8A==', 1)

                SET IDENTITY_INSERT [dbo].[LogType] ON
                INSERT [dbo].[LogType] ([Id], [Descricao]) VALUES (1, N'Insert')
                INSERT [dbo].[LogType] ([Id], [Descricao]) VALUES (2, N'Update')
                INSERT [dbo].[LogType] ([Id], [Descricao]) VALUES (3, N'Delete')
                INSERT [dbo].[LogType] ([Id], [Descricao]) VALUES (4, N'Error')
                SET IDENTITY_INSERT [dbo].[LogType] OFF

                INSERT [dbo].[Menu] ([Id], [Nome]) VALUES (1, N'Configurações')
                INSERT [dbo].[Menu] ([Id], [Nome]) VALUES (2, N'Parametrização de Menus')
                INSERT [dbo].[Menu] ([Id], [Nome]) VALUES (3, N'Permissões')
                INSERT [dbo].[Menu] ([Id], [Nome]) VALUES (4, N'Telas')

                INSERT [dbo].[PerfilPorMenu] (MenuId, PerfilId) SELECT Id, @PerfilId FROM [dbo].[Menu]");
        }

        public override void Down()
        {
            Sql("DELETE [dbo].[Log]");
            Sql("DELETE [dbo].[LogType]");
            Sql("DELETE [dbo].[Usuario]");
            Sql("DELETE [dbo].[Perfil]");
            Sql("DELETE [dbo].[PerfilPorMenu]");
            Sql("DELETE [dbo].[Menu]");
        }
    }
}
