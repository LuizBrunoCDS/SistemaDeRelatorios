using System;

namespace Domain.Entities
{
    public class FuncionarioEmpresa
    {
        public int IdFuncionarioEmpresa { get; set; }
        public int IdFuncionario { get; set; }
        public int IdEmpresa { get; set; }
        public DateTime Data { get; set; }
        public string HorarioInicio { get; set; }
        public string HorarioTermino { get; set; }
        public string Observacao { get; set; }
        public virtual Funcionario funcionario { get; set; }
        public virtual Empresa empresa { get; set; }
    }
}
