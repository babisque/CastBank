using System.ComponentModel.DataAnnotations;

namespace CastBank.Models
{
    public class Emprestimo
    {
        public int Id { get; set; }
        public double Valor { get; set; }
        public int Parcelas { get; set; }
        [Display(Name = "Valor de Parcelas")]
        public double ValorParcelas { get; set; }
        public int EmpresaId { get; set; }
        public Empresa Empresa { get; set; }
        public string Status { get; set; } = "Aberto";
    }
}
