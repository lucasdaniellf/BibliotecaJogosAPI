using BibliotecaJogosAPI.Repository.DTO.Jogo.Read;

namespace BibliotecaJogosAPI.Repository.DTO.Estudio.Read
{
    public class EstudioJogoReadDTO
    {
        public int Id { get; set; }

        public string Nome { get; set; } = null!;

        public ICollection<JogoGeneroReadDTO> Generos { get; set; } = new List<JogoGeneroReadDTO>();
    }
}
