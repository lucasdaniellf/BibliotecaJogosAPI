using BibliotecaJogosAPI.Models;
using BibliotecaJogosAPI.Repository.Context;
using BibliotecaJogosAPI.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaJogosAPI.Repository
{
    public class GeneroRepository : IGeneroRepository
    {
        public bool DeleteGenero(BibliotecaDBContext _context, int id)
        {
            var genero = SelectGeneroPorID(_context, id);
            if(genero != null)
            {
                _context.Genero.Remove(genero);
                return SaveChanges(_context);
            }
            return false;
        }

        public bool InsertGenero(BibliotecaDBContext _context, Genero genero)
        {
            _context.Genero.Add(genero);
            return SaveChanges(_context);
        }

        public bool SaveChanges(BibliotecaDBContext _context)
        {
            return _context.SaveChanges() > 0;
        }

        public Genero? SelectGeneroPorID(BibliotecaDBContext _context, int id)
        {
            return _context.Genero.Include(x => x.Jogos).ThenInclude(x => x.Estudio).FirstOrDefault(x => x.Id == id);  
        }

        public IEnumerable<Genero> SelectGeneroPorNome(BibliotecaDBContext _context, string nome)
        {
            var generos = from genero in _context.Genero.Include(x => x.Jogos).ThenInclude(x => x.Estudio)
                          where genero.Nome.ToLower().Contains(nome.ToLower())
                          select genero;
            return generos;
        }

        public IEnumerable<Genero> SelectTodosGeneros(BibliotecaDBContext _context)
        {
            return _context.Genero.Include(x => x.Jogos).ThenInclude(x => x.Estudio).ToList();
        }

        public bool UpdateGenero(BibliotecaDBContext _context, Genero genero)
        {
            return SaveChanges(_context);
        }
    }
}
