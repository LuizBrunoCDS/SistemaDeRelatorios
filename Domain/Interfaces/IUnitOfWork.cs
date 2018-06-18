using Domain.Entities;
using System;

namespace Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Empresa> EmpresaRepository { get; }
        IRepository<Funcionario> FuncionarioRepository { get; }
        IRepository<FuncionarioEmpresa> FuncionarioEmpresaRepository { get; }
        IRepository<Usuario> UsuarioRepository { get; }
        void Commit();
    }
}
