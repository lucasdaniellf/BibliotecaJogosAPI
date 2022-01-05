using System.ComponentModel.DataAnnotations;

namespace BibliotecaJogosAPI.Repository.DTO.Estudio.Update
{
    public class EstudioUpdateDTO
    {
        [Required]
        public string Nome { get; set; } = null!;
    }
}
