using BibliotecaJogosAPI.Models;
using Microsoft.EntityFrameworkCore;
 
namespace BibliotecaJogosAPI.Repository.Context
{
    public class BibliotecaDBContext : DbContext
    {
        public BibliotecaDBContext(DbContextOptions<BibliotecaDBContext> opt) : base(opt)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Genero>().HasIndex(x => x.Nome).IsUnique();
            modelBuilder.Entity<Estudio>().HasIndex(x => x.Nome).IsUnique();
        }

        public DbSet<Jogo> Jogo { get; set; } = null!;
        public DbSet<Genero> Genero { get; set; } = null!;
        public DbSet<Estudio> Estudio { get; set; } = null!;
    }
}
