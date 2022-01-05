using System.ComponentModel.DataAnnotations;

namespace BibliotecaJogosAPI.Repository.DTO.Estudio.Write
{
    public class EstudioWriteDTO
    {
        [Required]
        public string Nome { get; set; } = null!;
    }
}
