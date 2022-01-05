using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BibliotecaJogosAPI.Models
{
    public class Jogo
    {
        //Propriedades//
        [Key]
        public int Id { get; set; }
        
        [Required]
        public string Nome { get; set; } = null!;
        
        [DataType(DataType.Date)]
        public DateTime DataCompra { get; set; }
        
        [Required]
        [Range(0, double.MaxValue)]
        public decimal PrecoCompra { get; set; }

        //Relações//
        [ForeignKey("Estudio")]
        public int? IdEstudio { get; set; }
        public Estudio? Estudio { get; set; }
        
        public ICollection<Genero> Generos { get; set; } = new List<Genero>();
    }
}
