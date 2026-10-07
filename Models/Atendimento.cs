using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pet_Shop.Models
{
    public class Atendimento
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "O pet é obrigatório.")]
        public int PetId { get; set; }

        [ForeignKey("PetId")]
        public Pet? Pet { get; set; }
        [Required(ErrorMessage = "O serviço é obrigatório.")]
        public int ServicoId { get; set; }
        [ForeignKey("ServicoId")]
        public Servico? Servico { get; set; }
        public DateTime Data { get; set; } = DateTime.Now;
        [Column(TypeName = "decimal(18,2)")]
        public decimal Valor { get; set; }
        [Required]
        public string Status { get; set; } = "Agendado"; // Status inicial do atendimento
    }
}
