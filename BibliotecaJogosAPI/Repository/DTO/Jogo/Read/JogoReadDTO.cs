using BibliotecaJogosAPI.Models;

namespace BibliotecaJogosAPI.Repository.DTO.Jogo.Read
{
    public class JogoReadDTO
    {
        public int Id { get; set; }

        public string Nome { get; set; } = null!;

        public DateTime DataCompra { get; set; }

        public decimal PrecoCompra { get; set; }

        //Relações//
        public JogoEstudioReadDTO? Estudio { get; set; }
        public ICollection<JogoGeneroReadDTO> Generos { get; set; } = new List<JogoGeneroReadDTO>();
    }
}
