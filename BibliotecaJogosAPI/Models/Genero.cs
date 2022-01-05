﻿using System;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BibliotecaJogosAPI.Models
{
    public class Genero
    {
        //Propriedades//
        [Key]
        public int Id { get; set; }
        [Required]
        public string Nome { get; set; } = null!;

        //Relações//
        public ICollection<Jogo> Jogos { get; set; } = new List<Jogo>();
    }
}
