using System.ComponentModel.DataAnnotations;

namespace CastBank.Models
{
    public class Empresa
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Campo requerido")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "Favor informar o CNPJ.")]
        public string CNPJ { get; set; }
        public bool EmprestimoAtivo { get; set; } = false;
    }
}
