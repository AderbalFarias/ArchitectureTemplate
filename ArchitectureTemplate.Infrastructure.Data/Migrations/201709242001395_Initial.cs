namespace ArchitectureTemplate.Infrastructure.Data.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                    "dbo.Hierarquia",
                    c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        HierarquiaPaiId = c.Long(),
                        Nome = c.String(nullable: false, maxLength: 100, unicode: false),
                        Ativo = c.Boolean(nullable: false),
                        TipoHierarquiaId = c.Int(),
                        Vertical = c.Boolean(nullable: false),
                        Descricao = c.String(maxLength: 500, unicode: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Hierarquia", t => t.HierarquiaPaiId)
                .ForeignKey("dbo.TipoHierarquia", t => t.TipoHierarquiaId)
                .Index(t => t.HierarquiaPaiId)
                .Index(t => t.TipoHierarquiaId);

            CreateTable(
                    "dbo.HierarquiaDetalhe",
                    c => new
                    {
                        HierarquiaId = c.Long(nullable: false),
                        PessoaContato = c.String(maxLength: 70, unicode: false),
                        CpfCnpj = c.Long(),
                        Telefone = c.String(maxLength: 15, unicode: false),
                        Email = c.String(maxLength: 100, unicode: false),
                        Codigo = c.Int(),
                    })
                .PrimaryKey(t => t.HierarquiaId)
                .ForeignKey("dbo.Hierarquia", t => t.HierarquiaId)
                .Index(t => t.HierarquiaId);

            CreateTable(
                    "dbo.TipoHierarquia",
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
                        TelaId = c.Int(),
                        UsuarioId = c.Long(),
                        Mensagem = c.String(nullable: false, maxLength: 1000, unicode: false),
                        NomeClasse = c.String(maxLength: 100, unicode: false),
                        Conteudo = c.String(nullable: false, unicode: false),
                        DataCadastro = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.LogType", t => t.LogTypeId)
                .ForeignKey("dbo.Tela", t => t.TelaId)
                .ForeignKey("dbo.Usuario", t => t.UsuarioId)
                .Index(t => t.LogTypeId)
                .Index(t => t.TelaId)
                .Index(t => t.UsuarioId);

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
                    "dbo.Tela",
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
                    "dbo.PerfilPorTela",
                    c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        PerfilId = c.Int(nullable: false),
                        TelaId = c.Int(nullable: false),
                        Create = c.Boolean(nullable: false),
                        Read = c.Boolean(nullable: false),
                        Update = c.Boolean(nullable: false),
                        Delete = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Perfil", t => t.PerfilId)
                .ForeignKey("dbo.Tela", t => t.TelaId, cascadeDelete: true)
                .Index(t => t.PerfilId)
                .Index(t => t.TelaId);

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
                    "dbo.Usuario",
                    c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        HierarquiaId = c.Long(),
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
                .ForeignKey("dbo.Hierarquia", t => t.HierarquiaId)
                .ForeignKey("dbo.Perfil", t => t.PerfilId)
                .Index(t => t.HierarquiaId)
                .Index(t => t.PerfilId)
                .Index(t => t.Email, unique: true, name: "UQ_Usuario_Email")
                .Index(t => t.Login, unique: true, name: "UQ_Usuario_Login");

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
            DropForeignKey("dbo.Log", "UsuarioId", "dbo.Usuario");
            DropForeignKey("dbo.Usuario", "PerfilId", "dbo.Perfil");
            DropForeignKey("dbo.Usuario", "HierarquiaId", "dbo.Hierarquia");
            DropForeignKey("dbo.Log", "TelaId", "dbo.Tela");
            DropForeignKey("dbo.PerfilPorTela", "TelaId", "dbo.Tela");
            DropForeignKey("dbo.PerfilPorTela", "PerfilId", "dbo.Perfil");
            DropForeignKey("dbo.Log", "LogTypeId", "dbo.LogType");
            DropForeignKey("dbo.Hierarquia", "TipoHierarquiaId", "dbo.TipoHierarquia");
            DropForeignKey("dbo.Hierarquia", "HierarquiaPaiId", "dbo.Hierarquia");
            DropForeignKey("dbo.HierarquiaDetalhe", "HierarquiaId", "dbo.Hierarquia");
            DropIndex("dbo.PerfilPorMenu", new[] { "MenuId" });
            DropIndex("dbo.PerfilPorMenu", new[] { "PerfilId" });
            DropIndex("dbo.Menu", "UQ_Menu_Nome");
            DropIndex("dbo.Usuario", "UQ_Usuario_Login");
            DropIndex("dbo.Usuario", "UQ_Usuario_Email");
            DropIndex("dbo.Usuario", new[] { "PerfilId" });
            DropIndex("dbo.Usuario", new[] { "HierarquiaId" });
            DropIndex("dbo.PerfilPorTela", new[] { "TelaId" });
            DropIndex("dbo.PerfilPorTela", new[] { "PerfilId" });
            DropIndex("dbo.LogType", "UQ_LogType_Description");
            DropIndex("dbo.Log", new[] { "UsuarioId" });
            DropIndex("dbo.Log", new[] { "TelaId" });
            DropIndex("dbo.Log", new[] { "LogTypeId" });
            DropIndex("dbo.HierarquiaDetalhe", new[] { "HierarquiaId" });
            DropIndex("dbo.Hierarquia", new[] { "TipoHierarquiaId" });
            DropIndex("dbo.Hierarquia", new[] { "HierarquiaPaiId" });
            DropTable("dbo.PerfilPorMenu");
            DropTable("dbo.Menu");
            DropTable("dbo.Usuario");
            DropTable("dbo.Perfil");
            DropTable("dbo.PerfilPorTela");
            DropTable("dbo.Tela");
            DropTable("dbo.LogType");
            DropTable("dbo.Log");
            DropTable("dbo.TipoHierarquia");
            DropTable("dbo.HierarquiaDetalhe");
            DropTable("dbo.Hierarquia");
        }
    }
}
