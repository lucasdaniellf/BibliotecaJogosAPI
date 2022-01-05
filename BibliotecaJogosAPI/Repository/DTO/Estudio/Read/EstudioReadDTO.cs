namespace BibliotecaJogosAPI.Repository.DTO.Estudio.Read
{
    public class EstudioReadDTO
    {
        //Propriedades//
        public int Id { get; set; }
        public string Nome { get; set; } = null!;

        //Relações//
        public ICollection<EstudioJogoReadDTO> Jogos { get; set; } = new List<EstudioJogoReadDTO>();
    }
}
