using System.Collections.Generic;

namespace Domain.Entities
{
    public class Funcionario
    {
        public int IdFuncionario { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }
        public string RG { get; set; }
        public string Cargo { get; set; }
        public string Telefone { get; set; }
        public string Status { get; set; }
        public ICollection<FuncionarioEmpresa> funcionarioEmpresa { get; set; }
        public virtual Usuario usuario { get; set; }
    }
}
