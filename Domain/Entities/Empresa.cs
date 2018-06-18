using System.Collections.Generic;

namespace Domain.Entities
{
    public class Empresa
    {
        public int IdEmpresa { get; set; }
        public string Nome { get; set; }
        public string Local { get; set; }
        public virtual ICollection<FuncionarioEmpresa> funcionarioEmpresa { get; set; }
    }
}
