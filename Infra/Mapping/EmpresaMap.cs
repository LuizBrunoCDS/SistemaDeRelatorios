using Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace Infra.Mapping
{
    public class EmpresaMap : EntityTypeConfiguration<Empresa>
    {
        public EmpresaMap()
        {
            HasKey(a => a.IdEmpresa);

            Property(a => a.Nome)
                .IsRequired()
                .HasMaxLength(100);

            Property(a => a.Local)
                .IsOptional()
                .HasMaxLength(100);

            ToTable("Empresas", "dbo");
        }
    }
}
