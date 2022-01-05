
using BibliotecaJogosAPI.Repository.DTO.Jogo.Read;

namespace BibliotecaJogosAPI.Repository.DTO.Genero.Read
{
    public class GeneroJogoReadDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; } = null!;
        //Relações//
        public JogoEstudioReadDTO? Estudio { get; set; }
    }
}
