using System.ComponentModel.DataAnnotations;

namespace BibliotecaJogosAPI.Repository.DTO.Jogo.Update
{
    public class JogoUpdateDTO
    {
        [Required]
        public string Nome { get; set; } = null!;
        [DataType(DataType.Date)]
        public DateTime DataCompra { get; set; }
        [Required]
        [Range(0, double.MaxValue)]
        public decimal PrecoCompra { get; set; }

        //Relações//
        public JogoEstudioUpdateDTO? Estudio { get; set; }
        public ICollection<JogoGeneroUpdateDTO> Generos { get; set; } = new List<JogoGeneroUpdateDTO>();
    }
}
