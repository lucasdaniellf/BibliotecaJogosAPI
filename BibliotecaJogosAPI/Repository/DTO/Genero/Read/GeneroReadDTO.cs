namespace BibliotecaJogosAPI.Repository.DTO.Genero.Read
{
    public class GeneroReadDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; } = null!;

        //Relações//
        public ICollection<GeneroJogoReadDTO> Jogos { get; set; } = new List<GeneroJogoReadDTO>();
    }
}
