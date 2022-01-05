using System.ComponentModel.DataAnnotations;

namespace BibliotecaJogosAPI.Repository.DTO.Genero.Update
{
    public class GeneroUpdateDTO
    {
        [Required]
        public string Nome { get; set; } = null!;
    
    }
}
