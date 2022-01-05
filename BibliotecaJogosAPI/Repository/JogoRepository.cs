using BibliotecaJogosAPI.Models;
using BibliotecaJogosAPI.Repository.Context;
using BibliotecaJogosAPI.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaJogosAPI.Repository
{
    public class JogoRepository : IJogoRepository
    {
        public bool DeleteJogo(BibliotecaDBContext _context, int id)
        {
            Jogo? jogo = _context.Jogo.FirstOrDefault(x => x.Id == id);
            
            if(jogo != null)
            {
                _context.Jogo.Remove(jogo);
                return SaveChanges(_context);
            }
            return false;
        }

        public bool InsertJogo(BibliotecaDBContext _context, Jogo jogo)
        {
            _context.Jogo.Add(jogo);
            return SaveChanges(_context);
        }

        public bool SaveChanges(BibliotecaDBContext _context)
        {
           return _context.SaveChanges() > 0;
        }

        public Jogo? SelectJogoPorID(BibliotecaDBContext _context, int id)
        {
            return _context.Jogo.Include(x => x.Generos)
                                .Include(y => y.Estudio)
                                .FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<Jogo> SelectJogoPorNome(BibliotecaDBContext _context, string nome)
        {
            var Jogos = from jogo in _context.Jogo.Include(x => x.Generos)
                                                  .Include(y => y.Estudio)
                        where jogo.Nome.ToLower().Contains(nome.ToLower())
                        select jogo;
            return Jogos;
        }

        public IEnumerable<Jogo> SelectTodosJogos(BibliotecaDBContext _context)
        {
            return _context.Jogo.Include( x => x.Estudio)
                                .Include(y => y.Generos)
                                .ToList();
        }

        public bool UpdateJogo(BibliotecaDBContext _context, Jogo jogo)
        {
            return SaveChanges(_context);
        }
    }
}
