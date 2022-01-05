using System.ComponentModel.DataAnnotations;

namespace BibliotecaJogosAPI.Repository.DTO.Jogo.Write
{
    public class JogoWriteDTO
    {
        [Required]
        public string Nome { get; set; } = null!;  
        [DataType(DataType.Date)]
        public DateTime DataCompra { get; set; }
        [Required]
        [Range(0, double.MaxValue)]
        public decimal PrecoCompra { get; set; }

        //Relações//
        public JogoEstudioWriteDTO? Estudio { get; set; }
        public ICollection<JogoGeneroWriteDTO> Generos { get; set; } = new List<JogoGeneroWriteDTO>();
    }
}
