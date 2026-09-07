namespace Pet_Shop.Models
{
    public class Atendimento
    {
        public int Id { get; set; }

        public int PetId { get; set; }
        public int ServicoId { get; set; }

        public DateTime Data { get; set; }
        public decimal Valor { get; set; }
        public string Status { get; set; }
    }
}
