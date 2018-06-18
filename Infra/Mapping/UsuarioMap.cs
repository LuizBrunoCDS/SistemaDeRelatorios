using Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace Infra.Mapping
{
    public class UsuarioMap : EntityTypeConfiguration<Usuario>
    {
        public UsuarioMap()
        {
            HasKey(a => a.IdUsuario);

            Property(a => a.Login)
                .IsRequired()
                .HasMaxLength(50);

            Property(a => a.Senha)
                .IsRequired()
                .HasMaxLength(50);

            ToTable("Usuarios", "dbo");
        }
    }
}
