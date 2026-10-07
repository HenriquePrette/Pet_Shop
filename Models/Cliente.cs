using System.ComponentModel.DataAnnotations;

namespace Pet_Shop.Models
{
    public class Cliente
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome do cliente é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome deve ter no máximo 100 caracteres.")]
        public string Nome { get; set; } = string.Empty;
        [Required(ErrorMessage = "O email do cliente é obrigatório.")]
        [StringLength(100, ErrorMessage = "O email deve ter no máximo 100 caracteres.")]
        public string Email { get; set; } = string.Empty;
        [Required(ErrorMessage = "O telefone do cliente é obrigatório.")]
        [Phone(ErrorMessage = "O telefone informado não é válido.")]
        public string Telefone { get; set; } = string.Empty;

        public ICollection<Pet>? Pets { get; set; }
    }
}
