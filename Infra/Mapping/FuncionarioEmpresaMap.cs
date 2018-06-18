using Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace Infra.Mapping
{
    public class FuncionarioEmpresaMap : EntityTypeConfiguration<FuncionarioEmpresa>
    {
        public FuncionarioEmpresaMap()
        {
            HasKey(a => a.IdFuncionarioEmpresa);

            Property(a => a.HorarioInicio)
                .IsRequired()
                .HasMaxLength(5);

            Property(a => a.HorarioTermino)
                .IsRequired()
                .HasMaxLength(5);

            Property(a => a.Data)
                .IsRequired()
                .HasColumnType("date");

            Property(a => a.Observacao)
                .IsOptional()
                .HasMaxLength(100);

            ToTable("FuncionarioEmpresa", "dbo");
        }
    }
}
