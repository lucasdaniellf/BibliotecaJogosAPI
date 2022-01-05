using System.ComponentModel.DataAnnotations;

namespace BibliotecaJogosAPI.Repository.DTO.Genero.Write
{
    public class GeneroWriteDTO
    {
        [Required]
        public string Nome { get; set; } = null!;
    }
}
