namespace Infra.Migrations
{
    using Domain.Entities;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Infra.Context.RelatoriosContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Infra.Context.RelatoriosContext context)
        {
            context.Funcionarios.AddOrUpdate(x => x.IdFuncionario,
                new Funcionario()
                {
                    IdFuncionario = 1,
                    Nome = "Luiz Bruno",
                    Cargo = "Administrador de Sistemas",
                    CPF = "12345678900",
                    RG = "123456789",
                    Telefone = "13 1234567890",
                    Status = "Ativo"
                },
                new Funcionario()
                {
                    IdFuncionario = 2,
                    Nome = "Jose Luiz",
                    Cargo = "Socio Administrador",
                    CPF = "98765432100",
                    RG = "987654321",
                    Telefone = "13 0987654321",
                    Status = "Ativo"
                }
                );
            context.Usuarios.AddOrUpdate(x => x.IdUsuario,
                new Usuario() { IdUsuario = 1, IdFuncionario = 1, Login = "luizbruno.cds", Senha = "e10adc3949ba59abbe56e057f20f883e" }, // 123456
                new Usuario() { IdUsuario = 2, IdFuncionario = 2, Login = "joseluizsfc", Senha = "e10adc3949ba59abbe56e057f20f883e" } // 123456
                );
        }
    }
}
