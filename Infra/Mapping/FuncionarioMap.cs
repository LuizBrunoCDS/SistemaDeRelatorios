using Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace Infra.Mapping
{
    public class FuncionarioMap : EntityTypeConfiguration<Funcionario>
    {
        public FuncionarioMap()
        {
            HasKey(a => a.IdFuncionario);

            Property(a => a.Nome)
                .IsRequired()
                .HasMaxLength(100);

            Property(a => a.RG)
                .IsRequired()
                .HasMaxLength(10);

            Property(a => a.CPF)
                .IsRequired()
                .HasMaxLength(11);

            Property(a => a.Cargo)
                .IsRequired()
                .HasMaxLength(50);

            Property(a => a.Telefone)
                .IsOptional()
                .HasMaxLength(15);

            Property(a => a.Status)
                .HasMaxLength(10)
                .IsRequired();

            HasOptional(a => a.usuario)
                   .WithRequired(a => a.funcionario);

            ToTable("Funcionarios", "dbo");
        }
    }
}
