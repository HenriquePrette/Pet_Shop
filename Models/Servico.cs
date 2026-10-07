using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pet_Shop.Models
{
    public class Servico
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "O nome do serviço é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome do serviço deve ter no máximo 100 caracteres.")]
        public string Nome { get; set; } = string.Empty;
        [Required(ErrorMessage = "A descrição do serviço é obrigatória.")]
        public string Descricao { get; set; } = string.Empty;
        [Required(ErrorMessage = "O preço é obrigatório.")]
        [Column(TypeName = "decimal(18,2)")]
        [Range(0.01, 10000.00, ErrorMessage = "O preço deve ser maior que zero.")]
        public decimal Preco { get; set; }

        public ICollection<Atendimento>? Atendimentos { get; set; }
    }
}
