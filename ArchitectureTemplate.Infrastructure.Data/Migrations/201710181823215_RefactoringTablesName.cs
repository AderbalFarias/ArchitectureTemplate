namespace ArchitectureTemplate.Infrastructure.Data.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class RefactoringTablesName : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.TipoHierarquia", newName: "HierarchyType");
            RenameTable(name: "dbo.Tela", newName: "Screen");
            RenameTable(name: "dbo.Perfil", newName: "Profile");
            DropForeignKey("dbo.HierarquiaDetalhe", "HierarquiaId", "dbo.Hierarquia");
            DropForeignKey("dbo.Hierarquia", "HierarquiaPaiId", "dbo.Hierarquia");
            DropForeignKey("dbo.Hierarquia", "TipoHierarquiaId", "dbo.TipoHierarquia");
            DropForeignKey("dbo.PerfilPorTela", "PerfilId", "dbo.Perfil");
            DropForeignKey("dbo.PerfilPorTela", "TelaId", "dbo.Tela");
            DropForeignKey("dbo.Usuario", "HierarquiaId", "dbo.Hierarquia");
            DropForeignKey("dbo.Usuario", "PerfilId", "dbo.Perfil");
            DropForeignKey("dbo.Log", "UsuarioId", "dbo.Usuario");
            DropForeignKey("dbo.PerfilPorMenu", "MenuId", "dbo.Menu");
            DropForeignKey("dbo.PerfilPorMenu", "PerfilId", "dbo.Perfil");
            DropIndex("dbo.Hierarquia", new[] { "HierarquiaPaiId" });
            DropIndex("dbo.Hierarquia", new[] { "TipoHierarquiaId" });
            DropIndex("dbo.HierarquiaDetalhe", new[] { "HierarquiaId" });
            DropIndex("dbo.Log", new[] { "UsuarioId" });
            DropIndex("dbo.PerfilPorTela", new[] { "PerfilId" });
            DropIndex("dbo.PerfilPorTela", new[] { "TelaId" });
            DropIndex("dbo.Usuario", new[] { "HierarquiaId" });
            DropIndex("dbo.Usuario", new[] { "PerfilId" });
            DropIndex("dbo.Usuario", "UQ_Usuario_Email");
            DropIndex("dbo.Usuario", "UQ_Usuario_Login");
            DropIndex("dbo.PerfilPorMenu", new[] { "PerfilId" });
            DropIndex("dbo.PerfilPorMenu", new[] { "MenuId" });
            RenameColumn(table: "dbo.Log", name: "TelaId", newName: "ScreenId");
            RenameIndex(table: "dbo.Log", name: "IX_TelaId", newName: "IX_ScreenId");
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
                .ForeignKey("dbo.HierarchyType", t => t.HierarchyTypeId)
                .ForeignKey("dbo.Hierarchy", t => t.HierarchyPaiId)
                .Index(t => t.HierarchyPaiId)
                .Index(t => t.HierarchyTypeId);

            CreateTable(
                "dbo.HierarchyDetail",
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
                "dbo.ProfileForScreen",
                c => new
                {
                    Id = c.Long(nullable: false, identity: true),
                    ProfileId = c.Int(nullable: false),
                    ScreenId = c.Int(nullable: false),
                    Create = c.Boolean(nullable: false),
                    Read = c.Boolean(nullable: false),
                    Update = c.Boolean(nullable: false),
                    Delete = c.Boolean(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Profile", t => t.ProfileId)
                .ForeignKey("dbo.Screen", t => t.ScreenId, cascadeDelete: true)
                .Index(t => t.ProfileId)
                .Index(t => t.ScreenId);

            CreateTable(
                "dbo.User",
                c => new
                {
                    Id = c.Long(nullable: false, identity: true),
                    HierarchyId = c.Long(),
                    ProfileId = c.Int(nullable: false),
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
                .ForeignKey("dbo.Profile", t => t.ProfileId)
                .Index(t => t.HierarchyId)
                .Index(t => t.ProfileId)
                .Index(t => t.Email, unique: true, name: "UQ_User_Email")
                .Index(t => t.Login, unique: true, name: "UQ_User_Login");

            CreateTable(
                "dbo.ProfileForMenu",
                c => new
                {
                    Id = c.Long(nullable: false, identity: true),
                    ProfileId = c.Int(nullable: false),
                    MenuId = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Menu", t => t.MenuId, cascadeDelete: true)
                .ForeignKey("dbo.Profile", t => t.ProfileId)
                .Index(t => t.ProfileId)
                .Index(t => t.MenuId);

            AddColumn("dbo.Log", "UserId", c => c.Long());
            CreateIndex("dbo.Log", "UserId");
            AddForeignKey("dbo.Log", "UserId", "dbo.User", "Id");
            DropColumn("dbo.Log", "UsuarioId");
            DropTable("dbo.Hierarquia");
            DropTable("dbo.HierarquiaDetalhe");
            DropTable("dbo.PerfilPorTela");
            DropTable("dbo.Usuario");
            DropTable("dbo.PerfilPorMenu");
        }

        public override void Down()
        {
            CreateTable(
                "dbo.PerfilPorMenu",
                c => new
                {
                    Id = c.Long(nullable: false, identity: true),
                    PerfilId = c.Int(nullable: false),
                    MenuId = c.Int(nullable: false),
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
                .PrimaryKey(t => t.Id);

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
                .PrimaryKey(t => t.HierarquiaId);

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
                .PrimaryKey(t => t.Id);

            AddColumn("dbo.Log", "UsuarioId", c => c.Long());
            DropForeignKey("dbo.ProfileForMenu", "ProfileId", "dbo.Profile");
            DropForeignKey("dbo.ProfileForMenu", "MenuId", "dbo.Menu");
            DropForeignKey("dbo.Log", "UserId", "dbo.User");
            DropForeignKey("dbo.User", "ProfileId", "dbo.Profile");
            DropForeignKey("dbo.User", "HierarchyId", "dbo.Hierarchy");
            DropForeignKey("dbo.ProfileForScreen", "ScreenId", "dbo.Screen");
            DropForeignKey("dbo.ProfileForScreen", "ProfileId", "dbo.Profile");
            DropForeignKey("dbo.Hierarchy", "HierarchyPaiId", "dbo.Hierarchy");
            DropForeignKey("dbo.Hierarchy", "HierarchyTypeId", "dbo.HierarchyType");
            DropForeignKey("dbo.HierarchyDetail", "HierarchyId", "dbo.Hierarchy");
            DropIndex("dbo.ProfileForMenu", new[] { "MenuId" });
            DropIndex("dbo.ProfileForMenu", new[] { "ProfileId" });
            DropIndex("dbo.User", "UQ_User_Login");
            DropIndex("dbo.User", "UQ_User_Email");
            DropIndex("dbo.User", new[] { "ProfileId" });
            DropIndex("dbo.User", new[] { "HierarchyId" });
            DropIndex("dbo.ProfileForScreen", new[] { "ScreenId" });
            DropIndex("dbo.ProfileForScreen", new[] { "ProfileId" });
            DropIndex("dbo.Log", new[] { "UserId" });
            DropIndex("dbo.HierarchyDetail", new[] { "HierarchyId" });
            DropIndex("dbo.Hierarchy", new[] { "HierarchyTypeId" });
            DropIndex("dbo.Hierarchy", new[] { "HierarchyPaiId" });
            DropColumn("dbo.Log", "UserId");
            DropTable("dbo.ProfileForMenu");
            DropTable("dbo.User");
            DropTable("dbo.ProfileForScreen");
            DropTable("dbo.HierarchyDetail");
            DropTable("dbo.Hierarchy");
            RenameIndex(table: "dbo.Log", name: "IX_ScreenId", newName: "IX_TelaId");
            RenameColumn(table: "dbo.Log", name: "ScreenId", newName: "TelaId");
            CreateIndex("dbo.PerfilPorMenu", "MenuId");
            CreateIndex("dbo.PerfilPorMenu", "PerfilId");
            CreateIndex("dbo.Usuario", "Login", unique: true, name: "UQ_Usuario_Login");
            CreateIndex("dbo.Usuario", "Email", unique: true, name: "UQ_Usuario_Email");
            CreateIndex("dbo.Usuario", "PerfilId");
            CreateIndex("dbo.Usuario", "HierarquiaId");
            CreateIndex("dbo.PerfilPorTela", "TelaId");
            CreateIndex("dbo.PerfilPorTela", "PerfilId");
            CreateIndex("dbo.Log", "UsuarioId");
            CreateIndex("dbo.HierarquiaDetalhe", "HierarquiaId");
            CreateIndex("dbo.Hierarquia", "TipoHierarquiaId");
            CreateIndex("dbo.Hierarquia", "HierarquiaPaiId");
            AddForeignKey("dbo.PerfilPorMenu", "PerfilId", "dbo.Perfil", "Id");
            AddForeignKey("dbo.PerfilPorMenu", "MenuId", "dbo.Menu", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Log", "UsuarioId", "dbo.Usuario", "Id");
            AddForeignKey("dbo.Usuario", "PerfilId", "dbo.Perfil", "Id");
            AddForeignKey("dbo.Usuario", "HierarquiaId", "dbo.Hierarquia", "Id");
            AddForeignKey("dbo.PerfilPorTela", "TelaId", "dbo.Tela", "Id", cascadeDelete: true);
            AddForeignKey("dbo.PerfilPorTela", "PerfilId", "dbo.Perfil", "Id");
            AddForeignKey("dbo.Hierarquia", "TipoHierarquiaId", "dbo.TipoHierarquia", "Id");
            AddForeignKey("dbo.Hierarquia", "HierarquiaPaiId", "dbo.Hierarquia", "Id");
            AddForeignKey("dbo.HierarquiaDetalhe", "HierarquiaId", "dbo.Hierarquia", "Id");
            RenameTable(name: "dbo.Profile", newName: "Perfil");
            RenameTable(name: "dbo.Screen", newName: "Tela");
            RenameTable(name: "dbo.HierarchyType", newName: "TipoHierarquia");
        }
    }
}
