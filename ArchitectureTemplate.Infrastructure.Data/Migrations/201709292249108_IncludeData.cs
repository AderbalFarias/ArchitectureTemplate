namespace ArchitectureTemplate.Infrastructure.Data.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class IncludeData : DbMigration
    {
        public override void Up()
        {
            Sql(@"declare @PerfilId integer 
                insert into [dbo].[Perfil](Nome, Ativo, DataCadastro) values('Administrator', 1, GETDATE())
                set @PerfilId = SCOPE_IDENTITY() 
                insert into [dbo].[Usuario](PerfilId, Nome, Cpf, Email, Telefone, Login, Senha, Ativo) 
                    values(@PerfilId, 'Admin Test', 22815616319, 'test@test.com', 1232242412, 'Admin', 'T8BDdQokQd79jjXS4j6E8A==', 1)");

            Sql(@"SET IDENTITY_INSERT [dbo].[LogType] ON
                INSERT [dbo].[LogType] ([Id], [Descricao]) VALUES (1, N'Insert')
                INSERT [dbo].[LogType] ([Id], [Descricao]) VALUES (2, N'Update')
                INSERT [dbo].[LogType] ([Id], [Descricao]) VALUES (3, N'Delete')
                INSERT [dbo].[LogType] ([Id], [Descricao]) VALUES (4, N'Error')
                SET IDENTITY_INSERT [dbo].[LogType] OFF");
        }

        public override void Down()
        {
            Sql("DELETE [dbo].[Usuario]");
            Sql("DELETE [dbo].[Perfil]");
            Sql("DELETE [dbo].[Log]");
            Sql("DELETE [dbo].[LogType]");
        }
    }
}
