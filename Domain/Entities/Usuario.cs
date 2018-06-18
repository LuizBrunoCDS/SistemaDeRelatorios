namespace Domain.Entities
{
    public class Usuario
    {
        public int IdUsuario { get; set; }
        public int IdFuncionario { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }
        public virtual Funcionario funcionario { get; set; }
    }
}
