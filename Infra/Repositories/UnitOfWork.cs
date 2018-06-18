using Domain.Entities;
using Domain.Interfaces;
using Infra.Context;
using System;

namespace Infra.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly RelatoriosContext context;
        private Repository<Empresa> empresaRepository;
        private Repository<Funcionario> funcionarioRepository;
        private Repository<Usuario> usuarioRepository;
        private Repository<FuncionarioEmpresa> funcionarioEmpresaRepository;

        public UnitOfWork(RelatoriosContext _context)
        {
            context = _context;
        }

        public IRepository<Empresa> EmpresaRepository => empresaRepository ?? (empresaRepository = new Repository<Empresa>(context));
        public IRepository<Funcionario> FuncionarioRepository => funcionarioRepository ?? (funcionarioRepository = new Repository<Funcionario>(context));
        public IRepository<Usuario> UsuarioRepository => usuarioRepository ?? (usuarioRepository = new Repository<Usuario>(context));
        public IRepository<FuncionarioEmpresa> FuncionarioEmpresaRepository => funcionarioEmpresaRepository ?? (funcionarioEmpresaRepository = new Repository<FuncionarioEmpresa>(context));

        public void Commit()
        {
            context.SaveChanges();
        }

        private bool disposed;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed && disposing)
                context.Dispose();
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
