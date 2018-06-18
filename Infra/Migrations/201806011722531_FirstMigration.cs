namespace Infra.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Empresas",
                c => new
                    {
                        IdEmpresa = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false, maxLength: 100, unicode: false),
                        Local = c.String(maxLength: 100, unicode: false),
                    })
                .PrimaryKey(t => t.IdEmpresa);
            
            CreateTable(
                "dbo.FuncionarioEmpresa",
                c => new
                    {
                        IdFuncionarioEmpresa = c.Int(nullable: false, identity: true),
                        IdFuncionario = c.Int(nullable: false),
                        IdEmpresa = c.Int(nullable: false),
                        Data = c.DateTime(nullable: false, storeType: "date"),
                        HorarioInicio = c.String(nullable: false, maxLength: 5, unicode: false),
                        HorarioTermino = c.String(nullable: false, maxLength: 5, unicode: false),
                        Observacao = c.String(maxLength: 100, unicode: false),
                    })
                .PrimaryKey(t => t.IdFuncionarioEmpresa)
                .ForeignKey("dbo.Empresas", t => t.IdEmpresa)
                .ForeignKey("dbo.Funcionarios", t => t.IdFuncionario)
                .Index(t => t.IdFuncionario)
                .Index(t => t.IdEmpresa);
            
            CreateTable(
                "dbo.Funcionarios",
                c => new
                    {
                        IdFuncionario = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false, maxLength: 100, unicode: false),
                        CPF = c.String(nullable: false, maxLength: 11, unicode: false),
                        RG = c.String(nullable: false, maxLength: 10, unicode: false),
                        Cargo = c.String(nullable: false, maxLength: 50, unicode: false),
                        Telefone = c.String(maxLength: 15, unicode: false),
                        Status = c.String(nullable: false, maxLength: 10, unicode: false),
                    })
                .PrimaryKey(t => t.IdFuncionario);
            
            CreateTable(
                "dbo.Usuarios",
                c => new
                    {
                        IdUsuario = c.Int(nullable: false),
                        IdFuncionario = c.Int(nullable: false),
                        Login = c.String(nullable: false, maxLength: 50, unicode: false),
                        Senha = c.String(nullable: false, maxLength: 50, unicode: false),
                    })
                .PrimaryKey(t => t.IdUsuario)
                .ForeignKey("dbo.Funcionarios", t => t.IdUsuario)
                .Index(t => t.IdUsuario);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Usuarios", "IdUsuario", "dbo.Funcionarios");
            DropForeignKey("dbo.FuncionarioEmpresa", "IdFuncionario", "dbo.Funcionarios");
            DropForeignKey("dbo.FuncionarioEmpresa", "IdEmpresa", "dbo.Empresas");
            DropIndex("dbo.Usuarios", new[] { "IdUsuario" });
            DropIndex("dbo.FuncionarioEmpresa", new[] { "IdEmpresa" });
            DropIndex("dbo.FuncionarioEmpresa", new[] { "IdFuncionario" });
            DropTable("dbo.Usuarios");
            DropTable("dbo.Funcionarios");
            DropTable("dbo.FuncionarioEmpresa");
            DropTable("dbo.Empresas");
        }
    }
}
