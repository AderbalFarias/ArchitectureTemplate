namespace ArchitectureTemplate.Infrastructure.Data.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Hierarchy",
                c => new
                {
                    Id = c.Long(nullable: false, identity: true),
                    HierarchyPaiId = c.Long(),
                    Nome = c.String(nullable: false, maxLength: 100, unicode: false),
                    Ativo = c.Boolean(nullable: false),
                    HierarchyTypeId = c.Int(),
                    Vertical = c.Boolean(nullable: false),
                    Descricao = c.String(maxLength: 500, unicode: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Hierarchy", t => t.HierarchyPaiId)
                .ForeignKey("dbo.HierarchyType", t => t.HierarchyTypeId)
                .Index(t => t.HierarchyPaiId)
                .Index(t => t.HierarchyTypeId);

            CreateTable(
                "dbo.HierarchyDetalhe",
                c => new
                {
                    HierarchyId = c.Long(nullable: false),
                    PessoaContato = c.String(maxLength: 70, unicode: false),
                    CpfCnpj = c.Long(),
                    Telefone = c.String(maxLength: 15, unicode: false),
                    Email = c.String(maxLength: 100, unicode: false),
                    Codigo = c.Int(),
                })
                .PrimaryKey(t => t.HierarchyId)
                .ForeignKey("dbo.Hierarchy", t => t.HierarchyId)
                .Index(t => t.HierarchyId);

            CreateTable(
                "dbo.HierarchyType",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Descricao = c.String(maxLength: 100, unicode: false),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.Log",
                c => new
                {
                    Id = c.Long(nullable: false, identity: true),
                    LogTypeId = c.Int(nullable: false),
                    ScreenId = c.Int(),
                    UserId = c.Long(),
                    Mensagem = c.String(nullable: false, maxLength: 1000, unicode: false),
                    NomeClasse = c.String(maxLength: 100, unicode: false),
                    Conteudo = c.String(nullable: false, unicode: false),
                    DataCadastro = c.DateTime(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.LogType", t => t.LogTypeId)
                .ForeignKey("dbo.Screen", t => t.ScreenId)
                .ForeignKey("dbo.User", t => t.UserId)
                .Index(t => t.LogTypeId)
                .Index(t => t.ScreenId)
                .Index(t => t.UserId);

            CreateTable(
                "dbo.LogType",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Descricao = c.String(nullable: false, maxLength: 30, unicode: false),
                })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Descricao, unique: true, name: "UQ_LogType_Description");

            CreateTable(
                "dbo.Screen",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Nome = c.String(nullable: false, maxLength: 50, unicode: false),
                    ControllerName = c.String(nullable: false, maxLength: 120, unicode: false),
                    DataCadastro = c.DateTime(nullable: false),
                    Ativo = c.Boolean(nullable: false),
                    Create = c.Boolean(nullable: false),
                    Read = c.Boolean(nullable: false),
                    Update = c.Boolean(nullable: false),
                    Delete = c.Boolean(nullable: false),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.PerfilPorScreen",
                c => new
                {
                    Id = c.Long(nullable: false, identity: true),
                    PerfilId = c.Int(nullable: false),
                    ScreenId = c.Int(nullable: false),
                    Create = c.Boolean(nullable: false),
                    Read = c.Boolean(nullable: false),
                    Update = c.Boolean(nullable: false),
                    Delete = c.Boolean(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Perfil", t => t.PerfilId)
                .ForeignKey("dbo.Screen", t => t.ScreenId, cascadeDelete: true)
                .Index(t => t.PerfilId)
                .Index(t => t.ScreenId);

            CreateTable(
                "dbo.Perfil",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Nome = c.String(nullable: false, maxLength: 50, unicode: false),
                    Ativo = c.Boolean(nullable: false),
                    Solicitante = c.String(maxLength: 100, unicode: false),
                    DataCadastro = c.DateTime(nullable: false),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.User",
                c => new
                {
                    Id = c.Long(nullable: false, identity: true),
                    HierarchyId = c.Long(),
                    PerfilId = c.Int(nullable: false),
                    Nome = c.String(nullable: false, maxLength: 80, unicode: false),
                    Cpf = c.Long(),
                    Email = c.String(nullable: false, maxLength: 100, unicode: false),
                    Telefone = c.String(maxLength: 15, unicode: false),
                    Login = c.String(nullable: false, maxLength: 40, unicode: false),
                    Senha = c.String(maxLength: 500, unicode: false),
                    Token = c.String(maxLength: 100, unicode: false),
                    DataExpiracaoSenha = c.DateTime(),
                    InvalidacaoProgramadaInicio = c.DateTime(),
                    InvalidacaoProgramadaFim = c.DateTime(),
                    Ativo = c.Boolean(nullable: false),
                    CodigoRecover = c.String(maxLength: 20, unicode: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Hierarchy", t => t.HierarchyId)
                .ForeignKey("dbo.Perfil", t => t.PerfilId)
                .Index(t => t.HierarchyId)
                .Index(t => t.PerfilId)
                .Index(t => t.Email, unique: true, name: "UQ_User_Email")
                .Index(t => t.Login, unique: true, name: "UQ_User_Login");

            CreateTable(
                "dbo.Menu",
                c => new
                {
                    Id = c.Int(nullable: false),
                    Nome = c.String(nullable: false, maxLength: 100, unicode: false),
                })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Nome, unique: true, name: "UQ_Menu_Nome");

            CreateTable(
                "dbo.PerfilPorMenu",
                c => new
                {
                    Id = c.Long(nullable: false, identity: true),
                    PerfilId = c.Int(nullable: false),
                    MenuId = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Menu", t => t.MenuId, cascadeDelete: true)
                .ForeignKey("dbo.Perfil", t => t.PerfilId)
                .Index(t => t.PerfilId)
                .Index(t => t.MenuId);

        }

        public override void Down()
        {
            DropForeignKey("dbo.PerfilPorMenu", "PerfilId", "dbo.Perfil");
            DropForeignKey("dbo.PerfilPorMenu", "MenuId", "dbo.Menu");
            DropForeignKey("dbo.Log", "UserId", "dbo.User");
            DropForeignKey("dbo.User", "PerfilId", "dbo.Perfil");
            DropForeignKey("dbo.User", "HierarchyId", "dbo.Hierarchy");
            DropForeignKey("dbo.Log", "ScreenId", "dbo.Screen");
            DropForeignKey("dbo.PerfilPorScreen", "ScreenId", "dbo.Screen");
            DropForeignKey("dbo.PerfilPorScreen", "PerfilId", "dbo.Perfil");
            DropForeignKey("dbo.Log", "LogTypeId", "dbo.LogType");
            DropForeignKey("dbo.Hierarchy", "HierarchyTypeId", "dbo.HierarchyType");
            DropForeignKey("dbo.Hierarchy", "HierarchyPaiId", "dbo.Hierarchy");
            DropForeignKey("dbo.HierarchyDetalhe", "HierarchyId", "dbo.Hierarchy");
            DropIndex("dbo.PerfilPorMenu", new[] { "MenuId" });
            DropIndex("dbo.PerfilPorMenu", new[] { "PerfilId" });
            DropIndex("dbo.Menu", "UQ_Menu_Nome");
            DropIndex("dbo.User", "UQ_User_Login");
            DropIndex("dbo.User", "UQ_User_Email");
            DropIndex("dbo.User", new[] { "PerfilId" });
            DropIndex("dbo.User", new[] { "HierarchyId" });
            DropIndex("dbo.PerfilPorScreen", new[] { "ScreenId" });
            DropIndex("dbo.PerfilPorScreen", new[] { "PerfilId" });
            DropIndex("dbo.LogType", "UQ_LogType_Description");
            DropIndex("dbo.Log", new[] { "UserId" });
            DropIndex("dbo.Log", new[] { "ScreenId" });
            DropIndex("dbo.Log", new[] { "LogTypeId" });
            DropIndex("dbo.HierarchyDetalhe", new[] { "HierarchyId" });
            DropIndex("dbo.Hierarchy", new[] { "HierarchyTypeId" });
            DropIndex("dbo.Hierarchy", new[] { "HierarchyPaiId" });
            DropTable("dbo.PerfilPorMenu");
            DropTable("dbo.Menu");
            DropTable("dbo.User");
            DropTable("dbo.Perfil");
            DropTable("dbo.PerfilPorScreen");
            DropTable("dbo.Screen");
            DropTable("dbo.LogType");
            DropTable("dbo.Log");
            DropTable("dbo.HierarchyType");
            DropTable("dbo.HierarchyDetalhe");
            DropTable("dbo.Hierarchy");
        }
    }
}
