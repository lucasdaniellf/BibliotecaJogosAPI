using BibliotecaJogosAPI.Models;
using BibliotecaJogosAPI.Repository.Context;

namespace BibliotecaJogosAPI.Repository.Interface
{
    public interface IGeneroRepository
    {
        public IEnumerable<Genero> SelectTodosGeneros(BibliotecaDBContext _context);
        public Genero? SelectGeneroPorID(BibliotecaDBContext _context, int id);
        public IEnumerable<Genero> SelectGeneroPorNome(BibliotecaDBContext _context, string nome);
        public bool DeleteGenero(BibliotecaDBContext _context, int id);
        public bool InsertGenero(BibliotecaDBContext _context, Genero genero);
        public bool UpdateGenero(BibliotecaDBContext _context, Genero genero);
        public bool SaveChanges(BibliotecaDBContext _context);
    }
}
