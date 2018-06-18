namespace Infra.Context
{
    using Domain.Entities;
    using Infra.Mapping;
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration.Conventions;

    public class RelatoriosContext : DbContext
    {
        public RelatoriosContext() : base("RelatoriosContext")
        {
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
        }

        #region DbSet
        public virtual DbSet<Usuario> Usuarios { get; set; }
        public virtual DbSet<Empresa> Empresas { get; set; }
        public virtual DbSet<Funcionario> Funcionarios { get; set; }
        public virtual DbSet<FuncionarioEmpresa> FuncionarioEmpresa { get; set; }
        #endregion

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Properties<string>().Configure(c => c.HasColumnType("varchar"));

            modelBuilder.Configurations.Add(new EmpresaMap());
            modelBuilder.Configurations.Add(new FuncionarioMap());
            modelBuilder.Configurations.Add(new UsuarioMap());
            modelBuilder.Configurations.Add(new FuncionarioEmpresaMap());

            base.OnModelCreating(modelBuilder);
        }
    }
}