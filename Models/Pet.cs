using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Pet_Shop.Models
{
    public class Pet
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "O nome do pet é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome do pet não pode ter mais de 100 caracteres.")]
        public string Nome { get; set; } = string.Empty;
        [Required(ErrorMessage = "A espécie do pet é obrigatória.")]
        public string Especie { get; set; } = string.Empty;
        public string Raca { get; set; } = string.Empty;
        public int Idade { get; set; }
        [Required(ErrorMessage = "O pet deve estar associado a um cliente.")]
        public int ClienteId { get; set; }
        [ForeignKey("ClienteId")]
        public Cliente? Cliente { get; set; }

        [JsonIgnore]
        public ICollection<Atendimento>? Atendimentos { get; set; }
    }
}
